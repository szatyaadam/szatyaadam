import { defineStore } from "pinia";
import { ref, watch } from "vue";
import base from "../service/urlBase";
import axios from "axios";
import urlBase from "../service/urlBase";
import { useRouter } from "vue-router";

export const useUserStore = defineStore("user", () => {
  const router = useRouter();
  //felhasználó adatai
  const user = ref({
    loggedIn: false,
    userName: "",
    email: "",
    meals: new Set(),
    favorites: new Set(),
  });
  //notify the user by popup
  const popupMessage = ref('');
  const popupMessageStack = ref([])
  watch(popupMessageStack.value, () => {
    popupMessageStack.value.forEach((message, index) => {
      setTimeout(() => {
        popupMessage.value = message
        setTimeout(() => {
          popupMessageStack.value.shift()
        },2000)
      }, 2000 * index);
      setTimeout(() => {
        popupMessage.value = ''
      }, 2000 * popupMessageStack.value.length + 1)
    })
  })    
    const createPopupMessage = (message) => {
      popupMessageStack.value.push(message)
  }
  //notify the user
  const notifyMessage = ref("");
  const createNotifyMessage = (message) => {
    notifyMessage.value = message;
    setTimeout(() => {
      notifyMessage.value = "";
    }, 6000);
  };
  //error message to user
  const errorMessage = ref("");
  const createErrorMessage = (message) => {
    errorMessage.value = message;
    setTimeout(() => {
      errorMessage.value = "";
    }, 6000);
  };
  //USER PROFILE
  async function getUserProfile() {
    await axios(base.userUrl + "/profile", configHeader())
      .then((res) => {
        let profile = res.data;
        user.value.userName = profile.userName;
        user.value.email = profile.email;
        for (const meal of profile.meals) {
          user.value.meals.add(meal);
        }
        return profile.favorites;
      })
      .then((favorites) => {
        const requests = favorites.map((favorite) =>
          axios(urlBase.mealUrl + "/" + favorite.mealsId)
        );

        axios.all(requests).then((res) => {
          for (let index = 0; index < res.length; index++) {
            //CREATE OBJECTS FROM PROMISE MEALS AND FAVORITE ID "{MEAL, FAVID}"
            user.value.favorites.add({
              meal: res[index].data,
              favId: favorites[index].id,
            });
          }
        });
        user.value.loggedIn = true;
      })
      .catch(() => {
        user.value.loggedIn = false;
        createPopupMessage("A profil betöltése sikertelen.")
      });
  }
  //KIJELENTKEZÉS
  function logout() {
    const tokens = getTokensFromSession();
    const body = {
      Access_Token: tokens.accessToken,
      Refresh_Token: tokens.refreshToken,
    };
    const config = configHeader();
    axios
      .post(base.tokenUrl + "/logout", body, config)
      .then(() => {
        removeTokensFromSession();
        this.user.loggedIn = false;
        router.replace("/login");
      })
      .catch((err) => {
        console.log(err.message);
      });
  }
  //REFRESH TOKEN HANDLER
  async function refreshTokenHandler() {
    const tokens = getTokensFromSession();
    const body = {
      access_token: tokens.accessToken,
      refresh_token: tokens.refreshToken,
    };
    await axios
      .post(base.tokenUrl + "/refresh", body, configHeader())
      .then((res) => {
        let accessToken = JSON.stringify(res.data.access_Token);
        let refreshToken = JSON.stringify(res.data.refresh_Token);
        addTokensToSession(accessToken, refreshToken);
        getUserProfile();
      })
      .catch((err) => {
        createPopupMessage("Token problem.")
        user.value.loggedIn = false;
      });
  }
  //Configure header - BearerToken
  const configHeader = () => {
    const accessToken = getTokensFromSession().accessToken;

    const config = {
      headers: { Authorization: `Bearer ${accessToken}` },
    };
    return config;
  };
  //Add to favorites
  function addToFavorites(id) {
    if (user.value.loggedIn) {
      let hasMeal = Array.from(user.value.meals.values()).some((meal) => meal.id === id);
      let hasFavorite = Array.from(user.value.favorites.values()).some((meal) => meal.meal.id === id);

      if (!hasMeal && !hasFavorite) {
        const config = configHeader();
        axios(urlBase.favoriteUrl + "/add/" + id, config)
          .then(async () => {
            await axios(urlBase.mealUrl + "/" + id).then((res) => {
              user.value.favorites.add({
                meal: res.data,
                favId: id,
              });
              createPopupMessage(`Hozzáadva a kedvencekhez!`)
              return true;
            });
          })
          .catch((err) => {
            console.log(err)
            return false;
          });
      }else {
        hasMeal ? createPopupMessage("Saját receptet nem lehet hozzáadni a kedvencekhez!") : createPopupMessage("A recept már szerepel a kedvencek között!")
        
      }
    }
    return false;
  }
  //get tokens from session
  function getTokensFromSession() {
    const accessToken = JSON.parse(sessionStorage.getItem("accessToken"));
    const refreshToken = JSON.parse(sessionStorage.getItem("refreshToken"));

    return { accessToken, refreshToken };
  }
  //add tokens to session
  function addTokensToSession(accessToken, refreshToken) {
    sessionStorage.setItem("accessToken", accessToken);
    sessionStorage.setItem("refreshToken", refreshToken);
  }
  //remove tokens from session
  function removeTokensFromSession() {
    sessionStorage.removeItem("accessToken");
    sessionStorage.removeItem("refreshToken");
  }
  return {
    logout,
    user,
    getUserProfile,
    refreshTokenHandler,
    errorMessage,
    notifyMessage,
    popupMessage,
    createPopupMessage,
    createErrorMessage,
    createNotifyMessage,
    config: configHeader,
    addToFavorites,
    addTokensToSession,
    getTokensFromSession,
  };
});
