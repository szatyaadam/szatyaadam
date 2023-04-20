<template>
    <section class="d-flex w-100 justify-content-center">
      <div class="container p-0 m-0">
        <div class="row p-0 m-0">
          <TopMealsComponent/>
          <div class="col-12 col-lg-10">
            <p class="text-center mt-3 fs-3">Keresgéljen kedvére a sok nagyszerű recept között.</p>
            <MealtypesComponent @setType="setType"/>
            <p class="text-center" v-if="!inProgress">{{ itemsCount }} recept található.</p>
            <CardListComponent v-if="recipes"
                              @searchLoad="searchLoad(loadParams)" 
                              :recipes="recipes" 
                              :loadParams="loadParams"
                              :itemsCount="itemsCount"/>
            <div class="d-flex align-items-center justify-content-center"
                v-if="onError">
                <h3>Valami hiba történt.</h3>
            </div>
            <div class="d-flex align-items-center justify-content-center"
                v-if="inProgress && !onError">
                <ProgressComponent/>
            </div>
            <div class="d-flex align-items-center justify-content-center"
                v-if="!inProgress && itemsCount == 0">
                <h3>Nem található a keresett recept!</h3>
            </div>
          </div>
        </div>
      </div>
      <Teleport to="#search-input">
        <SearchComponent @setSearch="setSearch"/>
      </Teleport>
    </section>
</template>

<script setup>
import { reactive, ref, onMounted, Teleport } from 'vue';
import base from '../service/urlBase';
import axios from 'axios';
import AtvaltoComponent from '../components/AtvaltoComponent.vue';
import MealtypesComponent from '../components/MealtypesComponent.vue';
import CardListComponent from '../components/CardListComonent.vue';
import ProgressComponent from '../components/ProgressComponent.vue';
import TopMealsComponent from '../components/TopMealsComponent.vue';
import SearchComponent from '../components/SearchComponent.vue';

const ITEMS_PER_PAGE = parseInt(import.meta.env.VITE_ITEMS_PER_PAGE)

let recipes = reactive(new Set())
const page = ref(1)
const itemsCount = ref(null)
const inProgress = ref(false)
const onError = ref(false)

const loadParams = reactive({
  search : '',
  mType : '',
  page : page.value,
  itemsPerPage : ITEMS_PER_PAGE
  })

const setSearch = (param) => {
  loadParams.search = param
  searchLoad(loadParams)
}

const setType = (mType) => {
  loadParams.mType = mType
  searchLoad(loadParams)
}

const searchLoad = async (param) => {
  inProgress.value = true
  onError.value = false
  recipes.clear
  await axios(base.mealUrl, { params: param })
  .then(res => {
    itemsCount.value = res.data.totalItems
    if (loadParams.page != 1){
      let nextPage = res.data.data.map(meal => meal)
      recipes = new Set([...recipes, ...nextPage])
    }else{
      recipes.clear
      recipes = res.data.data.map(meal => meal)
    }
    inProgress.value = false
  })
  .catch(err => {
    onError.value = true
    console.error(err.message)
  })
}
//RECIPES
const load = async () =>{
  inProgress.value = true
  onError.value = false
  recipes.clear
  await axios(base.mealUrl, { params: loadParams })
  .then(res => {
    itemsCount.value = res.data.totalItems
    const meals = Array.from(res.data.data).reverse()
    recipes = meals.map(meal => meal)
    inProgress.value = false
  })
  .catch(err => {
    onError.value = true
    console.error(err.message)
  })
}

onMounted(async() => {
  await load()
})
</script>