import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import Home from '@/views/Home.vue'
import PointSystem from '@/views/PointSystem.vue';
import Admin from '@/views/Admin.vue';
import Resources from '@/views/Resources.vue';

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
  {
    path: '/',
    name: 'Home',
    redirect: '/players'
  },
  {
    path: '/players',
    name: 'Players',
    component: Home
  },
  {
    path: '/points',
    name: 'Points',
    component: PointSystem
  },
  {
    path: '/admin',
    name: 'Admin',
    component: Admin
  },
  {
    path: '/resources',
    name: 'Resources',
    component: Resources
  }
]

const router = new VueRouter({
  routes
})

export default router
