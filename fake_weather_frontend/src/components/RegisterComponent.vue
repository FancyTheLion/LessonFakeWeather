<script setup>

import {onMounted, reactive } from "vue";
import {helpers, required} from "@vuelidate/validators";
import router from "@/router";
import useVuelidate from "@vuelidate/core";

  const apiBaseUrl = process.env.VUE_APP_API_URL

  const registerFormData = reactive({
    login: "",
    password: "",
    passwordRepeat: ""
  })

  const registerFormRules = {
    login: {
      $autoDirty: true,
      required,
      isLoginFree: helpers.withAsync(IsLoginFree)
    },

    password: {
      $autoDirty: true,
      required
    },

    passwordRepeat: {
      $autoDirty: true,
      required,
      sameAsPassword: CheckPasswordRepeat
    }
  }

  const registerFormValidator = useVuelidate(registerFormRules, registerFormData)

  onMounted(async () =>
  {
    await OnLoadAsync();
  })

  async function OnLoadAsync()
  {
    await registerFormValidator.value.$validate()
  }

  function CheckPasswordRepeat(passwordRepeat)
  {
    return passwordRepeat === registerFormData.password
  }

  async function DoRegisterAsync()
  {
    const response = await fetch(apiBaseUrl + "/api/Users/Register", {
      method: "POST",
      body: JSON.stringify({
        "request": {
          "login": registerFormData.login,
          "password": registerFormData.password
        }
      }),
      headers: { "Content-Type": "application/json" }
    })

    if (response.status !== 200)
    {
      alert("Неожиданная ошибка при регистрации пользователя!")
      return
    }

    const registrationResult = (await response.json()).registrationResult

    switch (registrationResult.result)
    {
      case 0:
        // Success!
        alert("Пользователь успешно создан, сейчас вы будете перенаправлены на страницу входа на сайт!")
        await router.push("/login")
        return

      case 1:
        // Login is taken
        alert("Данное имя пользователя занято! Выберите другое.")
        return

      case 2:
        // Generic error
        alert("Общая ошибка регистрации пользователя. Повторите попытку позже.")
        return

      default:
        alert("Неизвестный код ошибки пришёл с бекенда. Где-то баг!")
        return
    }
  }

// eslint-disable-next-line no-unused-vars
  async function IsLoginFree(login)
  {
    const response = await fetch(apiBaseUrl + "/api/Users/IsLoginTaken", {
      method: "POST",
      body: JSON.stringify({
        "checkData": {
          "login": login,
        }
      }),
      headers: { "Content-Type": "application/json" }
    })

    if (response.status !== 200)
    {
      alert("Ошибка при проверке занятости логина!")
      return false
    }

    return !(await response.json()).checkResult.isTaken
  }

</script>

<template>
  <div class="register-form-container"> <!--Я отвечаю за весь элемент регистрации-->

    <div class="text-centered"><!--Я центрирую текст внутри себя-->

      <div class="register-form"><!--Я рисую таблицу-->

        <div class="register-form-title"> <!--Я говорю о регистрации-->
          Регистрация
        </div>

        <div> <!--Я отсек для ввода инфы о логине-->

          <div>

            <div>
              Введите логин:
            </div>

            <div
                v-if="registerFormValidator.login.isLoginFree.$invalid"
                class="registration-login-taken-message">
              Этот логин занят
            </div>

          </div>

          <div>
            <input
                :class="(registerFormValidator.login.$error) ? 'registration-form-invalid-field' : 'registration-from-valid-field'"
                type="text"
                placeholder="Введите логин"
                v-model="registerFormData.login"
            />
          </div>

        </div>

        <div> <!--Я отсек для ввода инфы о пароле-->

          <div>
            Введите пароль:
          </div>

          <input
              :class="(registerFormValidator.password.$error) ? 'registration-form-invalid-field' : 'registration-from-valid-field'"
              type="password"
              placeholder="Введите пароль"
              v-model="registerFormData.password" />

        </div>

        <div> <!--Я отсек для повторного ввода инфы о пароле-->

          <div>
            Проверка пароля:
          </div>

          <input
              :class="(registerFormValidator.passwordRepeat.$error) ? 'registration-form-invalid-field' : 'registration-from-valid-field'"
              type="password"
              placeholder="Повторите пароль"
              v-model="registerFormData.passwordRepeat" />

        </div>

        <div> <!--Я - хлебушек (батонка регистрации)-->

          <button
              :disabled="registerFormValidator.$errors.length > 0"
              @click="async () => await DoRegisterAsync()">
            Зарегистрироваться
          </button>

        </div>

        <div> <!--Я отсек для показа вопроса о том, зареганы ли вы-->

          <div> <!--Я - хлебушек (вопросительная батонка)-->

              <a class="registration-bottom-link" href="/login" title="Вход на сайт">Вы уже зарегистрированы?</a>

          </div>

        </div>

      </div>

      <div>

        <div> <!--Я - ссылка-->

          <a class="login-bottom-link" href="//localhost:8080" title="Переход на главную странцу">Вернуться на главную? </a>

        </div>

      </div>

    </div>

  </div>

</template>

