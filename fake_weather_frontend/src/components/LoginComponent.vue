<script setup>

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
          />

        </div>

        <div> <!--Я отсек для ввода инфы о пароле-->

          <div>
            Введите пароль:
          </div>

          <input
          />

        </div>

        <div> <!--Я отсек для запоминания логина-->

          Запомнить меня?

          <input type="checkbox">

        </div>

        <div> <!--Я - хлебушек (батонка для входа)-->

          <button>
            Я батон?
          </button>

        </div>

        <div> <!--Я отсек для показа вопроса о том, зареганы ли вы-->

          <div> <!--Я - вопросительная ссылка-->

            <a class="login-bottom-link" href="" title="Вход на сайт">Что то с паролем?</a>

          </div>

          <div> <!--Я - вопросительная ссылка-->

            <a class="login-bottom-link" href="/register" title="Вход на сайт">Зарегистрироваться?</a>

          </div>

        </div>

      </div>

    </div>

  </div>
</template>

