<script setup>
  import {onMounted, ref} from "vue";
  import moment from "moment";
  import WeatherComponent from "@/components/WeatherComponent.vue";
  import LoadingSymbol from "@/components/LoadingSymbol.vue";
  import AddWeatherComponent from "@/components/AddWeatherComponent.vue";
  import CloudinessComponent from "@/components/CloudinessComponent.vue";
  import WeatherPhotoComponent from "@/components/WeatherPhotoComponent.vue";
  import {WebClientSendGetRequest} from "@/js/LibWebClient";
  import CreatureWelcomerComponent from "@/components/CreatureWelcomerComponent.vue";

  const isLoading = ref(true)

  const isNoWeathers = ref(false)

  const lastWeatherTime = ref(0)
  const lastWeatherTemperature = ref(0)
  const lastWeatherHumidity = ref(0)
  const lastWeatherPressure = ref(0)
  const lastWeatherCloudiness = ref(0)
  const lastWeatherPhotoPreviewId = ref("")
  const lastWeatherPhotoId = ref("")

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
    const lastWeatherReferenceResponse = await (await WebClientSendGetRequest(
        "/api/WeatherReference/Last"))
        .json()

    if (lastWeatherReferenceResponse.isNoReference === true)
    {
      // We have no last weather (=no weathers in database)
      isNoWeathers.value = true
      isLoading.value = false
      return
    }

    const lastWeatherReference = lastWeatherReferenceResponse.weatherReference.weatherId

    const lastWeatherResponse = await (await WebClientSendGetRequest(
        "/api/Weather/" + lastWeatherReference,
        )).json()

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

    lastWeatherPhotoId.value = lastWeatherResponse
        .weather
        .photoId

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
    const lastWeathersReferencesResponse = await (await WebClientSendGetRequest(
        "/api/WeatherReferences"))
        .json()

    lastWeathersReferences.value = lastWeathersReferencesResponse.weatherReferences
  }

</script>

<template>

  <LoadingSymbol v-if="isLoading"/>

  <div v-if="!isLoading">

    <div class="flex-container-header"><!-- Контейнер с заголовком и ссылками перехода -->

      <div class="full-width-flex-element-header"> <!-- Только заголовое -->
        <h1 class="text-left page-title text-size-home-header"> Погода от Тигона</h1>
      </div>

      <div class="full-width-flex-element-link text-right">

        <CreatureWelcomerComponent />

      </div>


    </div>

    <div class="flex-container">

      <!-- Last weather -->
      <div class="full-width-flex-element">
        <div
            v-if="!isShowDetailedWeather"
            @click="async () => await ShowDetailedWeather()"
            class="latest-weather-summary">
          Показать свежую погоду
        </div>

        <div v-if="isShowDetailedWeather">
          <div
              @click="async () => await HideDetailedWeather()"
              class="latest-weather-summary">
            Скрыть свежую погоду
          </div>

          <div class="latest-weather-details">

            <div v-if="isNoWeathers">
              Нет последней погоды!
            </div>

            <div v-if="!isNoWeathers">
              <div>Время: {{ lastWeatherTime }}</div>
              <div>Температура: {{ lastWeatherTemperature }}</div>
              <div>Влажность: {{ lastWeatherHumidity }}</div>
              <div>Давление: {{ lastWeatherPressure }}</div>

              <CloudinessComponent :cloudiness="lastWeatherCloudiness" />

              <WeatherPhotoComponent
                  :photoPreviewId="lastWeatherPhotoPreviewId"
                  :fullSizePhotoId="lastWeatherPhotoId" />
            </div>

          </div>

        </div>

      </div>

      <!-- Weathers list -->
      <div class="full-width-flex-element">

        <h2 class="text-centered">Список погод:</h2>

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
      <div class="full-width-flex-element text-centered">

        <div class="add-weather-container">
          <AddWeatherComponent
              @weatherAdded="async() => await OnWeatherAdded()"
          />
        </div>

      </div>

<!--      <div>
        <div> &lt;!&ndash;Вопросительная ссылка&ndash;&gt;

          <a class="link-on-home-page" href="/login" title="Вход на сайт">Войти?</a>

        </div>

        <div> &lt;!&ndash;Вопросительная ссылка&ndash;&gt;

          <a class="link-on-home-page" href="/register" title="Вход на сайт">Зарегистрироваться?</a>

        </div>
      </div>-->

    </div>

  </div>

</template>












