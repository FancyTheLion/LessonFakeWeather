<script setup>

</script>

<template>
  <div class="full-width-flex-element-register-or-login"> <!--Я отвечаю за весь элемент регистрации-->

    <div class="text-centered"><!--Я центрирую текст внутри себя-->

      <div class="user-registration-window"><!--Я рисую таблицу-->

        <div class="font-registration"> <!--Я говорю о регистрации-->
          Регистрация
        </div>

        <div> <!--Я отсек для ввода инфы о логине-->

          <div>
            Введите логин:
          </div>

          <input
          />

        </div>

        <div> <!--Я отсек для ввода инфы о пароле-->

          <div>
            Введите пароль:
          </div>

          <input
          />

        </div>

        <div> <!--Я отсек для повторного ввода инфы о пароле-->

          <div>
            Проверка пароля:
          </div>

          <input
          />

        </div>

        <div> <!--Я - хлебушек (батонка регистрации)-->

          <button>
            Зарегистрироваться
          </button>

        </div>

        <div> <!--Я отсек для показа вопроса о том, зареганы ли вы-->

          <div> <!--Я - хлебушек (вопросительная батонка)-->

              <a class="registration-bottom-link" href="/login" title="Вход на сайт">Вы уже зарегистрированы?</a>

          </div>

        </div>

      </div>

    </div>



  </div>

</template>

