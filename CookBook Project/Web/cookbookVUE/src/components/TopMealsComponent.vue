<template>
    <div class="col-12 col-lg-2 order-lg-2 m-0">
        <ul v-if="topTen"
            class="top-meals-container d-flex d-lg-inline gap-3">
            
            <li v-for="top in topTen" 
                :key="top.rank">

                <RouterLink class="card_link" 
                            :to="{path: `/recept/${top.id}`}">

                    <div class="rank my-lg-3">
                        <h4 class="rank-number">#{{ top.rank }}</h4>
                        <p>{{ top.mealName }}</p>
                        <h6 class="likes">Likes: {{ top.likes }}</h6>
                    </div>
                </RouterLink>
            </li>
        </ul>
    </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { RouterLink } from 'vue-router';
import axios from 'axios';
import urlBase from '../service/urlBase';
import { useUserStore } from '../stores/user';

const user = useUserStore()
const topTen = ref(null)
//GET TOP TEN MEAL
const getTopTenMeal = () => {
    axios(urlBase.favoriteUrl + "/top")
    .then(res => {
        topTen.value = res.data
    })
    .catch(() => {
        user.createPopupMessage("A toplista üres!")
    })
}

onMounted(() => {
    getTopTenMeal()
})
</script>

<style scoped>
.top-meals-container{
    flex-wrap: nowrap;
    list-style: none;
    height: min-content;
    overflow-x: scroll;
    padding: 20px;
}

.card_link{
    text-decoration: none;
    color: black;
    padding: 0;
    margin: 0;
}
.rank{
    position: relative;
    padding: 0 5px 15px 5px;
    border-radius: 3px;
    min-width: 160px;
    min-height: 100%;
    height: min-content;
    word-wrap: break-word;
    background-color: rgba(255, 255, 255, 0.858);
    align-items: center;
    border-radius: 10px;
}

.rank-number{
    width: fit-content;
    padding: 5px;
    border-radius: 50%;
    background-color: aquamarine;
    transform: translate(-10px, -5px);
}

.rank:hover{
    box-shadow: 0 0 5px black;
}

.likes{
    position: absolute;
    right: 0;
    bottom: -10px;
    background-color: rgb(130, 223, 254);
    padding: 2px 5px;
    border-radius: 10px;
    min-width: min-content;
}
</style>