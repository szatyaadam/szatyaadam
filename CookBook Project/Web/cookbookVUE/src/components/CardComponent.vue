<template>
    <div class="card bg-light shadow-lg container">
        <div class="row">
            <div class="col">
                <!-- CARD-TITLE -->
                <h1 class="card-title sticky-top bg-light text-center pt-3">
                    {{ props.recept.mealName }}
                    <hr>
                </h1>
            </div>
        </div>

        <div class="row">
            <!-- CARD IMAGE -->
            <div class="col-12 col-lg-6 p-0">
                <div class="image_container">
                    <img src="http://szegedtourism.hu/wp-content/uploads/2017/09/famous2.jpg"
                        class="card-img" alt="...">
                </div>
            </div>
            <!-- CARD-BODY -->
            <div class="col-12 col-lg-6">
                <div class="card-body p-0">
                    <div class="card-text">
                        <!-- DISCRIPTION -->
                        <div class="discription">
                            <h4 class="text-center">Leírás:</h4>
                            <p>{{ props.recept.discription }}</p>
                        </div>
                        <!-- INGREDIENTS -->
                        <div class="card_details">
                            <h4 class="text-center">Hozzávalók:</h4>
                            <p v-if="props.recept.mealtype">{{ props.recept.mealtype.mealTypeName }}</p>
                            <div v-for="ingredient in props.recept.ingredients" :key="ingredient.id">
                                <p v-if="ingredient.quantity">
                                    {{ ingredient.quantity }}
                                    <span v-if="ingredient.material">
                                        {{ ingredient.material.unitOfMeasure.measure }}
                                        {{ ingredient.material.ingredientName }}
                                    </span>
                                </p>
                            </div>
                            <p>Price: {{ recept.price }} Ft.-</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script setup>
import { defineProps } from 'vue';

const props = defineProps(['recept'])
</script>

<style scoped>
img{
    max-height: max-content;
}

.image_container{
    align-items: center;
    display: flex; padding: 10px;
}

.card-text{
    height: 300px;
    overflow: hidden;
}

.card_details{
    max-height: 180px;
    overflow: scroll;
    overflow-x:hidden;
}

.discription{
    height: 100px;
    overflow: hidden;
}
</style>