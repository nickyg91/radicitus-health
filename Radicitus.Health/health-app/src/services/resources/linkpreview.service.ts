import { AxiosInstance } from 'axios';
import LinkPreview from '../../models/link-preview.model';

export default class LinkPreviewService {
    constructor(private http: AxiosInstance) {

    }

    public async getLinkPreview(guid: string) {
        return (await this.http.get<LinkPreview>(`api/linkpreview/find/${guid}`)).data
    }
}
