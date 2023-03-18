<template>
  <main class="d-flex w-100 justify-content-center">
    <div class="container p-0 m-0">
      <div class="row p-0 m-0">
        <div class="col-12 col-lg-2 order-lg-2 sticky-top">
          <ul>
            <li>első</li>
            <li>második</li>
            <li>harmadik</li>
            <li>negyedik</li>
          </ul>
        </div>
        <div class="col-12 col-lg-10">
          <CookBookComponent/>
        </div>
      </div>
    </div>
  </main>
</template>

<script setup>
import AtvaltoComponent from '../components/AtvaltoComponent.vue';
import CookBookComponent from '../components/CookBookComponent.vue';
</script>