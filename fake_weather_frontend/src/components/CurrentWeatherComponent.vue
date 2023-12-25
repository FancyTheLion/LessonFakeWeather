<script setup>
  import {onMounted, ref} from "vue";
  import moment from "moment";

  const apiBaseUrl = process.env.VUE_APP_API_URL

  const currentTemperature = ref(0)

  const currentWeatherTime = ref(0)

  // OnMounted hook
  onMounted(async () =>
  {
    await OnLoad();
  })

  // Called when page is loaded
  async function OnLoad()
  {
    const currentWeatherResponse = await (await fetch(apiBaseUrl + "/api/Weather/Current", {
      method: 'GET'
    })).json()

    currentWeatherTime.value = moment(currentWeatherResponse
        .currentWeather
        .timestamp).format('HH:mm:ss DD.MM.YYYY')

    currentTemperature.value = currentWeatherResponse
        .currentWeather
        .temperature
  }

</script>

<template>
  <h1 class="centered">Привет, погода!</h1>

  <div>
    Погода на время: {{ currentWeatherTime }}
  </div>

  <div>
    Сейчас {{ currentTemperature }} градусов.
  </div>
</template>
