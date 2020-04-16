import Axios from 'axios';
import HealthInitiative from '../../models/initiative.model';
export default class AdminService {
    public async getInitiatives(): Promise<Array<HealthInitiative>> {
        const response = await Axios.get('api/health/initiatives/all', {
            headers: {
                'Content-Type': 'application/json'
            }
        });
        return response.data;
    }

    public async createInitative(initiative: HealthInitiative): Promise<void> {
        console.log(initiative);
        await Axios.post('api/health/initiatives/create', initiative, {
            headers: {
                'Content-Type': 'application/json'
            }
        });
    }
}
