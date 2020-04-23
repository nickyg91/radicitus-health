import Axios from 'axios';
import Participant from '../../models/participant.model';

export default class ParticipantsService {
    public async getParticipantsForInitiative(id: number): Promise<Array<Participant>> {
        const data = ((await (Axios.get(`api/participants/${id}`))).data) as Array<Participant>;
        return data;
    }
}
