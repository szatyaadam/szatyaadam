<template>
    <div class="login_page w-100 d-flex justify-content-center text-center">
        <!-- SING IN -->
        <div :class="{switchCard: disabled}" 
             class="user-login-form" 
             v-if="switchCardState">
            <SignInComponent/>
            <!-- SWITCH TO SING UN BTN -->
            <div class="align-items-center justify-content-end d-flex">
                <button class="btn swap_btn" 
                        @click="switchCard">
                    <SwapIcon/>
                Regisztráció</button>
            </div>
        </div>
        <!-- SING UP -->
        <div :class="{switchCard: disabled}" 
             class="user-login-form" 
             v-else >
            <SignUpComponent/>
            <!-- SWITCH TO SING IN BTN -->
            <div class="align-items-center justify-content-end d-flex">
                <button class="btn swap_btn" 
                        @click="switchCard">
                    <SwapIcon/>
                Bejelentkezés</button>
            </div>
        </div>
    </div>  
</template>

<script setup>
import SwapIcon from '../assets/icon/SwapIcon.vue';
import SignInComponent from '../components/SignInComponent.vue';
import SignUpComponent from '../components/SignUpComponent.vue';
import { ref } from 'vue';

const switchCardState = ref(true)
const disabled = ref(false)

//SWITCH SING IN CARD TO SIGN UP CARD
const switchCard = () => {
    disabled.value = true
    setTimeout(() => {
        disabled.value = false
    }, 800)
    switchCardState.value = !switchCardState.value
}
</script>