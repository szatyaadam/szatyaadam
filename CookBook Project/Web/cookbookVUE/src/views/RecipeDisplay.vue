<template>
    <div class="d-flex flex-column align-items-center pt-3">
        <CardComponent v-if="recipe"
                       :recipe="recipe"
                       class="card"/>
        <button class="align-self-end mx-auto" @click="historyBack">Vissza</button>
    </div>
</template>

<script setup>
import CardComponent from '../components/CardComponent.vue';
import { useRouter, useRoute } from 'vue-router';
import { ref, onMounted, onUnmounted } from 'vue';
import axios from 'axios';
import urlBase from '../service/urlBase';

const router = useRouter()
const route = useRoute()

const recipe = ref(null)

onMounted(() => {
    axios(urlBase.mealUrl + "/" + route.params.id)
    .then(res => {
        recipe.value = res.data
    })
    .catch(err => {
        console.error(err)
    })
})

onUnmounted(() => recipe.value = null )

const historyBack = () => router.back()

</script>

<style scoped>
.card{
    width: 80%;
}

@media only screen and (max-width: 576px) {
    .card {
    width: 95%;
    }
}

button{
    height: 30px;
    position: relative;
    aspect-ratio: 2/1;
    z-index: 10000;
    align-self: flex-end;
    background-color: aliceblue;
    border: 2px solid wheat;
    border-radius: 5px;
    transform: translateY(-50%);
    font-weight: bolder;
    font-size: 0.8em;
    box-shadow: 0px -1px 1px black;
}

.card-container{
    width: 80%;
}
</style>