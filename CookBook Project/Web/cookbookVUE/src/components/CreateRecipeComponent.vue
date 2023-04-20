<template>
    <form @submit.prevent="createRecipe(recipe)"
          id="create-recipt-from">
        <h3 class="text-center">Új recept</h3>
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
                   v-model="ingredient.quantity">
            <select v-model="material.unitOfMeasure.id">
                <option value="-1" disabled>Válassz</option>
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
               v-model="material.ingredientName">

        <button type="button" @click="addIngredient(ingredient)">Add</button>
            <!-- UNIT OF MEASURE LIST -->
        <div>
            <ul class="ingredients-list"
                v-if="recipe.ingredients">
                <li v-for="ing in recipe.ingredients" :key="ing">
                    <div class="container mt-2 d-flex p-0">
                        <input type="text" v-model="ing.quantity">
                        <select v-model="ing.materials.unitOfMeasure.id">
                            <option v-for="m in measures"
                                    :key="m"
                                    :value="m.id">
                                    {{ m.measure }}
                            </option>
                        </select>
                        <input type="text" v-model="ing.materials.ingredientName">
                    </div>
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
            <option value="-1" disabled>Válassz</option>
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
            <input type="checkbox" 
                   name="privacy" 
                   v-model="recipe.privacy">
        </div>
        <!-- ************************************************** -->

        <!-- SUBMIT BUTTON -->
        <button type="submit">Ok</button>
        <!-- ************************************************** -->
    </form>
</template>

<script setup>
import { reactive, ref, onMounted } from 'vue';
import axios from 'axios';
import { useUserStore } from '../stores/user';
import urlBase from '../service/urlBase';

const user = useUserStore()
const mealTypes = ref(new Set())
const measures = ref(new Set())

const material = reactive({
    unitOfMeasure: {
        id: -1
    },
    ingredientName: ""
})

const ingredient = reactive({
    quantity: 0,
    materials: material
})
//time format is 00:01:00
const recipe = reactive({
    mealName: "",
    preperationTime: "",
    price: 0,
    photo: "",
    discription: "",
    privacy: false,
    mealType: {id:-1},
    ingredients: []
})

//ADD INGREDIENT TO INGREDIENTS ARRAY
const addIngredient = (ingred) => {
    //deep copy ingredient object
    const item = JSON.parse(JSON.stringify(ingred))
    recipe.ingredients.push(Object.assign({}, item))
}
//TODO itt még értesíteni kell a felhasználót a request kimeneteléről
//CREATE A NEW RECIPE AND SEND TO API
const createRecipe = async (form) => {
    form.privacy ? form.privacy = 0 : form.privacy = 1
    recipe.preperationTime += ":00"
    console.log(form)
    await axios.post(urlBase.mealUrl + "/add", form, user.config())
    .then(res => {
        console.log(res.data)
        user.user.meals.add(res.data)
        user.createPopupMessage("Az étel sikeresen létrehozva!")
    })
    .catch(err => {
        console.log(err)
        user.createPopupMessage("Az ételt nem sikerült elmenteni.")
    })
}

onMounted(async() => {
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
</script>
