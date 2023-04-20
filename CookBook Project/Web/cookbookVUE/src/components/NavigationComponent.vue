<template>
    <nav class="navbar fixed-top align-content-center">
      <!-- ICON -->
      <RouterLink to="/"
         class="text-center"
         @click="goToTop">
        <FSIcon/>
      </RouterLink>
      <!-- EXPANDED MENU -->
      <div class="d-none d-lg-flex flex-grow-1 gap-5 align-items-center">
        <!-- COOKBOOK (HOME) -->
        <RouterLink to="/" class="fs-3">CookBook</RouterLink>

        <!-- PROFILE -->
        <RouterLink v-if="user.user.loggedIn"
                    :to="{path: `/profil/${user.user.userName}`}">
                    Profil
        </RouterLink>

        <!-- SEARCH -->
        <div id="search-input"></div>

        <!-- LOGIN/LOGOUT -->
        <div class="pe-4 ms-auto">
          <!-- LOGIN -->
          <RouterLink v-if="!user.user.loggedIn"
                      to="/login" 
                      >Bejelentkezés
          </RouterLink>

          <!-- LOGOUT -->
          <button v-else type="button"  
                  class="btn w-auto text-light" 
                  @mouseup.prevent="user.logout()"
                  >Kijelentkezés
          </button>
        </div>
      </div>

      <!-- SEARCH -->
      <div v-if="$route.name == 'home'"
           class="d-block d-lg-none">
        <div id="search-input"></div>
      </div>

      <!-- HAMBURGER MENU BUTTON-->
      <div class="d-flex d-lg-none ms-auto pe-2">
        <button @click="expand"
                class="p-0">
          <HamburgerMenuIcon/>
        </button>
      </div>

      <!-- HAMBURGER MENU -->
      <div v-if="!collapse" 
           id="hamburger_nav" 
           class="d-lg-none" 
           @click="expand">
        <HamburgerMenu />
      </div>
    </nav>
</template>

<script setup>
import FSIcon from '../assets/icon/ForkSpoonIcon.vue'
import HamburgerMenuIcon from '../assets/icon/HamburgerMenuIcon.vue';
import { RouterLink } from 'vue-router'
import { useUserStore } from '../stores/user';
import { ref } from 'vue';
import HamburgerMenu from './HamburgerMenuComponent.vue'

const user = useUserStore()
const collapse = ref(true)

const expand = () => collapse.value = !collapse.value
const goToTop = () => window.scrollTo(0,0)
</script>

<style scoped>
.navbar {
  position: sticky;
  display: flex;
  justify-content: start;
  gap: 10px;
  padding-left: 10px;
  height: var(--nav-height);
  background:rgb(0, 0, 0) 99.4%; 
}

@media screen and (max-width: 240px) {
  nav{
    flex-direction: column;
    height: fit-content;
  }
}

.navbar a {
  color: rgb(245, 245, 245);
  text-decoration: none;
}

.navbar a:hover {
  color: rgb(255, 255, 255);
}

#space_holder {
  height: var(--nav-height);
}

button {
  background-color: transparent;
  border: transparent;
}

svg {
  height: 40px;
  align-self: flex-end;
  fill: azure;
  margin: 0 10px;
}

#hamburger_nav {
  position: absolute;
  top: 0px;
  left: 0px;
  width: 100%;
  height: 100vh;
  padding: 0;
  margin-top: var(--nav-height);
  background-color: #100e05f9;
  display: grid;
  align-items: center;
  justify-content: center;
}

.search_input{
  min-width: 80px;
  width: 20vw;
  max-width: 230px;
  align-self: center;
}

@media only screen and (max-width: 260px) {
  #search_input {
    display: none !important;
  }
}
</style>