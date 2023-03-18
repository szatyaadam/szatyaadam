<template>
    <header>
      <NavigationComponent/>
    </header>
    <main class="d-flex align-items-strech">
      <!-- VIEW -->
      <RouterView id="view"/>
      <button id="go_up" @click="goToTop">^<br>UP</button>
    </main>
</template>

<script setup>
import { RouterView } from 'vue-router'
import NavigationComponent from './components/NavigationComponent.vue';

function goToTop(){
  if (window.scrollY != 0){
    window.scrollTo(0,0)
  }
}
</script>


<style>
#view {
  padding-top: 100px;
  background-color: var(--bg-color);
}

main{
  min-height: 100%;
}

#go_up{
  position: fixed;
  box-sizing: border-box;
  bottom: 20px;
  right: 20px;
  height:65px;
  aspect-ratio: 5/6;
  background-color: rgba(110, 67, 23, 0.334);
  color: #fffefe;
  text-align: center;
  border: 5px solid rgba(0, 0, 0, 0.093);
  border-radius: 15px;
  z-index: 10000000;
  box-shadow: 0 0 3px rgb(255, 255, 255);
}

#go_up:hover{
  cursor: pointer;
  background-color: #121212b7;
  color: #ffffff;
}
</style>
