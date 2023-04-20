<template>
    <div>
        <div class="d-flex justify-content-center w-100 mt-5"
             v-if="recipe">
             <form @submit.prevent="modifyRecipe(recipe)"
                   id="recipe-edit-form">
                <h3 class="text-center">Recept módosítás</h3>
                <!-- MEAL NAME -->
                <label for="mealName">Recept</label>
                <input type="text" name="mealName" max="100" 
                    v-model="recipe.mealName"
                    placeholder="Recept">
                <!-- ************************************************* -->

                <label for="discription">Elkészítés</label>
                <textarea name="discription" cols="30" rows="10" 
                          v-model="recipe.discription"
                          placeholder="Leírás..."></textarea>
                <!-- INGREDIENTS -->
                <label for="ingredients">Hozzávalók</label>
                    <!-- QUANTITY -->
                <div>
                    <input type="number" 
                        placeholder="Mennyiség"
                        v-model.number="ingredient.quantity">
                    <select v-model="newMaterial.unitOfMeasure.id">
                        <option value="-1" selected disabled>Válassz</option>
                        <option v-for="m in measures"
                                :key="m"
                                :value="m.id">
                                {{ m.measure }}
                        </option>
                    </select>
                </div>
                    <!-- INGREDIENT NAME -->
                <input type="text" 
                    placeholder="Hozzávaló"
                    v-model="newMaterial.ingredientName">

                <button type="button" @click="addIngredient(ingredient)">Hozzáad</button>
                    <!-- UNIT OF MEASURE LIST -->
                <div>
                    <ul class="ingredients-list"
                        v-if="recipe.ingredients">
                        <li v-for="ing, index in recipe.ingredients" :key="index">
                            <div class="container mt-2 d-flex flex-column flex-lg-row">
                                <input type="number" v-model="ing.quantity">
                                <select v-model="ing.materials.unitOfMeasure.id">
                                    <option v-for="m in measures"
                                            :key="m"
                                            :value="m.id">
                                            {{ m.measure }}
                                    </option>
                                </select>
                                <input type="text" v-model="ing.materials.ingredientName">
                                <p class="ingredient-list-del-btn"
                                   @click="removeIngredient(index)">
                                     <MinusIcon />
                                     <span class="tooltip">Törlés</span>
                                </p>
                            </div>
                            <hr>
                        </li>
                    </ul>
                </div>
                <!-- ************************************************** -->

                <!-- PREPERATION TIME -->
                <label for="preperationTime">Elkészítési idő</label>
                <input type="time"
                    name="preperationTime" 
                    v-model="recipe.preperationTime">
                <!-- ************************************************** -->

                <!-- MEALTYPE -->
                <label for="mealType">Tipus</label>
                <select v-model="recipe.mealType.id">
                    <option v-for="mType in mealTypes" 
                            :key="mType.id" 
                            :value="mType.id">
                            {{ mType.mealTypeName }}
                    </option>
                </select>
                <!-- ************************************************** -->

                <!-- PICTURE -->
                <label for="photo">Kép</label>
                <input type="text" 
                    v-model="recipe.photo"
                    placeholder="URL">
                <!-- ************************************************** -->

                <!-- PRICE -->
                <label for="price">Ár</label>
                <input type="number" v-model="recipe.price">
                <!-- ************************************************** -->

                <!-- PRIVACY -->
                <div class="d-flex justify-content-evenly">
                    <label for="privacy">Publikálom</label>
                    <input type="checkbox" name="privacy" v-model="isPublic">
                </div>
                <!-- ************************************************** -->

                <!-- SUBMIT BUTTON -->
                <button type="submit">Ok</button>
                <!-- ************************************************** -->
            </form>
        </div>
        <button @click="historyBack">Vissza</button>
    </div>
</template>

<script setup>
import MinusIcon from '../assets/icon/MinusIcon.vue';
import { useRouter, useRoute } from 'vue-router';
import { ref, onMounted, onUnmounted, reactive, watch } from 'vue';
import { useUserStore } from '../stores/user';
import axios from 'axios';
import urlBase from '../service/urlBase';

const user = useUserStore() 
const router = useRouter()
const route = useRoute()

const mealTypes = ref(new Set())
const measures = ref(new Set())
const isPublic = ref(false)

watch(isPublic ,() => {
    isPublic.value ? recipe.privacy = 0 : recipe.privacy = 1
})

const recipe = ref({
    id: 0,
    discription: "",
    ingredients: [],
    mealName: "",
    mealType: { id: -1, mealTypeName: '' },
    photo: "",
    preperationTime: "",
    price: 0,
    privacy: 0
})

const newMaterial = reactive({
    unitOfMeasure: {id:-1},
    ingredientName: ''
})

const ingredient = reactive({
    id: 0,
    quantity: 0,
    materials: newMaterial
})

//ADD INGREDIENT TO INGREDIENTS ARRAY
const addIngredient = (ingred) => {
    //deep copy ingredient object
    console.log(ingred)
    if (ingred.quantity > 0 && ingred.materials.unitOfMeasure.id != -1){
        const item = JSON.parse(JSON.stringify(ingred))
        recipe.value.ingredients.push(Object.assign({}, item))
    }
}

const removeIngredient = (index) => recipe.value.ingredients.splice(index,1)

const modifyRecipe = async (meal) => {
    console.log(JSON.parse(JSON.stringify(meal)));
    if (meal !== null){
        await axios.put(urlBase.mealUrl + "/modify", meal, user.config())
        .then(res => {
            console.log(res.data)
            user.createPopupMessage("Sikeres frissítés!")
        })
        .catch(err => {
            console.log(err)
            user.createPopupMessage("Sajnos a recept frissítése nem sikerült!\n Próbálja újra!")
        })
    }else{
        console.log("meal is null")
    }
}

onMounted(async() => {
    //GET A SELECTED MEAL
    await axios(urlBase.mealUrl + "/" + route.params.id)
    .then(res => {
        recipe.value = res.data
        recipe.value.privacy === 0 ? isPublic.value = true : isPublic.value = false
    })
    .catch(err => {
        console.error(err)
    })

    //GET ALL MEALTYPE
    await axios(urlBase.mealTypeUrl + "/all")
    .then((res) => {
        mealTypes.value = res.data.map(type => type)
    })
    .catch((err) => {
        console.log(err.message);
    });

    //GET ALL MEASURES
    await axios(urlBase.measureUrl + "/all")
    .then(res => {
        measures.value = res.data.map(measure => measure)
    })
    .catch(err => {
        console.log(err)
    })
})

onUnmounted(() => {
    recipe.value = null
})

const historyBack = () => {
    router.back()
}
</script>
