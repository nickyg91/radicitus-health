import Axios from 'axios';
import CheckInModel from '../../models/check-in.model';

export default class ParticipantLogsService {
    public async saveLog(participantLog: CheckInModel) {
        await Axios.post('api/health/participants/logs/submit', participantLog, {
            headers: {
                'Content-Type': 'application/json'
            }
        });
    }
}
