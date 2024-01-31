<script setup>

  import {reactive} from "vue";

  const apiBaseUrl = process.env.VUE_APP_API_URL

  // Form data
  const addWeatherData = reactive({
    dateTime: 0,
    temperature: 0,
    cloudiness: 0
  })

  async function SendWeatherToBackend()
  {
    const response = await fetch(apiBaseUrl + "/api/Weather/Add", {
      method: 'POST',
      body: JSON.stringify({
        "weatherToAdd": {
          "timestamp": addWeatherData.dateTime,
          "temperature": addWeatherData.temperature,
          "cloudiness": addWeatherData.cloudiness
        }
      }),
      headers: { 'Content-Type': 'application/json' }
    })

    if (response.status === 200)
    {
      alert("Погода добавлена!")
    }
  }

</script>

<template>
  Тут будет добавление новой погоды в базу:

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