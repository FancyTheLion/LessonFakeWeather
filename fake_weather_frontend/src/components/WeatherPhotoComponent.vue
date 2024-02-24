<script setup>
  import {defineProps} from "vue";

  const apiBaseUrl = process.env.VUE_APP_API_URL

  const props = defineProps({
    photoId: String
  })
</script>

<template>
  <img
      class="weather-photo-preview"
      :src="apiBaseUrl + '/api/Files/Download/' + props.photoId"
      alt="Фото погоды" />
</template>