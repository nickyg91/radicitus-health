import Participant from './participant.model';

export default class UpdateParticipants {
    constructor() {
        this.updatedParticipants = new Array<Participant>();
        this.removedParticipants = new Array<Participant>();
    }
    updatedParticipants!: Participant[];
    removedParticipants!: Participant[];
}
