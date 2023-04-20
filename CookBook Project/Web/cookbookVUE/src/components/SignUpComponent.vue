<template>
    <form class="px-4">
        <h1 class="py-4">Regisztráció</h1>
        <!-- USERNAME -->
        <label class="form-label">Felhasználó név</label>
        <input type="text" 
               class="form-control" 
               v-model="form.username" 
               required>
        <div class="space-holder">
            <!-- ERROR MESSAGE -->
            <p class="error" 
               v-if="form.username.length < 3 &&
               form.username.length != 0">
               Minimum 3 karakter hosszú nevet adjon meg!
            </p>
        </div>
        <!-- EMAIL -->
        <label class="form-label">Email</label>
        <input type="email" 
               class="form-control" 
               v-model="form.email" 
               required>
        <div class="space-holder">
            <!-- ERROR MESSAGE -->
            <p class="error" 
               v-if="!regex.test(form.email) &&
                     form.email.length != 0">
               Email cím formátuma nem megfelelő!
            </p>
        </div>
        <!-- PASSWORD -->
        <label class="form-label">Jelszó</label>
        <input type="password" 
               class="form-control" 
               v-model="form.password" 
               required>
        <!-- PASSWORD 2X -->
        <label class="form-label">Jelszó megerősítés</label>
        <input type="password" 
               class="form-control" 
               v-model="passwordConfirm" 
               required>
        <div class="space-holder">
            <!-- ERROR MESSAGE -->
            <p class="error" v-if="form.password.length < 8 &&
                                   form.password.length != 0">
                A jelszó minimum 8 karakternek kell lenni!
            </p>
            <p class="error" v-if="passwordConfirm != form.password">
                Nem egyezik a megadott jelszó!
            </p>
            <!-- ERROR MESSAGE FROM SERVER -->
            <p class="error" v-if="user.errorMessage">
                {{ user.errorMessage }}
            </p>
        </div>
        <!-- SUBMIT BUTTON -->
        <button class="btn w-50 mt-3"
                v-if="form.username.length > 2 &&
                regex.test(form.email) && 
                form.password == passwordConfirm" 
                @mouseup.prevent="signUp(form)" 
                type="button">
                Ok
        </button>
        <!-- DISABLED BUTTON ON ERROR -->
        <button v-else 
                disabled 
                class="btn w-50 mt-3" 
                type="button">
                Ok
        </button>
    </form>
</template>

<script setup>
import { reactive, ref, onMounted } from 'vue';
import axios from 'axios';
import base from "../service/urlBase";
import { useUserStore } from '../stores/user';
import { useRouter } from 'vue-router';

const router = useRouter()
const user = useUserStore()

// *****************************************************************************************************************
// //source : https://uibakery.io/regex-library/email
// This Javascript regular expression will match 99% of valid email addresses and will not pass validation for email addresses that have, for instance:

//     Dots in the beginning
//     Multiple dots at the end

// But at the same time it will allow part after @ to be IP address.
const regex = /^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$/
// *****************************************************************************************************************
const form = reactive({
    username: '',
    email: '',
    password: ''
})
const passwordConfirm = ref('')

//SING UP
const signUp = async (form) => {
    if (form.password == passwordConfirm.value && form.password != '') {
        await axios.post(base.userUrl + '/signup', form)
            .then(res => {
                if (res.status === 200) {
                    router.go("/login")
                    user.createPopupMessage("Sikeres regisztráció!")
                }
            })
            .catch(err => {
                user.createErrorMessage(err.response.data)
            })
    }
}

onMounted(() => {
    user.errorMessage = null
})
</script>

<style scoped>
button {
    box-shadow: inset 0 0 3px white;
}

button:hover {
    background: rgba(140, 215, 134, 0.703);
    box-shadow: 0 0 10px ghostwhite, 0 0 20px grey;
}
</style>