import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import App from './App.vue'
import EventsList from './views/EventsList.vue'
import EventDetails from './views/EventDetails.vue'

import './style.css'

const routes = [
  { path: '/', component: EventsList },
  { path: '/events/:id', component: EventDetails, props: true }
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

createApp(App)
  .use(router)
  .mount('#app')