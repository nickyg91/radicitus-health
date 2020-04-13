import Participant from './participant.model';

export default class HealthInitiative {
    public id!: number;
    public name!: string;
    public totalWeightLossGoal!: number;
    public isCurrent!: boolean;
    public startDateTime!: Date;
    public participants!: Array<Participant>;
}
