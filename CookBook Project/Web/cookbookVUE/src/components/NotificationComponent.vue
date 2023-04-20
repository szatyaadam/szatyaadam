<template>
    <div class="notification-container">
        <p>{{ props.message }}</p>
    </div>
</template>

<script setup>
import { defineProps } from 'vue';

const props = defineProps(['message'])
</script>

<style scoped>
.notification-container{
    display: flex;
    flex-direction: column;
    align-items: center;
    position: relative;
    height: 10vh;
    width: 200px;
    min-width: fit-content;
    max-width: 20vw;
    padding: 0 10px;
    word-wrap: break-word;
    min-height: fit-content;
    background-color: aquamarine;
    border-radius: 15px;
}
</style>