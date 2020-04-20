import HealthInitiative from './initiative.model';
import LeaderboardEntry from './leaderboard-entry.model';

export default class CurrentHealthInitiative {
    constructor() {
        this.healthInitiative = new HealthInitiative();
        this.leaderboard = new Array<LeaderboardEntry>();
    }
    public healthInitiative!: HealthInitiative;
    public leaderboard!: Array<LeaderboardEntry>;
    public goal!: number;
    public percentFinished!: number;
}
