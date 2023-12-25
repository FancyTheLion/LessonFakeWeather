import { createRouter, createWebHistory } from 'vue-router'
import CurrentWeatherView from "@/views/CurrentWeatherView.vue";

const routes = [
  // Main page with current weather
  {
    path: '/',
    name: 'currentWeather',
    component: CurrentWeatherView
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
