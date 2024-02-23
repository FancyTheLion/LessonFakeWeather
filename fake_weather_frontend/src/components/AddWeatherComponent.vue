<script setup>

  import {reactive, defineEmits, onMounted, ref} from "vue";
  import {helpers, required} from "@vuelidate/validators";
  import useVuelidate from "@vuelidate/core";
  import LoadingSymbol from "@/components/LoadingSymbol.vue";

  const apiBaseUrl = process.env.VUE_APP_API_URL

  const isLoading = ref(true)

  const weatherValidationSettings = ref(null)

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
      isTemperatureValid: helpers.withAsync(IsTemperatureValid)
    },

    // Cloudiness
    cloudiness: {
      $autoDirty: true,
      required,
      isCloudinessValid: helpers.withAsync(IsCloudinessValid)
    },

    // Humidity
    humidity: {
      $autoDirty: true,
      required,
      isHumidityValid: helpers.withAsync(IsHumidityValid)
    },

    // Pressure
    pressure: {
      $autoDirty: true,
      required,
      isPressureValid: helpers.withAsync(IsPressureValid)
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

    // Loading weather validation settings
    weatherValidationSettings.value = (await (await fetch(apiBaseUrl + "/api/Weather/ValidationSettings", {
      method: 'GET'
    })).json()).weatherValidationSettings

    await addWeatherFormValidator.value.$validate()

    isLoading.value = false
  }

  // Method to validate temperature
  async function IsTemperatureValid(temperature)
  {
    if (temperature < weatherValidationSettings.value.lowestPossibleTemperature)
    {
      return false;
    }

    if (temperature > weatherValidationSettings.value.highestPossibleTemperature)
    {
      return false;
    }

    return true;
  }

  // Method to validate cloudiness
  async function IsCloudinessValid(cloudiness)
  {
    if (cloudiness < weatherValidationSettings.value.lowestPossibleCloudiness)
    {
      return false;
    }

    if (cloudiness > weatherValidationSettings.value.highestPossibleCloudiness)
    {
      return false;
    }

    return true;
  }

  // Method to validate humidity
  async function IsHumidityValid(humidity)
  {
    if (humidity < weatherValidationSettings.value.lowestPossibleHumidity)
    {
      return false;
    }

    if (humidity > weatherValidationSettings.value.highestPossibleHumidity)
    {
      return false;
    }

    return true;
  }

// Method to validate pressure
async function IsPressureValid(pressure)
{
  if (pressure < weatherValidationSettings.value.lowestPossiblePressure)
  {
    return false;
  }

  if (pressure > weatherValidationSettings.value.highestPossiblePressure)
  {
    return false;
  }

  return true;
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
  <LoadingSymbol v-if="isLoading" />

  <div v-if="!isLoading">
    <div class="add-weather left">

      <!-- Date and time -->
      <div>
        <div class="centered form-input-caption">
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
        <div class="centered form-input-caption">
          Введите температуру:
        </div>

        <input
            type="number"
            :class="(addWeatherFormValidator.temperature.$error)?'form-field-with-error':'form-field-without-error'"
            v-model="addWeatherData.temperature"
        />

        [{{ weatherValidationSettings.lowestPossibleTemperature }}; {{weatherValidationSettings.highestPossibleTemperature}}]
      </div>

      <!-- Cloudiness -->
      <div>
        <div class="centered form-input-caption">
          Введите облачность:
        </div>

        <input
            type="number"
            :class="(addWeatherFormValidator.cloudiness.$error)?'form-field-with-error':'form-field-without-error'"
            v-model="addWeatherData.cloudiness"
        />

        [{{ weatherValidationSettings.lowestPossibleCloudiness }}; {{weatherValidationSettings.highestPossibleCloudiness}}]
      </div>

      <div>
        <div class="centered form-input-caption">
          Введите влажность:
        </div>

        <input
            type="number"
            :class="(addWeatherFormValidator.humidity.$error)?'form-field-with-error':'form-field-without-error'"
            v-model="addWeatherData.humidity"
        />

        [{{ weatherValidationSettings.lowestPossibleHumidity }}; {{weatherValidationSettings.highestPossibleHumidity}}]
      </div>

      <div>
        <div class="centered form-input-caption">
          Введите атмосферное давление:
        </div>

        <input
            type="number"
            :class="(addWeatherFormValidator.pressure.$error)?'form-field-with-error':'form-field-without-error'"
            v-model="addWeatherData.pressure"
        />

        [{{ weatherValidationSettings.lowestPossiblePressure }}; {{weatherValidationSettings.highestPossiblePressure}}]
      </div>

      <!-- Add button -->
      <div>
        <button
            type="button"
            :disabled="addWeatherFormValidator.$errors.length > 0"
            @click="async() => await SendWeatherToBackend()">
          Добавить погоду
        </button>
      </div>

    </div>

  </div>

</template>