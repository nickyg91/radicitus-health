import Axios from 'axios';
import Participant from '../../models/participant.model';
import UpdateParticipants from '../../models/update-participants.model';

export default class ParticipantsService {
    public async getParticipantsForInitiative(id: number): Promise<Array<Participant>> {
        const data = ((await (Axios.get(`api/participants/${id}`))).data) as Array<Participant>;
        return data;
    }

    public async saveChangesToParticipants(id: number, participants: UpdateParticipants): Promise<void> {
        await (Axios.patch(`api/participants/update/${id}`, participants, {
            headers: {
                'content-type': 'application/json'
            }
        }));
    }
}
