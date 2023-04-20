<template>
    <div class="float-container">
        <!-- MAIN FLOAT MENU ITEM -->
        <div class="menu-toggle" 
            :class="menuToggle"
            @click="floatMenu">
            <span class="fa">
                <FloatMainIcon />
            </span>
        </div>

        <!-- ROUND FLOAT MENU -->
        <div class="menu-round" 
            :class="menuRound">
            <!-- ROUND 1 - -->
            <div class="btn-app"
                 @click="user.logout()">
                <div class="fa">
                    <LogOutIcon/>
                </div>
            </div>
            <!-- ROUND 2 - PROFILE MENU -->
            <div class="btn-app" 
                @click="openMenu(profile)">
                <div class="fa">
                    <ProfilIcon />
                </div>
            </div>
            <!-- ROUND 3 - HOME VIEW RECIPES -->
            <div class="btn-app">
                <RouterLink :to="{ path: `/profil/${user.user.userName}` }">
                    <div class="fa">
                        <ProfilBookIcon />
                    </div>
                </RouterLink>
            </div>
        </div>
        <!-- LINE MENU -->
        <div class="menu-line" 
            :class="menuLine">
            <!-- LINE 1 -->
            <!-- CREATE NEW RECIPE -->
            <div class="btn-app"
                 @click="openMenu(newRecipe)">
                <div class="fa">
                    <PlusIcon />
                </div>
            </div>
            <!-- LINE 2 -->
            <!-- REMOVE OWN RECIPE OR FAVORITE -->
            <div class="btn-app"
                 @click="openMenu(removeRecipe)">
                <div class="fa">
                    <MinusIcon/>
                </div>
            </div>

            <!-- FLOAT MENU ITEM TELEPORT TO PROFIL VIEW -->
            <Teleport v-if="newRecipe.open" to="#float-menu-item-place">
                <!-- CREATE RECIPE -->
                <CreateRecipeComponent />
            </Teleport>
            <Teleport v-if="profile.open" to="#float-menu-item-place">
                <!-- PROFIL EDIT -->
                <ProfileComponent />
            </Teleport>
            <Teleport v-if="removeRecipe.open" to="#float-menu-item-place">
                <!-- REMOVE RECIPE -->
                <RemoveRecipeComponent />
            </Teleport>
        </div>
    </div>
</template>

<script setup>
import "../assets/floatmenu.scss"
import LogOutIcon from "../assets/icon/LogOutIcon.vue";
import PlusIcon from '../assets/icon/PlusIcon.vue'
import MinusIcon from "../assets/icon/MinusIcon.vue";
import ProfilBookIcon from '../assets/icon/ProfilBookIcon.vue'
import FloatMainIcon from '../assets/icon/FloatMainIcon.vue';
import ProfilIcon from '../assets/icon/ProfilIcon.vue'
import ProfileComponent from './ProfileComponent.vue';
import CreateRecipeComponent from './CreateRecipeComponent.vue';
import RemoveRecipeComponent from './RemoveRecipeComponent.vue';
import { ref, Teleport, reactive, watch, defineEmits } from 'vue';
import { RouterLink } from 'vue-router';
import { useUserStore } from '../stores/user';

const user = useUserStore()
const emit = defineEmits(['menuIsOpen'])
//MENU ITEMS
const profile = reactive({
    name: 'profil',
    open: false
})
const newRecipe = reactive({
    name: 'newRecipe',
    open: false
})
const removeRecipe = reactive({
    name: 'removeRecipe',
    open: false
})
//MENU ITEMS ARRAY
const menuItems = reactive([profile, newRecipe, removeRecipe])

//VARIABLES
const isActive = ref(false)
//STYLE VARIABLES
const menuToggle = ref('')
const menuRound = ref('')
const menuLine = ref('')

//OPEN ONLY A SINGLE MENU ITEM AND CLOSE ANOTHER
const openMenu = (menu) => {
    menu.open ? menu.open = false : menuItems.map(item => item.name == menu.name ? item.open = true : item.open = false)
}
//CHECK AN OPENED MENU ITEM
const menuItemIsOpen = () => menuItems.some(item => item.open === true)

watch(menuItems, () => {
    emit('menuIsOpen', menuItemIsOpen())
})
//HANDLING CSS CLASSES
const floatMenu = () => {
    isActive.value = !isActive.value

    if (isActive.value) {
        menuToggle.value = 'menu-toggle open'
        menuRound.value = 'menu-round open'
        menuLine.value = 'menu-line open'
    } else {
        menuToggle.value = 'menu-toggle'
        menuRound.value = 'menu-round'
        menuLine.value = 'menu-line'
        //CLOSE ALL MENU ITEM
        closeAllMenuItem()
    }
}

const closeAllMenuItem = () => menuItems.map(item => item.open = false)
</script>