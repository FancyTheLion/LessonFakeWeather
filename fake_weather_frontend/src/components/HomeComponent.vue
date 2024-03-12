<script setup>
  import {onMounted, ref} from "vue";
  import moment from "moment";
  import WeatherComponent from "@/components/WeatherComponent.vue";
  import LoadingSymbol from "@/components/LoadingSymbol.vue";
  import AddWeatherComponent from "@/components/AddWeatherComponent.vue";
  import CloudinessComponent from "@/components/CloudinessComponent.vue";
  import WeatherPhotoComponent from "@/components/WeatherPhotoComponent.vue";

  const apiBaseUrl = process.env.VUE_APP_API_URL

  const isLoading = ref(true)

  const isNoWeathers = ref(false)

  const lastWeatherTime = ref(0)
  const lastWeatherTemperature = ref(0)
  const lastWeatherHumidity = ref(0)
  const lastWeatherPressure = ref(0)
  const lastWeatherCloudiness = ref(0)
  const lastWeatherPhotoPreviewId = ref("")

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

    if (lastWeatherReferenceResponse.isNoReference === true)
    {
      // We have no last weather (=no weathers in database)
      isNoWeathers.value = true
      isLoading.value = false
      return
    }

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

    lastWeatherHumidity.value = lastWeatherResponse
        .weather
        .humidity

    lastWeatherPressure.value = lastWeatherResponse
        .weather
        .pressure

    lastWeatherCloudiness.value = lastWeatherResponse
        .weather
        .cloudiness

    lastWeatherPhotoPreviewId.value = lastWeatherResponse
        .weather
        .photoPreviewId

    await LoadLastWeatherReferences()

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

  async function OnWeatherAdded()
  {
    await LoadLastWeatherReferences()
  }

  async function LoadLastWeatherReferences()
  {
    const lastWeathersReferencesResponse = await (await fetch(apiBaseUrl + "/api/WeatherReferences", {
      method: "GET"
    })).json()

    lastWeathersReferences.value = lastWeathersReferencesResponse.weatherReferences
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

          <div class="details-window">

            <div v-if="isNoWeathers">
              Нет последней погоды!
            </div>

            <div v-if="!isNoWeathers">
              <div>Время: {{ lastWeatherTime }}</div>
              <div>Температура: {{ lastWeatherTemperature }}</div>
              <div>Влажность: {{ lastWeatherHumidity }}</div>
              <div>Давление: {{ lastWeatherPressure }}</div>

              <CloudinessComponent :cloudiness="lastWeatherCloudiness" />

              <WeatherPhotoComponent :photoId="lastWeatherPhotoPreviewId" />
            </div>

          </div>

        </div>

      </div>

      <!-- Weathers list -->
      <div class="full-width-flex-element">

        <h2 class="centered">Список погод:</h2>

        <div class="flex-container">

          <div class="weather-list">

            <div v-if="isNoWeathers">
              Ни одна погода ещё не была загружена на сайт!
            </div>

            <div v-if="!isNoWeathers">
              <WeatherComponent
                  v-for="weatherReference in lastWeathersReferences" :key="weatherReference.weatherId"
                  :weatherId="weatherReference.weatherId">
              </WeatherComponent>
            </div>

          </div>

        </div>

      </div>

      <!-- Right part -->
      <div class="full-width-flex-element centered">

        <div class="add-weather-container">
          <AddWeatherComponent
              @weatherAdded="async() => await OnWeatherAdded()"
          />
        </div>

      </div>

    </div>

  </div>

</template>












