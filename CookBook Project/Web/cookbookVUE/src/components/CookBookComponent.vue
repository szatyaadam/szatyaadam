<template>
    <div class="d-flex align-items-center gap-5 mt-5">
        <ul v-for="mealType in mealtypes" :key="mealType.id">
            <li @click="loadMeal('',mealType.mealTypeName)">{{ mealType.mealTypeName }}</li>
        </ul>
    </div>
    <div class="d-flex flex-column align-items-center gap-5 mt-5">
        <div class="px-4 m-auto m-lg-0">
            <label class="mx-2">Keresés</label>
            <input class="rounded" type="search" placeholder="...." v-model="params.search" @change="loadMeal(params.search,'')"/>
        </div>
        <div id="cook_book_cards" class="shadow-lg p-3 rounded-3" v-for="recept in recepts" :key="recept.id">
            <CardComponent :recept="recept"/>
        </div>
    </div>
</template>

<script setup>
import CardComponent from './CardComponent.vue';
import base from '../service/base';
import { reactive, onBeforeMount } from 'vue';
import axios from 'axios';

const recepts = reactive([])
const mealtypes = reactive([])
// const meassures = reactive([])

//keresési paraméterek meghatározása
const params = reactive({
        search: '',
        mType: '',
        itemsPerPage: 20,
        page: 1
    })

const loadMeal = (search, mType) => {
    if (search != ''){
        params.search = search
    }
    if (mType != ''){
        params.mType = mType
    }
    //kiürítem a receptek listáját
    recepts.splice(0,recepts.length+1)

    axios(base.mealUrl, {params})
        .then((res) => {
            params.mType = '';
            params.search = '';
            res.data.forEach((element) => {
                recepts.push(element);
            });
            console.log(res.data);
        })
        .catch((err) => {
            console.log(err.message);
        });
}

onBeforeMount(() => {
    loadMeal();
    
    //MEALTYPE
    axios(base.mealTypeUrl + "/all")
        .then((res) => {
            res.data.forEach((element) => {
                mealtypes.push(element);
            });
        })
        .catch((err) => {
            console.log(err.message);
        });
    
    // //MEASSURE
    // axios(base.meassureUrl + "/all")
    //     .then((res) => {
    //         res.data.forEach((element) => {
    //             meassures.push(element);
    //         });
    //         console.log(res.data);
    //     })
    //     .catch((err) => {
    //         console.log(err.message);
    //     });
})

</script>

<style scoped>
#cook_book_cards {
    width: 85%;
    box-sizing: content-box;
}
</style>