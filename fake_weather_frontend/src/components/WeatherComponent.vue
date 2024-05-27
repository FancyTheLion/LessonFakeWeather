<script setup>
import {defineProps, onMounted, reactive, ref} from "vue";
import moment from "moment/moment";
import LoadingSymbol from "@/components/LoadingSymbol.vue";
import CloudinessComponent from "@/components/CloudinessComponent.vue";
import WeatherPhotoComponent from "@/components/WeatherPhotoComponent.vue";
import {WebClientSendGetRequest, WebClientSendPostRequest} from "@/js/LibWebClient";
import CommentsListComponent from "@/components/CommentsListComponent.vue";
import {required} from "@vuelidate/validators";
import useVuelidate from "@vuelidate/core";

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

  // Add comment form data
  const addCommentData = reactive({
    content: ""
  })

  // Validation rules
  const addCommentFormRules = {

    // Content
    content: {
      $autoDirty: true,
      required
    }
  }

  const addCommentFormValidator = useVuelidate(addCommentFormRules, addCommentData)

  const commentsList = ref(null);

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    await addCommentFormValidator.value.$validate()

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

  async function AddComment(content)
  {
    const response = await WebClientSendPostRequest(
        "/api/Comments/Add",
        {
          "commentToAdd": {
            "content": content,
            "weatherId": props.weatherId
          }
        })

    if (response.status === 200)
    {
      addCommentData.content = ""

      await commentsList.value.ReloadComments()
    }
    else
    {
      alert("Ошибка добавления комментария!")
    }
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

      <div
          v-if="isCommentsShown">

        <CommentsListComponent
            ref="commentsList"
            :weatherId="props.weatherId" />

        <div>
          <div>
            <textarea
                class="add-comment-textarea"
                placeholder="Введите комментарий тут..."
                v-model="addCommentData.content"
            />
          </div>

          <div>

            <button
                type="button"
                :disabled="addCommentFormValidator.$errors.length > 0"
                @click="async () => await AddComment(addCommentData.content)">
              Добавить
            </button>

          </div>

        </div>

      </div>

    </div>
  </div>
</template>

