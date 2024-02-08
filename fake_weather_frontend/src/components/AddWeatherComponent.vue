<script setup>

import {reactive, defineEmits, onMounted} from "vue";
import {maxValue, minValue, required} from "@vuelidate/validators";
  import useVuelidate from "@vuelidate/core";

  const apiBaseUrl = process.env.VUE_APP_API_URL

  // Form data
  const addWeatherData = reactive({
    dateTime: new Date(),
    temperature: 0,
    cloudiness: 0,
    humidity: 0,
    pressure: 0
  })

  // Validation rules
  const addWeatherFormRules = {

    // Date and time
    dateTime: {
      $autoDirty: true,
      required
    },

    // Temperature
    temperature: {
      $autoDirty: true,
      required,
      minValueValue: minValue(-50),
      maxValueValue: maxValue(50)
    },

    // Cloudiness
    cloudiness: {
      $autoDirty: true,
      required,
      minValueValue: minValue(0),
      maxValueValue: maxValue(100)
    },

    // Humidity
    humidity: {
      $autoDirty: true,
      required,
      minValueValue: minValue(0),
      maxValueValue: maxValue(100)
    },

    // Pressure
    pressure: {
      $autoDirty: true,
      required,
      minValueValue: minValue(700),
      maxValueValue: maxValue(800)
    }
  }

  const addWeatherFormValidator = useVuelidate(addWeatherFormRules, addWeatherData)

  const emit = defineEmits(["weatherAdded"])

  // OnMounted hook
  onMounted(async () =>
  {
    await OnLoad();
  })

  // Called when component is loaded
  async function OnLoad()
  {
    addWeatherData.dateTime = null

    await addWeatherFormValidator.value.$validate()
  }

  async function SendWeatherToBackend()
  {
    const response = await fetch(apiBaseUrl + "/api/Weather/Add", {
      method: "POST",
      body: JSON.stringify({
        "weatherToAdd": {
          "timestamp": new Date(addWeatherData.dateTime).toISOString(),
          "temperature": addWeatherData.temperature,
          "cloudiness": addWeatherData.cloudiness,
          "humidity": addWeatherData.humidity,
          "pressure": addWeatherData.pressure
        }
      }),
      headers: { "Content-Type": "application/json" }
    })

    if (response.status === 200)
    {
      await OnWeatherAdded()
    }
    else
    {
      alert("Ошибка добавления погоды")
    }
  }

  // This method gets called when the new weather is added successfully
  async function OnWeatherAdded()
  {
    // Clearing the form
    addWeatherData.dateTime = null
    addWeatherData.temperature = 0
    addWeatherData.cloudiness = 0
    addWeatherData.humidity = 0
    addWeatherData.pressure = 0

    emit("weatherAdded")
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
        :class="(addWeatherFormValidator.dateTime.$error)?'form-field-with-error':'form-field-without-error'"
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
        :class="(addWeatherFormValidator.temperature.$error)?'form-field-with-error':'form-field-without-error'"
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
        :class="(addWeatherFormValidator.cloudiness.$error)?'form-field-with-error':'form-field-without-error'"
        v-model="addWeatherData.cloudiness"
    />
  </div>

  <div>
    <div>
      Введите влажность (в процентах):
    </div>

    <input
        type="number"
        :class="(addWeatherFormValidator.humidity.$error)?'form-field-with-error':'form-field-without-error'"
        v-model="addWeatherData.humidity"
    />
  </div>

  <div>
    <div>
      Введите атмосферное давление (в мм ртутного столба):
    </div>

    <input
        type="number"
        :class="(addWeatherFormValidator.pressure.$error)?'form-field-with-error':'form-field-without-error'"
        v-model="addWeatherData.pressure"
    />
  </div>


  <!-- Add button -->
  <button
    type="button"
    :disabled="addWeatherFormValidator.$errors.length > 0"
    @click="async() => await SendWeatherToBackend()">
    Добавить погоду
  </button>

</template>