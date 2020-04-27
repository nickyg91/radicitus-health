import CurrentHealthInitiative from '../../models/current-initiative.model';
import Axios from 'axios';

export default class LeaderboardService {

    public async getCurrentLeaderboard(): Promise<CurrentHealthInitiative> {
        const response = await Axios.get('api/health/initiatives/current', {
            headers: {
                'Content-Type': 'application/json'
            }
        });
        return response.data;
    }
}
