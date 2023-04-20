<template>
  <!-- NAVIGATION -->
  <NavigationComponent />
  <!-- VIEW -->
  <RouterView id="view" 
              :class="{'view-blur': MenuIsOpen}"/>

  <!-- FLOAT MENU CONTENT -->
  <div id="float-menu-item-place" 
       v-if="MenuIsOpen">
  </div>
  <!-- FLOAT MENU -->
  <FloatMenuComonent v-if="user.user.loggedIn"
                     id="float-menu" 
                     @menuIsOpen="menuIsOpen"/>
  <!-- Notification -->
  <NotificationComponent v-if="user.popupMessage"
                         :message="user.popupMessage" 
                         id="float-notificication"/>
</template>

<script setup>
import NavigationComponent from './components/NavigationComponent.vue';
import FloatMenuComonent from './components/FloatMenuComonent.vue';
import NotificationComponent from './components/NotificationComponent.vue';
import { RouterView } from 'vue-router';
import { useUserStore } from './stores/user';
import { ref, onBeforeMount } from 'vue';

const user = useUserStore()
const tokens = user.getTokensFromSession()
const MenuIsOpen = ref(false)


const menuIsOpen = (state) => MenuIsOpen.value = state

onBeforeMount(async () => {if (tokens.accessToken != null && tokens.accessToken != null) await user.refreshTokenHandler() })
</script>