<script setup>
import {onMounted, reactive} from "vue";
  import {required} from "@vuelidate/validators";
  import useVuelidate from "@vuelidate/core";
  import {AuthLogCreatureIn} from "@/js/Auth";

  const logInFormData = reactive({
    login: "",
    password: "",
    isRememberMe: false
  })

  const logInFormRules = {
    login: {
      $autoDirty: true,
      required
    },
    password: {
      $autoDirty: true,
      required
    }
  }

  const logInFormValidator = useVuelidate(logInFormRules, logInFormData)

  onMounted(async () =>
  {
    await OnLoad();
  })

  async function OnLoad()
  {
    await logInFormValidator.value.$validate()
  }

  async function DoLogInAsync() {
    await AuthLogCreatureIn(logInFormData.login, logInFormData.password)
  }

</script>

<template>
  <div class="login-form-container"> <!--Я отвечаю за весь элемент залогинивания-->

    <div class="text-centered"><!--Я центрирую текст внутри себя-->

      <div class="login-form"><!--Я рисую таблицу-->

        <div class="login-form-title"> <!--Я говорю о залогинивании-->
          Вход
        </div>

        <div> <!--Я отсек для ввода инфы о логине-->

          <div>
            Введите логин:
          </div>

          <input
              :class="(logInFormValidator.login.$error) ? 'login-form-invalid-field' : 'login-from-valid-field'"
              type="text"
              placeholder="Имя пользователя"
              v-model="logInFormData.login"
          />

        </div>

        <div> <!--Я отсек для ввода инфы о пароле-->

          <div>
            Введите пароль:
          </div>

          <input
              :class="(logInFormValidator.password.$error) ? 'login-form-invalid-field' : 'login-from-valid-field'"
              type="password"
              placeholder="Пароль"
              v-model="logInFormData.password"
          />

        </div>

        <div> <!--Я отсек для запоминания логина-->

          Запомни меня..

          <input
              type="checkbox"
              v-model="logInFormData.isRememberMe"
          />

        </div>

        <div> <!--Я - хлебушек (батонка для входа)-->

          <button
            type="button"
            :disabled="logInFormValidator.$errors.length > 0"
            @click="async () => await DoLogInAsync()">
            Войти
          </button>

        </div>

        <div> <!--Я отсек для показа вопроса о том, зареганы ли вы-->

          <div> <!--Я - вопросительная ссылка-->

            <a class="login-bottom-link" href="" title="Что то случилось с паролем?">Что то с паролем?</a>

          </div>

          <div> <!--Я - вопросительная ссылка-->

            <a class="login-bottom-link" href="/register" title="Хотите зарегистрироваться?">Зарегистрироваться?</a>

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

