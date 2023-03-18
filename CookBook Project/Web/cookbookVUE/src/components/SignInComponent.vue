<template>
        <form class="w-100">
            <h1 class="py-4">SIGN IN</h1>
            <!-- LOGIN NAME -->
            <label class="form-label">Username</label>
            <input type="text" class="form-control" v-model="form.username">
            <!-- LOGIN PASSWORD -->
            <label class="form-label">Password</label>
            <input type="password" class="form-control" v-model="form.password">
            <!-- LOGIN ERROR MESSAGE -->
            <p class="error" v-if="user.errorMessage">{{ user.errorMessage }}</p>
            <!-- LOGIN BUTTON -->
            <button class="btn w-50 mt-4" type="button" @mouseup.prevent="user.login(form)">Submit</button>
        </form>
</template>
<script setup>
import { reactive } from 'vue';
import { useUserStore } from '../stores/user';

const user = useUserStore()

const form = reactive({
    username: '',
    password: ''
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