<template>
    <div class="d-block text-center">
        <p>Betöltés</p>
        <progress :value="percent" max="100"></progress>
    </div>
</template>

<script setup>
import {ref, onMounted, onUnmounted} from 'vue'

const percent = ref(0)
let load

const loading = () => {
    percent.value += 2
    if(percent.value >= 100){
        percent.value = 0
    }
}

onMounted(() => {
     load = setInterval(loading,20)
})

onUnmounted(() => {
    clearInterval(load)
})
</script>

<style scoped>
progress::-moz-progress-bar 
{
    background: #000000;
    width: 80%;
    border-radius: 5px;
}

progress::-webkit-progress-value 
{
    background: rgb(0, 0, 0);
    width: 80%;
    border-radius: 5px;
}

progress{
    border-radius: 5px;
    -webkit-appearance: none;
    appearance: none;
}

</style>