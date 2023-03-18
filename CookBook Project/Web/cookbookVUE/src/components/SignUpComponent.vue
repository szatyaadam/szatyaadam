<template>
    <div class="d-flex flex-column justify-content-center align-items-center">
        <form class="w-100">
            <h1 class="py-4">SIGN UP</h1>
            <!-- USERNAME -->
            <label class="form-label">Username</label>
            <input type="text" class="form-control" v-model="form.username" required>
            <!-- ERROR MESSAGE -->
            <p class="error" v-if="form.username.length < 3 && 
                                   form.username.length != 0">Minimum 3 karakter hosszú!</p>
            <!-- EMAIL -->
            <label class="form-label">Email</label>
            <input type="email" class="form-control" v-model="form.email" required>
            <!-- ERROR MESSAGE -->
            <p class="error" v-if="!regex.test(form.email) &&
                                    form.email.length != 0">Email cím formátuma nem megfelelő!</p>
            <!-- PASSWORD -->
            <label class="form-label">Password</label>
            <input type="password" class="form-control" v-model="form.password" required>
            <!-- ERROR MESSAGE -->
            <p class="error" v-if="form.password.length < 8 && 
                                   form.password.length != 0">Minimum 8 karakter!</p>
            <!-- PASSWORD 2X -->
            <label class="form-label">Password</label>
            <input type="password" class="form-control" v-model="form.passwordConfirm" required>
            <!-- ERROR MESSAGE -->
            <p class="error" v-if="form.passwordConfirm != form.password">Nem egyezik a megadott jelszó!</p>
            <!-- SUBMIT BUTTON -->
            <button class="btn w-50 mt-3" 
                v-if="form.username.length > 2 &&
                      regex.test(form.email) &&
                      form.password == form.passwordConfirm" @mouseup.prevent="signUp(form)"
                      type="button">Submit</button>
            <!-- DISABLED BUTTON ON ERROR -->
            <button v-else disabled class="btn w-50 mt-3" type="button">Submit</button>
        </form>
    </div>
</template>

<script setup>
import { reactive } from 'vue';
import axios from 'axios';
import base from "../service/base";
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
    password: '',
    passwordConfirm: ''
})


const signUp = (el) => {
    if (form.password == form.passwordConfirm) {
        let user = JSON.parse(JSON.stringify(el))
        axios.post(base.userUrl + '/signup', user)
            .then(res => {
                console.log(res.data)
            })
            .catch(err => {
                console.log(err.message)
            })
    }
}
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