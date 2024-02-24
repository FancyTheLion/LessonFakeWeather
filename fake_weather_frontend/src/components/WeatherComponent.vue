<script setup>
import {defineProps, onMounted, ref} from "vue";
import moment from "moment/moment";
import LoadingSymbol from "@/components/LoadingSymbol.vue";
import CloudinessComponent from "@/components/CloudinessComponent.vue";
import WeatherPhotoComponent from "@/components/WeatherPhotoComponent.vue";

  const apiBaseUrl = process.env.VUE_APP_API_URL

  const props = defineProps({
    weatherId: String
  })

  const isLoading = ref(true)

  const weatherTime = ref(0)
  const weatherTemperature = ref(0)
  const weatherCloudiness = ref(0)
  const weatherHumidity = ref(0)
  const weatherPressure = ref(0)
  const weatherPhotoId = ref("")

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    const weatherResponse = await (await fetch(apiBaseUrl + "/api/Weather/" + props.weatherId, {
      method: 'GET'
    })).json()

    weatherTime.value = moment(weatherResponse
        .weather
        .timestamp).format('DD.MM.YYYY HH:mm:ss')

    weatherTemperature.value = weatherResponse
        .weather
        .temperature

    weatherCloudiness.value = weatherResponse
        .weather
        .cloudiness

    weatherHumidity.value = weatherResponse
        .weather
        .humidity

    weatherPressure.value = weatherResponse
        .weather
        .pressure

    weatherPhotoId.value = weatherResponse
        .weather
        .photoId

    isLoading.value = false
  }

</script>

<template>
  <LoadingSymbol v-if="isLoading" />

  <div
      v-if="!isLoading"
      class="weather-list-element-even weather-list-element-odd">

    Время: {{ weatherTime }}, Температура: {{ weatherTemperature }}, Влажность: {{ weatherHumidity }}, Давление: {{ weatherPressure }}

    <CloudinessComponent :cloudiness="weatherCloudiness" />

    <WeatherPhotoComponent :photoId="weatherPhotoId" />
  </div>
</template>

