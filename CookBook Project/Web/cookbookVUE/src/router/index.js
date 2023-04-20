import { createRouter, createWebHistory } from 'vue-router'
import CookBookHome from '../views/CookBookHome.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: CookBookHome,
    },
    {
      path: '/profil/:user',
      name: 'profil',
      scrollBehavior () {
        return { top: 0 }
      },
      component: () => import('../views/ProfileView.vue'),
    },
    {
      path: '/profil/:user/edit/:id',
      name: 'edit',
      component: () => import('../views/EditRecipeView.vue')
    },
    {
      path: '/recept/:id',
      name:'modify',
      component: () => import('../views/RecipeDisplay.vue')
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('../views/LoginView.vue')
    },
    {
      path: '/recept/:id',
      name: 'recept',
      scrollBehavior () {
        return { top: 0 }
      },
      component: () => import('../views/RecipeDisplay.vue')
    },
    {
      path: '/:pathMatch(.*)*',
      name: 'notFound',
      component: () => import('../views/NotFound.vue')
    }
  ]
})

export default router
