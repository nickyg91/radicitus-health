import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import buefy from 'buefy'
import axios from 'axios'
import '@fortawesome/fontawesome-free/css/all.css'
import moment from 'moment';


Vue.use(buefy, {
  defaultIconPack: 'fas'
});

Vue.filter('date', (value: string) => {
  return moment(value).format('MM/DD/YYYY');
});

Vue.config.productionTip = false
const axiosConfig = axios.create({
  headers: {
    'content-type': 'application/json'
  }
})
Vue.prototype.$http = axiosConfig;
new Vue({
  router,
  store,
  render: h => h(App),
}).$mount('#app')
