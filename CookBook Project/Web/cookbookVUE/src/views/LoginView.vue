<template>
    <div class="login_page w-100 d-flex justify-content-center text-center">
        <div :class="{flip: disabled}" class="form_card d-flex flex-column justify-content-between"  v-if="flipCard">
            <div class="h-75 w-50 m-auto">
                <SignInComponent class="h-100"/>
            </div>
            <div class="h-25 text-end align-items-center justify-content-end d-flex">
                <button class="btn w-25 swap_btn" @click="flip">
                    <img src="https://cdn-icons-png.flaticon.com/512/5266/5266493.png" alt="swap">
                Sign Up</button>
            </div>
        </div>
        <div :class="{flip: disabled}" class="form_card d-flex flex-column justify-content-between" v-else >
            <div class="h-75 w-50 m-auto">
                <SignUpComponent class="h-100"/>
            </div>
            <div class="h-25 text-end align-items-center justify-content-end d-flex">
                <button class="btn w-25 swap_btn" @click="flip">
                    <img src="https://cdn-icons-png.flaticon.com/512/5266/5266493.png" alt="swap">
                Sign Ip</button>
            </div>
        </div>
    </div>
</template>

<script setup>
import SignInComponent from '../components/SignInComponent.vue';
import SignUpComponent from '../components/SignUpComponent.vue';
import { ref } from 'vue';

const flipCard = ref(true)
const disabled = ref(false)

const flip = () => {
    disabled.value = true
    setTimeout(() => {
        disabled.value = false
    }, 800)
    flipCard.value = !flipCard.value
}

</script>

<style scoped>
.form_card{
    background:rgba(56, 56, 56, 0.455);
    box-shadow: 0 0 10px ghostwhite, 0 0 20px grey;
    border-radius: 60px;
    border-radius: 50px;
    min-width: 360px;
    max-height: max-content;
    height: fit-content;
    max-width: 660px;
    width: 60%;
    padding: 10px 0;
    margin-top: 50px;
}
.login_page{
    height: 100vh;
}

button, button:hover, button:active{
    background-color: rgba(240, 248, 255, 0.703);
}

img{
    height: 30px;
}

.flip {
  animation: flip 0.8s linear;
}

@keyframes flip {
    0%{
        width: 100%;
        filter: blur(10px);
    }
    50%{
        width: 0%;
        background-color: rgba(0, 0, 0, 0.678);
        filter: blur(20px);
    }
    100%{
        width: 100%;
        background-color: rgba(156, 156, 156, 0.214);
        transform: rotateY(0);
        filter: blur(0px);
    }

}

.swap_btn{
    border-radius: 5px 2px 36px 5px;
}
</style>