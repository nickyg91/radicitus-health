<style scoped>
.mt-5 {
  margin-top: 5px;
}
</style>
<template>
  <section class="section">
    <div v-if="currentInitiative.healthInitiative != undefined" class="container">
      <div class="container">
        <div class="has-text-centered">
          <h1
            class="title is-center is-size-1"
          >Guild Goal: {{currentInitiative.healthInitiative.totalWeightLossGoal}}</h1>
        </div>
      </div>
      <div class="container">
        <div class="columns">
          <div class="column is-offset-one-third is-one-third has-text-centered">
            <b-progress
              type="is-success"
              size="is-large"
              :max="100"
              :value="currentInitiative.percentFinished == undefined ? 0 : currentInitiative.percentFinished"
              format="percent"
            ></b-progress>
          </div>
        </div>
      </div>
      <div class="has-text-centered container">
        <h1
          class="is-size-2"
        >{{currentInitiative.goal}} / {{currentInitiative.healthInitiative.totalWeightLossGoal}}</h1>
      </div>
      <div class="mt-5 has-text-centered container">
        <h1 class="is-size-1">Leaderboards</h1>
      </div>
      <div class="mt-5 container has-text-centered">
        <button
          @click="onCheckinClicked"
          class="button is-full is-large is-success"
        >Click here to submit weekly check-in</button>
      </div>
      <div class="mt-5 table-container">
        <div class="is-offset-one-third is-one-third">
          <table class="table table-light is-fullwidth">
            <thead>
              <tr>
                <th>Place</th>
                <th>Name</th>
                <th>Pounds Lost</th>
                <th>Goal %</th>
                <th>Points</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="entry in currentInitiative.leaderboard" v-bind:key="entry.name">
                <td>{{entry.place}}</td>
                <td>{{entry.name}}</td>
                <td>{{entry.poundsLost}}</td>
                <td>{{entry.percentTowardsGoal}}</td>
                <td>{{entry.points}}</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
    <div class="container has-text-centered" v-if="currentInitiative.healthInitiative == undefined">
      <h2 class="is-size-1">There is no current Health Initiative active!</h2>
    </div>
  </section>
</template>
<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import LeaderboardService from '@/services/leaderboard/leaderboard.service';
import CurrentHealthInitiative from '../models/current-initiative.model';
import Participant from '../models/participant.model';
import ParticipantsService from '@/services/leaderboard/participants.service';
import CheckIn from '@/components/CheckIn.vue';

@Component({
  components: {
    CheckIn
  }
})
export default class CurrentHealthInititative extends Vue {
  private service = new LeaderboardService();
  private participantsService = new ParticipantsService();
  public currentInitiative = new CurrentHealthInitiative();
  public participants = new Array<Participant>();
  async mounted() {
    this.currentInitiative = await this.service.getCurrentLeaderboard();
    this.participants = await this.participantsService.getParticipantsForInitiative(this.currentInitiative.healthInitiative.id);
  }

  public onCheckinClicked() {
    this.$buefy.modal.open({
      parent: this,
      component: CheckIn,
      props: {
        participants: this.participants,
        healthInititativeId: this.currentInitiative.healthInitiative.id,
        trapFocus: true
      }
    });
  }
}
</script>
