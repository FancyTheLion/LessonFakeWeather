import { createRouter, createWebHistory } from 'vue-router'
import HomeView from "@/views/HomeView.vue";
import RegisterView from "@/views/RegisterView.vue";
import LoginView from "@/views/LoginView.vue";



const routes = [
  // Main page
  {
    path: '/',
    name: 'homePage',
    component: HomeView
  },

  // Login
  {
    path: '/login',
    name: 'login',
    component: LoginView
  },

  // Registration
  {
    path: '/register',
    name: 'register',
    component: RegisterView
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
