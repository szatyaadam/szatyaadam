<template>
    <div class="container-fluid">
        <!-- OWN MEALS -->
        <div class="w-100">
            <h3 class="text-center mt-5">Receptjeim</h3>
            <ul class="own-meals-container mt-5 p-0">
                <li class="card-list-item" v-for="recipe in user.user.meals" :key="recipe.id">
                    <RouterLink class="card_link" :to="{ path: `/profil/${user.user.userName}/edit/${recipe.id}` }">
                        <CardComponent :recipe="recipe" />
                    </RouterLink>
                </li>
            </ul>
        </div>
        <!-- FAVORITE MEALS -->
        <div class="w-100">
            <h4 class="text-center mt-5">Kedvenceim</h4>
            <ul class="own-meals-container mt-5 p-0">
                <li class="card-list-item" v-for="recipe in user.user.favorites" :key="recipe.favId">
                    <RouterLink class="card_link" :to="{ path: `/recept/${recipe.meal.id}` }">
                        <CardComponent :recipe="recipe.meal" />
                    </RouterLink>
                </li>
            </ul>
        </div>
    </div>
</template>

<script setup>
import CardComponent from '../components/CardComponent.vue'
import { RouterLink } from 'vue-router';
import { useUserStore } from '../stores/user';

const user = useUserStore()
</script>