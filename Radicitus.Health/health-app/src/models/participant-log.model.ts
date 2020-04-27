export default class ParticipantLog {
    public id!: number;
    public participantId!: number;
    public healthInitiativeId!: number;
    public currentWeight!: number;
    public photoBase64?: string;
    public dateSubmitted!: Date;
    public points!: number;
}
