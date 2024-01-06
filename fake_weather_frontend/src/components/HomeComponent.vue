<script setup>
import {onMounted, ref} from "vue";
import moment from "moment";

  const apiBaseUrl = process.env.VUE_APP_API_URL

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
        .timestamp).format('HH:mm:ss DD.MM.YYYY')

    lastWeatherTemperature.value = lastWeatherResponse
        .weather
        .temperature

    const lastWeathersReferencesResponse = await (await fetch(apiBaseUrl + "/api/WeatherReferences", {
      method: 'GET'
    })).json()

    lastWeathersReferences.value = lastWeathersReferencesResponse.weatherReferences
  }

</script>

<template>

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

    <div v-for="weatherReference in lastWeathersReferences" :key="weatherReference.weatherId">
      Ссылка: {{ weatherReference }}
    </div>

  </div>

</template>
