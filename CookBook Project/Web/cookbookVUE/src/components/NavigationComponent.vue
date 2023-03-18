<template>
    <nav class="navbar fixed-top">
        <div class="d-flex gap-4 fs-3 justify-content-start mx-3 my-lg-0 align-items-center w-100">
            <RouterLink to="/" class="text-decoration-none text-center">
                <!-- ICON -->
                <svg class="m-auto" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                    <path fill="#FF9800"
                        d="M416 160c0-26.5-21.5-48-48-48h-32c-26.5 0-48 21.5-48 48v160c0 26.5 21.5 48 48 48h32c26.5 0 48-21.5 48-48V160z" />
                    <path fill="#FF9800"
                        d="M312.6 128h-95.2c-14.8 0-26.6 11.8-26.6 26.6 0 14.8 11.8 26.6 26.6 26.6h95.2c14.8 0 26.6-11.8 26.6-26.6 0-14.8-11.8-26.6-26.6-26.6z" />
                    <path fill="#FF9800"
                        d="M224 48C100.3 48 0 148.3 0 272s100.3 224 224 224 224-100.3 224-224S347.7 48 224 48zm0 336c-88.2 0-160-71.8-160-160S135.8 112 224 112s160 71.8 160 160-71.8 160-160 160z" />
                </svg>
            </RouterLink>
            <!-- COOKBOOK (HOME) -->
            <RouterLink to="/" class="text-decoration-none">CookBook</RouterLink>
            <!-- PROFILE -->
            <RouterLink to="/profile" class="text-decoration-none" v-if="user.user.loggedIn">Profile</RouterLink>
            <!-- USER PROFILE -->
            <div id="toggle_user" class="ms-auto"></div>
            <!-- LOGIN -->
            <RouterLink to="/login" class="text-decoration-none" v-if="!user.user.loggedIn">Login</RouterLink>

            <button class="btn w-auto text-light fs-4" v-else type="button"
                @mouseup.prevent="user.logout()">Logout</button>
        </div>
    </nav>
</template>

<script setup>
import { RouterLink } from 'vue-router'
import { useUserStore } from '../stores/user';

const user = useUserStore()
</script>

<style scoped>
nav {
  background: linear-gradient(to bottom, #323232 0%, #3F3F3F 40%, #1C1C1C 150%), linear-gradient(to top, rgba(255,255,255,0.40) 0%, rgba(0,0,0,0.25) 200%);
  background-blend-mode: multiply;
}

nav a{
  color: rgb(245, 245, 245);
}

nav a:hover {
  color: rgb(255, 255, 255);
}

svg{
  height: 80px;
}

</style>