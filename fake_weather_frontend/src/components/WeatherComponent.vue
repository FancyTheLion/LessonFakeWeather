<script setup>
import {defineProps, onMounted, ref} from "vue";
import moment from "moment/moment";
import LoadingSymbol from "@/components/LoadingSymbol.vue";
import CloudinessComponent from "@/components/CloudinessComponent.vue";

  const apiBaseUrl = process.env.VUE_APP_API_URL

  const props = defineProps({
    weatherId: String
  })

  const isLoading = ref(true)

  const weatherTime = ref(0)
  const weatherTemperature = ref(0)
  const weatherCloudiness = ref(0)

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

    isLoading.value = false
  }

</script>

<template>
  <LoadingSymbol v-if="isLoading" />

  <div
      v-if="!isLoading"
      class="weather-list-element-even weather-list-element-odd">

    Время: {{ weatherTime }}, Температура: {{ weatherTemperature }}

    <CloudinessComponent :cloudiness="weatherCloudiness" />
  </div>
</template>

