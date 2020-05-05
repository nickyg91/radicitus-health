import { AxiosInstance } from 'axios';
import ResourceItem from '../../models/resource.model';

export default class ResourcesService {
    private _http: AxiosInstance;
    constructor(axiosInstance: AxiosInstance) {
        this._http = axiosInstance;
    }

    public async getAllResources(): Promise<Array<ResourceItem>> {
        const response = await this._http.get<ResourceItem[]>('api/resource/all');
        return response.data;
    }
}
