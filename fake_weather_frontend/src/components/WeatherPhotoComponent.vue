<script setup>
import {defineProps, ref} from "vue";

  const isVisible = ref(false)

  const apiBaseUrl = process.env.VUE_APP_API_URL

  const props = defineProps({
    photoPreviewId: String,
    fullSizePhotoId: String
  })

  async function ClickOnPicture()
  {
    isVisible.value = true
  }
</script>

<template>
  <div>
    <img
      class="weather-photo-preview"
      :src="apiBaseUrl + '/api/Files/Download/' + props.photoPreviewId"
      alt="Фото погоды (превью)"
      @click="async() => await ClickOnPicture()" />
  </div>


  <div v-if="isVisible">

    <!-- Popup lower layer -->
    <div class="popup-lower-layer">
    </div>

    <!-- Popup upper layer -->
    <div class="popup-upper-layer">

      <div class="popup-main-image-section">
        <img
            class="popup-image"
            :src="apiBaseUrl + '/api/Files/Download/' + props.fullSizePhotoId"
            alt="Полноразмерное фото погоды"/>
      </div>

    </div>

  </div>
</template>