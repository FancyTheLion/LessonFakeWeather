<script setup>
import {defineProps, ref} from "vue";

  const isVisible = ref(false)

  const apiBaseUrl = process.env.VUE_APP_API_URL

  const props = defineProps({
    photoPreviewId: String,
    fullSizePhotoId: String
  })

  async function ShowFullSizePhoto()
  {
    isVisible.value = true
  }

async function HideFullSizePhoto()
{
  isVisible.value = false
}

async function DoNothing()
{

}

</script>

<template>
  <div>
    <img
      class="weather-photo-preview"
      :src="apiBaseUrl + '/api/Files/Download/' + props.photoPreviewId"
      alt="Фото погоды (превью)"
      @click="async() => await ShowFullSizePhoto()" />
  </div>


  <div v-if="isVisible">

    <!-- Popup lower layer -->
    <div class="popup-lower-layer">
    </div>

    <!-- Popup upper layer -->
    <div class="popup-upper-layer"
         @click="async() => await HideFullSizePhoto()">

      <div class="popup-main-image-section" @click.stop="DoNothing">
          <button
              class="popup-close-button"
              @click="async() => await HideFullSizePhoto()">

            <div class="cursor">
              <img class="popup-close-button-image" src="/images/close.webp" alt="Закрыть полноразмерное фото" />
            </div>

          </button>
          <img
            class="popup-image"
            :src="apiBaseUrl + '/api/Files/Download/' + props.fullSizePhotoId"
            alt="Полноразмерное фото погоды"/>

      </div>

    </div>

  </div>
</template>