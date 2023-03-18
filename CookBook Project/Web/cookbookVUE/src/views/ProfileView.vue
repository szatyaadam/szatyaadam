<template>
    <div class="container-fluid">
        <!-- PROFILE CARD -->
        <header class="w-100">
            <div v-if="user.collapsUser" class="navbar_item" @click="user.collapsUser = !user.collapsUser">
                    <span>
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512"><path d="M304 128a80 80 0 1 0 -160 0 80 80 0 1 0 160 0zM96 128a128 128 0 1 1 256 0A128 128 0 1 1 96 128zM49.3 464H398.7c-8.9-63.3-63.3-112-129-112H178.3c-65.7 0-120.1 48.7-129 112zM0 482.3C0 383.8 79.8 304 178.3 304h91.4C368.2 304 448 383.8 448 482.3c0 16.4-13.3 29.7-29.7 29.7H29.7C13.3 512 0 498.7 0 482.3z"/></svg>
                    </span>
            </div>
            <div v-else>
                <ProfileComponent />
                <button type="button" @click="user.collapsUser = !user.collapsUser">Elrejt</button>
            </div>
        </header>
        <!-- OWN MEALS -->
        <main class="w-100">
            <h4>Own Meals</h4>
            <div class="d-flex flex-wrap justify-content-center align-items-center gap-4 mt-5">
                <div id="cook_book_cards" class="shadow-lg p-3 rounded-3" 
                        v-for="recept in user.user.meals"
                        :key="recept.id">
                        <CardComponent :recept="recept" />
                </div>
            </div>
        </main>
        <!-- FAVORITE MEALS -->
        <footer class="w-100">
            <h4>Favorites</h4>
            <div class="d-flex align-items-center gap-4 mt-5">
                <div id="cook_book_cards" class="shadow-lg p-3 rounded-3" 
                        v-for="recept in user.user.favorites"
                        :key="recept.id">
                        <CardComponent :recept="recept" />
                </div>
            </div>
        </footer>
    </div>
</template>

<script setup>
import { onMounted } from 'vue';
import ProfileComponent from '../components/ProfileComponent.vue';
import CardComponent from '../components/CardComponent.vue'
import { useUserStore } from '../stores/user';

const user = useUserStore()

onMounted(() => {
        console.log("PROFILE LOAD")
        if (user.user.loggedIn) {
                user.getUserProfile()
        }
})
</script>

<style scoped>
svg{
    filter: sepia();
    height: 40px;
}
</style>