import Participant from './participant.model';

export default class HealthInitiative {
    constructor() {
        this.participants = new Array<Participant>();
    }
    public id!: number;
    public name?: string;
    public totalWeightLossGoal?: number;
    public startDateTime?: Date;
    public endDateTime?: Date;
    public participants?: Array<Participant>;
}
