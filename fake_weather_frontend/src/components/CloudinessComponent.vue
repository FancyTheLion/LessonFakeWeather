<script setup>
  import {defineProps} from "vue";

  const props = defineProps({
    cloudiness: Number
  })

</script>

<template>
  <div>Облачность: {{ props.cloudiness }}</div>
</template>

