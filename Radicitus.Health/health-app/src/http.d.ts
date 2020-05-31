import Vue, { VueConstructor } from 'vue';
import { AxiosInstance } from 'axios';
declare module 'vue/types/vue' {
    interface Vue {
        $http: AxiosInstance;
    }
    interface VueConstructor {
        $http: AxiosInstance;
    }
}

declare module 'vue/types/options' {
    interface ComponentOptions<V extends Vue> {
        $http?: AxiosInstance;
    }
}
