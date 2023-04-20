<template>
    <div class="d-flex flex-column">
            <div>
        Válasszon mértékegységet
        <select v-model="mertek" @change="setDataTONull">
            <option value="-1" disabled selected>Válasszon</option>
            <option value="Terfogat">Térfogat</option>
            <option value="Tömeg">Tömeg</option>
        </select>
    </div>
    <div v-if="mertek == 'Terfogat'">

        A megadott Térfogat
        <input type="number" id="numbers2" :value="adat1" @change="setVolume" placeholder="Adjon meg egy számot!">
        <select id="fromVolume" @change="setVolume">
            <option value=1>ml-ben</option>
            <option value=2>cl-ben</option>
            <option value=3>dl-ben</option>
            <option value=4>l-ben</option>
        </select>

        <br>
        Az eredmény {{ adat2 }}
        <select id="toVolume" @change="setVolume">
            <option value=1>ml</option>
            <option value=2>cl</option>
            <option value=3>dl</option>
            <option value=4>l</option>
        </select>
    </div>
    <div v-if="mertek == 'Tömeg'">
        A Megadott Tömeg
        <input type="number" id="numbers3" :value="adat1" @change="setweight" placeholder="Adjon meg egy számot!">
        <select id="fromWeight" @change="setweight">
            <option value=0>mg-ban</option>
            <option value=1>g-ban</option>
            <option value=2>dkg-ban</option>
            <option value=3>kg-ban</option>

        </select>


        <br>
        Az Eredmény {{ adat2 }}
        <select id="toWeight" @change="setweight">
            <option value=0>mg-ban</option>
            <option value=1>g-ban</option>
            <option value=2>dkg-ban</option>
            <option value=3>kg-ban</option>

        </select>
    </div>
    </div>


</template>
<script setup>

import { ref } from "vue";
const mertek = ref('')
const adat1 = ref(0);
const adat2 = ref(0);
const Weight2 = [1000, 10, 100, 1000]

function setDataTONull() {
    adat1.value = 0;
    adat2.value = 0;
}

function setVolume(e, v = +e.target.value) {
    adat1.value = numbers2.value
    adat2.value = (adat1.value / Math.pow(10, (toVolume.value - fromVolume.value)))
}
function setweight(e, v = +e.target.value) {
    v = numbers3.value
    adat1.value = v

    if (toWeight.value < fromWeight.value) {
        adat2.value = 0
        for (let i = fromWeight.value - 1; i >= toWeight.value; i--) {

            v = v * Weight2[i];
            adat2.value = v

        }

    }
    else if (toWeight.value > fromWeight.value) {
        adat2.value = 0
        for (let i = fromWeight.value; i < toWeight.value; i++) {
            v = v / Weight2[i];
            adat2.value = v
        }
    }
    else adat2.value = adat1.value
}

</script>

