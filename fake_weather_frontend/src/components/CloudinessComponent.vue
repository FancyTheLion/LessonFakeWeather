<script setup>
import {defineProps, ref} from "vue";

  const iconsBaseAddress = "images/"

  const props = defineProps({
    cloudiness: Number
  })

  const iconAddress = ref("")

  if (props.cloudiness < 0)
  {
    throw new Error("Cloudiness must be at least 0!")
  }
  else if (props.cloudiness >= 0 && props.cloudiness < 30)
  {
    iconAddress.value = iconsBaseAddress + "cloudiness-sunny.webp"
  }
  else if (props.cloudiness >= 30 && props.cloudiness < 80)
  {
    iconAddress.value = iconsBaseAddress + "cloudiness-partly.webp"
  }
  else if (props.cloudiness >= 80 && props.cloudiness <= 100)
  {
    iconAddress.value = iconsBaseAddress + "cloudiness-full.webp"
  }
  else
  {
    throw new Error("Cloudiness must be less than 100!");
  }
</script>

<template>
  <div class="centered">
    <img class="cloudiness-icon" :src="iconAddress" alt="Cloudiness icon" />
  </div>

  <div>Облачность: {{ props.cloudiness }}</div>
</template>

