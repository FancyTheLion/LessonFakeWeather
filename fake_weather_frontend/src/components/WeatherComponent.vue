<script setup>
import {defineProps, onMounted, ref} from "vue";
import moment from "moment/moment";
import LoadingSymbol from "@/components/LoadingSymbol.vue";
import CloudinessComponent from "@/components/CloudinessComponent.vue";
import WeatherPhotoComponent from "@/components/WeatherPhotoComponent.vue";
import {WebClientSendGetRequest} from "@/js/LibWebClient";
import CommentsListComponent from "@/components/CommentsListComponent.vue";

  const props = defineProps({
    weatherId: String
  })

  const isLoading = ref(true)

  const weatherTime = ref(0)
  const weatherTemperature = ref(0)
  const weatherCloudiness = ref(0)
  const weatherHumidity = ref(0)
  const weatherPressure = ref(0)
  const weatherPhotoPreviewId = ref("")
  const weatherPhotoId = ref("")

  const isCommentsShown = ref(false)

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    const weatherResponse = await (await WebClientSendGetRequest("/api/Weather/" + props.weatherId, {
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

    weatherPhotoPreviewId.value = weatherResponse
        .weather
        .photoPreviewId

    weatherPhotoId.value = weatherResponse
        .weather
        .photoId

    isLoading.value = false
  }

  async function ShowComments()
  {
    isCommentsShown.value = true
  }

</script>

<template>
  <LoadingSymbol v-if="isLoading" />

  <div
      v-if="!isLoading"
      class="weather-list-element-even weather-list-element-odd">

    Время: {{ weatherTime }}, Температура: {{ weatherTemperature }}, Влажность: {{ weatherHumidity }}, Давление: {{ weatherPressure }}

    <CloudinessComponent :cloudiness="weatherCloudiness" />

    <WeatherPhotoComponent
        :photoPreviewId="weatherPhotoPreviewId"
        :fullSizePhotoId = "weatherPhotoId" />

    <div>

      <div
          v-if="!isCommentsShown"
          class="pseudolink"
          @click="async () => await ShowComments()">
        [Загрузить комментарии]
      </div>

      <CommentsListComponent
        v-if="isCommentsShown"
        :weatherId="props.weatherId" />

    </div>
  </div>
</template>

