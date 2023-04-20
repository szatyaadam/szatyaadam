<template>
    <ul id="hamburger-menu-container">
        <li>
            <!-- COOKBOOK (HOME) -->
            <RouterLink to="/">CookBook</RouterLink>
        </li>
        <li v-if="user.user.loggedIn">
            <!-- PROFILE -->
            <RouterLink :to="{path: `/profil/${user.user.userName}`}">Profil
            </RouterLink>
        </li>
        <li>
            <!-- LOGIN/LOGOUT -->
            <div class="ms-auto">
                <!-- LOGIN -->
                <RouterLink to="/login" v-if="!user.user.loggedIn">Bejelentkezés
                </RouterLink>
                <!-- LOGOUT -->
                <a class="ms-auto" v-else @mouseup.prevent="user.logout()">Kijelentkezés
                </a>
            </div>
        </li>
    </ul>
</template>

<script setup>
import { RouterLink } from 'vue-router';
import { useUserStore } from '../stores/user';

const user = useUserStore()
</script>