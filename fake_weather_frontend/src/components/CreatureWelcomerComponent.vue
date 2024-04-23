<script setup>
// OnMounted hook
import {onMounted, ref} from "vue";
import {AuthGetCreatureLogin, AuthIsCreatureLoggedIn, AuthLogCreatureOut} from "@/js/Auth";

  const isLoggedIn = ref(false)
  const creatureLogin = ref(null)

  // Called when page is mounted
  onMounted(async () =>
  {
    await OnLoad();
  })

  // Called when component is loaded
  async function OnLoad()
  {
    isLoggedIn.value = await AuthIsCreatureLoggedIn()

    if (isLoggedIn.value)
    {
      creatureLogin.value = await AuthGetCreatureLogin()
    }
  }

  // Log out from site
  async function DoLogOut()
  {
    await AuthLogCreatureOut()
  }

</script>

<template>
  <!-- For not logged-in creatures -->
  <div v-if="!isLoggedIn">

    <a class="link-on-home-page" href="/login" title="Вход на сайт">Войти</a>
    |
    <a class="link-on-home-page" href="/register" title="Регистрация на сайте">Зарегистрироваться</a>

  </div>

  <!-- For logged-in creatures -->
  <div v-if="isLoggedIn">

    Привет, {{ creatureLogin }}!
    |
    <span
        class="pseudolink"
        @click="async () => await DoLogOut()">
      Выйти
    </span>

  </div>
</template>
