import Participant from './participant.model';

export default class HealthInitiative {
    public id?: number;
    public name?: string;
    public totalWeightLossGoal?: number;
    public startDateTime?: Date;
    public endDateTime?: Date;
    public participants?: Array<Participant>;
}
