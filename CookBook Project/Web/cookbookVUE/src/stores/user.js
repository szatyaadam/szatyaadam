import { defineStore } from "pinia";
import axios from "axios";
import { reactive, onBeforeMount, ref } from "vue";
import base from "../service/base";

export const useUserStore = defineStore("user", () => {
  const user = reactive({
    loggedIn: false,
    userName: '',
    email: '',
    meals: [],
    favorites: [],
  });
  
  const collapsUser = ref(true)
  //notify the user
  const notifyMessage = ref ('')
  const createNotifyMessage =(message) => {
    notifyMessage.value = message

    setInterval(() => {
      notifyMessage.value = ''
    },6000)
  }
  //error message to user
  const errorMessage = ref('')
  const createErrorMessage = (message) => {
    errorMessage.value = message

    setInterval(() => {
      errorMessage.value = ''
    },6000)
  }
  
  onBeforeMount(() => {
    const accessToken = JSON.parse(sessionStorage.getItem("accessToken"));
    const refreshToken = JSON.parse(sessionStorage.getItem("refreshToken"));
    if (accessToken != null && refreshToken != null) {
      console.log("stayLOGIN")
      stayLogin(accessToken, refreshToken)
    }
  });
  //USER PROFILE
  function getUserProfile() {
    axios(base.userUrl + "/profile", config())
      .then((res) => {
        const profile = res.data
        user.userName = profile.userName;
        user.email = profile.email;
        profile.favorites.forEach((element) => {
          user.favorites.push(element);
        });
        profile.meals.forEach((element) => {
          user.meals.push(element);
        });
      })
      .catch((err) => {
        user.loggedIn = false;
        console.log(err.message);
      });
  }
  //BEJELENTKEZÉS
  function login(el) {
    let user = JSON.parse(JSON.stringify(el));
    axios
      .post(base.tokenUrl + "/login", user)
      .then((res) => {
        let accessToken = JSON.stringify(res.data.access_Token);
        let refreshToken = JSON.stringify(res.data.refresh_Token);
        sessionStorage.setItem("accessToken", accessToken);
        sessionStorage.setItem("refreshToken", refreshToken);
        this.user.loggedIn = true;
        //Sikeres belépés után átirányítom a kezdő képernyőre
        location.replace("/")
      })
      .catch((err) => {
        // console.log(err.response.data)
        if(err.response){
          createErrorMessage(err.response.data);
        }
      });
  }
  //KIJELENTKEZÉS
  function logout() {
    const accessToken = JSON.parse(sessionStorage.getItem("accessToken"));
    const refreshToken = JSON.parse(sessionStorage.getItem("refreshToken"));

    const body = {
      access_token: accessToken,
      refresh_token: refreshToken,
    };

    axios
      .post(base.tokenUrl + "/logout", body, config())
      .then(() => {
        console.log("kilépett");
        sessionStorage.removeItem("accessToken");
        sessionStorage.removeItem("refreshToken");
        this.user.loggedIn = false;
        location.replace("/login")
      })
      .catch((err) => {
        console.log(err.message);
      });
  }
  //REFRESH TOKEN HANDLING
  function stayLogin(accessToken, refreshToken) {
    sessionStorage.removeItem("accessToken", accessToken);
    sessionStorage.removeItem("refreshToken", refreshToken);

    const body = {
      access_token: accessToken,
      refresh_token: refreshToken,
    };
    
    axios
      .post(base.tokenUrl + "/refresh", body, config())
      .then((res) => {
        let accessToken = JSON.stringify(res.data.access_Token);
        let refreshToken = JSON.stringify(res.data.refresh_Token);
        sessionStorage.setItem("accessToken", accessToken);
        sessionStorage.setItem("refreshToken", refreshToken);
        user.loggedIn = true;
        getUserProfile()
      })
      .catch((err) => {
        console.log(err.message);
        user.loggedIn = false;
      });
  }
  //Configure header - BearerToken
  const config = () => {
    const accessToken = JSON.parse(sessionStorage.getItem("accessToken"));
    const config = {
      headers: { Authorization: `Bearer ${accessToken}` },
    };

    return config;
  }

  return { login, logout, user, getUserProfile, stayLogin, errorMessage, notifyMessage, createErrorMessage, createNotifyMessage, config, collapsUser };
});
