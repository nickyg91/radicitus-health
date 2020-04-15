import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import buefy from 'buefy'
import Axios from 'axios'
import '@fortawesome/fontawesome-free/css/all.css'
Vue.use(buefy);
Vue.config.productionTip = false
Axios.defaults.headers.common['content-type'] = 'application/json'
new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
