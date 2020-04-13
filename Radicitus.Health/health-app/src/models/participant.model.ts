import ParticipantLog from './participant-log.model';

export default class Participant {
    public id!: number;
    public healthInitiativeId!: number;
    public name!: string;
    public individualWeightLossGoal!: number;
    public participantLogs!: Array<ParticipantLog>;
}
