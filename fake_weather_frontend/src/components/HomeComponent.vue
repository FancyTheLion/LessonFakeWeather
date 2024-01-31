<script setup>
import {onMounted, ref} from "vue";
import moment from "moment";
import WeatherComponent from "@/components/WeatherComponent.vue";
import LoadingSymbol from "@/components/LoadingSymbol.vue";
import AddWeatherComponent from "@/components/AddWeatherComponent.vue";

  const apiBaseUrl = process.env.VUE_APP_API_URL

  const isLoading = ref(true)

  const lastWeatherTime = ref(0)
  const lastWeatherTemperature = ref(0)

  const lastWeathersReferences = ref([])

  const isShowDetailedWeather = ref(false)

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
        .timestamp).format('DD.MM.YYYY HH:mm')

    lastWeatherTemperature.value = lastWeatherResponse
        .weather
        .temperature

    const lastWeathersReferencesResponse = await (await fetch(apiBaseUrl + "/api/WeatherReferences", {
      method: 'GET'
    })).json()

    lastWeathersReferences.value = lastWeathersReferencesResponse.weatherReferences

    isLoading.value = false
  }

  async function ShowDetailedWeather()
  {
    isShowDetailedWeather.value = true
  }

  async function HideDetailedWeather()
  {
    isShowDetailedWeather.value = false
  }

</script>

<template>

  <LoadingSymbol v-if="isLoading"/>

  <div v-if="!isLoading">
    <h1 class="left underline-text" >Погода от Тигона</h1>

    <div class="flex-container">

      <!-- Last weather -->
      <div class="full-width-flex-element">
        <div
            v-if="!isShowDetailedWeather"
            @click="async () => await ShowDetailedWeather()"
            class="details-summary">
          Показать свежую погоду
        </div>

        <div v-if="isShowDetailedWeather">
          <div
              @click="async () => await HideDetailedWeather()"
              class="details-summary">
            Скрыть свежую погоду
          </div>

           <div>
             <div class="details-window">
               <div>Время: {{ lastWeatherTime }}</div>
               <div>Температура: {{ lastWeatherTemperature }}</div>
             </div>
           </div>
        </div>
      </div>

      <!-- Weathers list -->
      <div class="full-width-flex-element">

        <h2 class="centered">Список погод:</h2>

        <div class="flex-container">

          <div class="weather-list">

            <WeatherComponent
                v-for="weatherReference in lastWeathersReferences" :key="weatherReference.weatherId"
                :weatherId="weatherReference.weatherId">
            </WeatherComponent>

          </div>

        </div>

      </div>

      <!-- Right part -->
      <div class="full-width-flex-element">
      </div>

    </div>

    <AddWeatherComponent />

  </div>

</template>












