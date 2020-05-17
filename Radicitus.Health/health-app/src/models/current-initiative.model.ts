import HealthInitiative from './initiative.model';
import LeaderboardEntry from './leaderboard-entry.model';

export default class CurrentHealthInitiative {
    constructor() {
        this.healthInitiative = new HealthInitiative();
        this.leaderboard = new Array<LeaderboardEntry>();
    }
    public healthInitiative!: HealthInitiative;
    public leaderboard!: Array<LeaderboardEntry>;
    public poundsLost!: number;
    public percentFinished!: number;
    public canSubmitPicture!: boolean;
}
