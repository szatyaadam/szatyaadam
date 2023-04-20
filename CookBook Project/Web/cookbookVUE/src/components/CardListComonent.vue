<template>
    <div class="d-flex flex-column align-items-center mt-3">
        <ul class="p-0 container-fluid"
            id="cook-book-cards-container">
            <!-- CREATE A LIST ABOUT CARDS -->
            <li class="p-3 rounded-3 my-2 mx-lg-5"
                v-for="recipe in props.recipes"
                :key="recipe.id">
                <!-- CREATE ROUTER LINKS ABOUT CARDS -->
                <RouterLink class="card_link" 
                            :to="{path: `/recept/${recipe.id}`}">
                    <!-- CARD COMPONENT -->
                    <CardComponent :recipe="recipe" />
                </RouterLink>
            </li>
        </ul>
    </div>
</template>

<script setup>
import CardComponent from './CardComponent.vue';
import { onUnmounted, onMounted, defineEmits, defineProps } from 'vue';

const emit = defineEmits()
const props = defineProps(['recipes', 'loadParams','itemsCount'])

//CHECK THE SCROLL POSITION AND LOAD ANOTHER 20 PIECE OF RECIPE IF THE USER REACH THE BOTTOM OF WINDOW
const checkPos = async () => {
    const maxP = document.getElementById("cook-book-cards-container")
    if (maxP != null){
        const endOfPage = window.innerHeight + window.pageYOffset >= (maxP.offsetHeight);

        if(endOfPage){
            //CHECK ALL RECIPE IS ALREADY LOADED
            if( props.loadParams.itemsPerPage * props.loadParams.page < props.itemsCount){
                window.removeEventListener('scroll', checkPos)
                props.loadParams.page++
                emit('searchLoad', props.loadParams)

                setTimeout(() => {
                    window.addEventListener('scroll', checkPos)
                }, 3000)
            }
        }
    }
}
//ADD SCROLL CHECKER TO WINDOW
onMounted(() => { window.addEventListener('scroll', checkPos) })
//REMOVE SCROLL CHECKER FROM WINDOW
onUnmounted(() => { window.removeEventListener('scroll', checkPos) })
</script>