import ParticipantLog from './participant-log.model';

export default class CheckInModel {
    public participantLog!: ParticipantLog;
    public healthInitiativeId!: number;
    public photoBase64!: string;
}
