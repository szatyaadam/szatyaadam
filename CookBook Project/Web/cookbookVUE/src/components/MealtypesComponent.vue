<template>
    <div class="overflow-wrap"
         id="mealtypes-container">
        <ul>
            <!-- MEALTYPES -->
            <li class="mealtype-button"
                v-for="mealT
                in mealTypes" 
                :key="mealT.id"
                @click=" emit('setType',mealT.mealTypeName), mealType = mealT.mealTypeName">
                {{ mealT.mealTypeName }}
            </li>
        </ul>
        <div class="w-100 text-center">
            <!-- FILTER TITLE -->
            <div id="search_title_placeholder">
                <div class="d-flex justify-content-center align-items-center gap-3"  v-if="mealType">
                    <span>Szűrő: {{ mealType }}</span>
                    <button class="mealtype-button"
                            @click="emit('setType',''), mealType = ''">
                            Mind
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, onBeforeMount } from 'vue';
import urlBase from '../service/urlBase';
import axios from 'axios';

const emit = defineEmits(['setType'])

const mealTypes = ref(new Set())
const mealType = ref('')

onBeforeMount(async () => {
    //MEALTYPE
    await axios(urlBase.mealTypeUrl + "/all")
    .then((res) => {
        mealTypes.value = res.data.map(type => type)
    })
    .catch((err) => {
        console.log(err.message);
    });
})

</script>