<template>
    <div class="remove-recipe">
        <h4>Recept törlése / eltávolítása</h4>
        <ul class="remove-recipe-menu">
            <li @click="myRecipesOpen = !myRecipesOpen">Receptjeim</li>
            <li v-if="myRecipesOpen">
                <ul class="remove-recipe-menu-items">
                    <li v-for="recipe in user.user.meals">
                        {{ recipe.mealName }}
                        <span @click="removeRecipe(recipe.id)">
                            <MinusIcon class="remove-recipe-menu-item-icon"/>
                        </span>
                    </li>
                </ul>
            </li>
                
            <li @click="myFavoritesOpen = !myFavoritesOpen">Kedvenceim</li>
            <li v-if="myFavoritesOpen">
                <ul class="remove-recipe-menu-items">
                    <li v-for="recipe in user.user.favorites">
                        {{ recipe.meal.mealName }}
                        <span @click="removeFavorite(recipe.favId)">
                            <MinusIcon class="remove-recipe-menu-item-icon"/>
                        </span>
                    </li>
                </ul>
            </li>
        </ul>
    </div>
</template>

<script setup>
import MinusIcon from '../assets/icon/MinusIcon.vue';
import { useUserStore } from '../stores/user';
import axios from 'axios';
import urlBase from '../service/urlBase';
import { ref } from 'vue';
import { use } from 'chai';

const user = useUserStore()
const myRecipesOpen = ref(false)
const myFavoritesOpen = ref(false)

const removeRecipe = async (id) => {
    await axios.delete(urlBase.mealUrl + '/delete/' + id, user.config() )
    .then(res => {
        user.user.meals.forEach(recipe => { if (recipe.id == id) user.user.meals.delete(recipe) })
        console.log(res.data)
        user.createPopupMessage(res.data)
    })
    .catch(err => {
        console.log(err)
        user.createPopupMessage("Nem sikerült a recept törlése!")
    })
}

const removeFavorite = async (id) => {
    await axios.delete(urlBase.favoriteUrl + '/delete/' + id, user.config())
    .then(res => {
        user.user.favorites.forEach(recipe => { if (recipe.favId == id) user.user.favorites.delete(recipe) })
        console.log(res.data)
        user.createPopupMessage(res.data)
    })
    .catch(err => {
        console.log(err)
        user.createPopupMessage("Nem sikerült a recept törlése!")
    })
}
</script>