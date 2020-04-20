<template>
  <section class="section">
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
            :value="currentInitiative.percentFinished"
            show-value="true"
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
    <div class="has-text-centered container">
      <h1 class="is-size-1">Leaderboards</h1>
    </div>
    <div class="container">
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
          <tbody></tbody>
        </table>
      </div>
    </div>
  </section>
</template>
<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import LeaderboardService from '@/services/leaderboard/leaderboard.service';
import CurrentHealthInitiative from '../models/current-initiative.model';
@Component
export default class CurrentHealthInititative extends Vue {
  private service = new LeaderboardService();
  public currentInitiative = new CurrentHealthInitiative();
  async mounted() {
    this.currentInitiative = await this.service.getCurrentLeaderboard();
  }
}
</script>
