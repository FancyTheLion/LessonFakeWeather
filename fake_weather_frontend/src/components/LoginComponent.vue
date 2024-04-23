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
    await AuthLogCreatureIn(logInFormData.login, logInFormData.password, logInFormData.isRememberMe)
  }

</script>

<template>
  <div class="login-form-container"> <!--I am responsible for the entire login element-->

    <div class="text-centered"><!--I center the text within myself -->

      <div class="login-form"><!--I'm drawing a table-->

        <div class="login-form-title"> <!--I'm talking about logging in-->
          Вход
        </div>

        <div> <!--I am the section for entering login information-->

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

        <div> <!--I have a compartment for entering information or password-->

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

        <div> <!--I have a compartment for remembering login-->

          Запомни меня..

          <input
              type="checkbox"
              v-model="logInFormData.isRememberMe"
          />

        </div>

        <div>

          <button
            type="button"
            :disabled="logInFormValidator.$errors.length > 0"
            @click="async () => await DoLogInAsync()">
            Войти
          </button>

        </div>

        <div> <!--I have a section where it asks if you are registered.-->

          <div>

            <a class="login-bottom-link" href="" title="Что то случилось с паролем?">Что то с паролем?</a>

          </div>

          <div>

            <a class="login-bottom-link" href="/register" title="Хотите зарегистрироваться?">Зарегистрироваться?</a>

          </div>

        </div>

      </div>

      <div>

        <div>

          <a class="login-bottom-link" href="//localhost:8080" title="Переход на главную странцу">Вернуться на главную? </a>

        </div>

      </div>

    </div>

  </div>
</template>

