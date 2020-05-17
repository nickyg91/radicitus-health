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

    public async getAllTags(): Promise<Array<string>> {
        const response = await this._http.get<Array<string>>('api/resource/tags');
        return response.data;
    }

    public async submitResource(resource: ResourceItem): Promise<void> {
        await this._http.post('api/resource/submit', resource);
    }

    public async filterResources(tags: Array<string>): Promise<Array<ResourceItem>> {
        const response = await this._http.post('api/resource/filter', tags);
        return response.data;
    }

    public async remove(guid: string): Promise<void> {
        await this._http.delete(`api/resource/remove/${guid}`);
    }
}
