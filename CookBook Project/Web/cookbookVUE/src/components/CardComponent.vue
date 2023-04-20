<template>
    <div class="card bg-light shadow-lg">
        <!-- CARD IMAGE -->
        <div class="card-img-top" :style="{ backgroundImage: 'url(' + sourceImg + ')' }">
            <!-- LIKE -->
            <div v-if="($route.name == 'home' ||
                $route.name == 'recept')" class="align-self-start ms-auto">
                <button @click.prevent="like(props.recipe.id)" class="like-button" :class="{ likeEffect: likeEff }">
                    <span v-if="!user.user.loggedIn" :class="{ tooltip: tooltipShow }">Bejelentkezés szükséges!</span>
                    <LikeIcon />
                </button>
            </div>
        </div>

        <div class="card-body" :id="props.recipe.id">
            <!-- CARD-TITLE -->
            <div class="card-title">
                <h6 class=" text-center">
                    {{ props.recipe.mealName }}
                    <p v-if="props.recipe.mealType" style="font-size: 0.7em;">
                        ({{ props.recipe.mealType.mealTypeName }})
                    </p>
                </h6>
            </div>
            <!-- CARD TEXT -->
            <div class="card-text">
                <!-- DISCRIPTION -->
                <div class="discription">
                    <h6 class="text-center">Leírás:</h6>
                    <p> {{ props.recipe.discription }} </p>
                </div>

                <!-- INGREDIENTS -->
                <div class="card_details" v-if="$route.name != 'home'">
                    <hr>
                    <table v-if="props.recipe.ingredients" class="table">
                        <tr>
                            <th>Menny.</th>
                            <th>Mért.egy.</th>
                            <th>Hozzávaló</th>
                        </tr>
                        <tr v-for="ingredient in props.recipe.ingredients" :key="ingredient.id">
                            <td v-if="ingredient.quantity">{{ ingredient.quantity }}</td>
                            <td v-if="ingredient.materials">{{ ingredient.materials.unitOfMeasure.measure }}</td>
                            <td v-if="ingredient.materials">{{ ingredient.materials.ingredientName }}</td>
                        </tr>
                    </table>
                    <div class="d-flex justify-content-between mb-4">
                        <p class="card-price">Ár: {{ props.recipe.price }} Ft.-</p>
                        <span @click.prevent="printDiv">
                            <PrintIcon class="print-btn" />
                        </span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script setup>
import LikeIcon from '../assets/icon/LikeIcon.vue';
import PrintIcon from '../assets/icon/PrintIcon.vue';
import { ref, computed } from 'vue';
import { useUserStore } from '../stores/user';

const props = defineProps(['recipe'])
const user = useUserStore()
const likeEff = ref(false)
const tooltipShow = ref(true)
//todo megoldani hogy lehessen képet feltölteni a recepthez
const bgImage = "http://szegedtourism.hu/wp-content/uploads/2017/09/famous2.jpg"

const sourceImg = computed(() => { return props.recipe.photo !== "" ? props.recipe.photo : bgImage })

//ADD TO FAVORITES THE CARD
const like = (id) => {
    if (user.user.loggedIn) {
        let success = user.addToFavorites(id)
        if (success){
            likeEff.value = true
            setTimeout(() => {
                likeEff.value = false
            }, 1500)
        }
    }
}

const printDiv = () => {
    document.querySelector('.print-btn').style.display = 'none'
    let divContents = document.getElementById(props.recipe.id).innerHTML;
    let a = window.open('', '', 'height=500px, width=450px');
    document.querySelector('.print-btn').style.visibility = 'block'
    a.document.write('<html><body >');
    a.document.write(divContents);
    a.document.write('</body></html>');
    a.document.close();
    a.print();
}
</script>