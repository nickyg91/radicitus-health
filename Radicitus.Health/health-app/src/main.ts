import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import buefy from 'buefy'
import Axios from 'axios'
import '@fortawesome/fontawesome-free/css/all.css'
import moment from 'moment';

Vue.use(buefy, {
  defaultIconPack: 'fas'
});

Vue.filter('date', (value: string) => {
  return moment(value).format('MM/DD/YYYY');
});

Vue.config.productionTip = false
Axios.defaults.headers.common['content-type'] = 'application/json'
new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
