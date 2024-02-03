<script setup>

  import {reactive, defineEmits} from "vue";

  const apiBaseUrl = process.env.VUE_APP_API_URL

  // Form data
  const addWeatherData = reactive({
    dateTime: new Date(),
    temperature: 0,
    cloudiness: 0
  })

  const emit = defineEmits(["weatherAdded"])

  async function SendWeatherToBackend()
  {
    const response = await fetch(apiBaseUrl + "/api/Weather/Add", {
      method: "POST",
      body: JSON.stringify({
        "weatherToAdd": {
          "timestamp": new Date(addWeatherData.dateTime).toISOString(),
          "temperature": addWeatherData.temperature,
          "cloudiness": addWeatherData.cloudiness
        }
      }),
      headers: { "Content-Type": "application/json" }
    })

    if (response.status === 200)
    {
      emit("weatherAdded")
    }
    else
    {
      alert("Ошибка добавления погоды")
    }
  }

</script>

<template>
  <!-- Date and time -->
  <div>
    <div>
      Введите дату и время измерения погоды:
    </div>

    <input
        type="datetime-local"
        v-model="addWeatherData.dateTime"
    />

  </div>

  <!-- Temperature -->
  <div>
    <div>
      Введите температуру:
    </div>

    <input
        type="number"
        v-model="addWeatherData.temperature"
    />
  </div>

  <!-- Cloudiness -->
  <div>
    <div>
      Введите облачность (в процентах):
    </div>

    <input
        type="number"
        v-model="addWeatherData.cloudiness"
    />
  </div>

  <!-- Add button -->
  <button
    type="button"
    @click="async() => await SendWeatherToBackend()">
    Добавить погоду
  </button>

</template>