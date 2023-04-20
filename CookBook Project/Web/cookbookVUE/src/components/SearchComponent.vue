<template>
    <input class="rounded" 
           type="text" 
           placeholder="keresés" 
           v-model="searchInput"
           @change="emitSearch" />
</template>

<script setup>
import { defineEmits, ref } from 'vue';

const emit = defineEmits(['setSearch'])
const searchInput = ref('')

const emitSearch = () => emit('setSearch', searchInput)
</script>