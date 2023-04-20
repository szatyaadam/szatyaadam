<template>
        <form class="px-4">
            <h1 class="py-4">Bejelentkezés</h1>
            <!-- LOGIN NAME -->
            <label class="form-label">Felhasználó név</label>
            <input type="text" class="form-control" v-model="form.username">
            <!-- LOGIN PASSWORD -->
            <label class="form-label">Jelszó</label>
            <input type="password" class="form-control" v-model="form.password">
            <!-- LOGIN ERROR MESSAGE -->
            <div class="space-holder">
                <p class="error" v-if="user.errorMessage">{{ user.errorMessage }}</p>
            </div>
            <!-- LOGIN BUTTON -->
            <button class="btn w-50 mt-4" type="button" @mouseup.prevent="login(form)">Ok</button>
        </form>
</template>
<script setup>
import { reactive, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import { useUserStore } from '../stores/user';
import axios from 'axios';
import base from '../service/urlBase';

const router = useRouter()
const user = useUserStore()

const form = reactive({
    username: '',
    password: ''
})

//LOGIN
function login(form) {
  axios.post(base.tokenUrl + "/login", form )
       .then((res) => {
       let accessToken = JSON.stringify(res.data.access_Token);
       let refreshToken = JSON.stringify(res.data.refresh_Token);
       user.addTokensToSession(accessToken, refreshToken)
       user.user.loggedIn = true;
       user.user.userName = res.data.userName
       user.user.email = res.data.email
       user.getUserProfile()
       //Sikeres belépés után átirányítom a kezdő képernyőre
       router.replace("/")
       user.createPopupMessage(`Üdvözöllek kedves ${res.data.userName}!`)
       })
       .catch((err) => {
           user.createErrorMessage(err.response.data);
       });
}

onMounted(() => {
    user.errorMessage = null
})
</script>

<style scoped>
button{
    box-shadow: inset 0 0 3px white;
}

button:hover {
    background: rgba(140, 215, 134, 0.703);
    box-shadow: 0 0 10px ghostwhite, 0 0 20px grey;
}
</style>