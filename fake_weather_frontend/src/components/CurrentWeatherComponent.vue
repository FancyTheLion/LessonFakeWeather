<script setup>
import {onMounted, ref} from "vue";
import moment from "moment";

  const apiBaseUrl = process.env.VUE_APP_API_URL

  const lastWeatherTime = ref(0)
  const lastWeatherTemperature = ref(0)

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

    console.log(lastWeatherResponse)

    lastWeatherTime.value = moment(lastWeatherResponse
        .weather
        .timestamp).format('HH:mm:ss DD.MM.YYYY')

    lastWeatherTemperature.value = lastWeatherResponse
        .weather
        .temperature
  }

</script>

<template>
  <h1 class="centered">Привет, погода!</h1>
  <h2 class="centered">Самая свежая (из известных) погода:</h2>
  <p class="centered">Время: {{ lastWeatherTime }}</p>
  <p class="centered">Температура: {{ lastWeatherTemperature }}</p>


</template>
