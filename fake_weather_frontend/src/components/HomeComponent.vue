<script setup>
import {onMounted, ref} from "vue";
import moment from "moment";
import WeatherComponent from "@/components/WeatherComponent.vue";
import LoadingSymbol from "@/components/LoadingSymbol.vue";

  const apiBaseUrl = process.env.VUE_APP_API_URL

  const isLoading = ref(true)

  const lastWeatherTime = ref(0)
  const lastWeatherTemperature = ref(0)

  const lastWeathersReferences = ref([])

  // OnMounted hook
  onMounted(async () =>
  {
    await OnLoad();
  })

  // Called when page is loaded
  async function OnLoad()
  {
    const lastWeatherReferenceResponse = await (await fetch(apiBaseUrl + "/api/WeatherReference/Last", {
      method: 'GET'
    })).json()

    const lastWeatherReference = lastWeatherReferenceResponse.weatherReference.weatherId

    const lastWeatherResponse = await (await fetch(apiBaseUrl + "/api/Weather/" + lastWeatherReference, {
      method: 'GET'
    })).json()

    lastWeatherTime.value = moment(lastWeatherResponse
        .weather
        .timestamp).format('DD.MM.YYYY HH:mm:ss')

    lastWeatherTemperature.value = lastWeatherResponse
        .weather
        .temperature

    const lastWeathersReferencesResponse = await (await fetch(apiBaseUrl + "/api/WeatherReferences", {
      method: 'GET'
    })).json()

    lastWeathersReferences.value = lastWeathersReferencesResponse.weatherReferences

    isLoading.value = false
  }

</script>

<template>

  <LoadingSymbol v-if="isLoading"/>

  <div v-if="!isLoading">
    <h1 class="centered">Привет, погода!</h1>

    <!-- Last weather -->
    <div class="centered">
      <h2>Самая свежая (из известных) погода:</h2>
      <div>Время: {{ lastWeatherTime }}</div>
      <div>Температура: {{ lastWeatherTemperature }}</div>
    </div>

    <!-- Weathers list -->
    <div class="centered">
      <h2>Список погод:</h2>

      <div class="weather-list">
        <WeatherComponent
            v-for="weatherReference in lastWeathersReferences" :key="weatherReference.weatherId"
            :weatherId="weatherReference.weatherId">

        </WeatherComponent>
      </div>

    </div>

  </div>

</template>
