<template>
  <div>
    <div class="has-text-centered" v-if="healthInitiatives.length < 1">
      <h2 class="is-size-2">No Health Initiatives have been added!</h2>
    </div>
    <div v-if="healthInitiatives.length > 0">
      <div class="columns">
        <div class="column is-centered">
          <table class="is-fullwidth is-hoverable table table-light">
            <thead>
              <tr>
                <th>Name</th>
                <th># of Participants</th>
                <th>Start Date</th>
                <th>End Date</th>
                <th>Total Weight Loss Goal</th>
                <th class="width-zero"></th>
                <th class="width-zero"></th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="initiative in healthInitiatives" v-bind:key="initiative.id">
                <td>{{initiative.name}}</td>
                <td>{{initiative.participants.length}}</td>
                <td>{{initiative.startDateTime | date}}</td>
                <td>{{initiative.endDateTime | date}}</td>
                <td>{{initiative.totalWeightLossGoal}} lbs.</td>
                <td>
                  <button @click="viewLogs(initiative.id)" class="button is-outlined is-primary">
                    <span class="icon">
                      <i class="fas fa-eye"></i>
                    </span>
                    <span>View Logs</span>
                  </button>
                </td>
                <td>
                  <button class="button is-outlined is-info">
                    <span class="icon">
                      <i class="fas fa-edit"></i>
                    </span>
                    <span>Edit</span>
                  </button>
                </td>
              </tr>
            </tbody>
          </table>
          <b-loading :active.sync="isLoading"></b-loading>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator'
import HealthInitiative from '../models/initiative.model'
import Participant from '@/models/participant.model';
import ParticipantsService from '../services/leaderboard/participants.service';
@Component
export default class ViewAdminHealthInitiatives extends Vue {
  @Prop({ default: () => { new Array<HealthInitiative>() } })
  healthInitiatives!: Array<HealthInitiative>;
  private participantsService = new ParticipantsService();
  public participantLogs!: Array<Participant>;
  public isLoading = false;
  public isViewingLogs = false;
  public async viewLogs(id: number) {
    this.isLoading = true;
    try {
      this.participantLogs = await this.participantsService.getParticipantsForInitiative(id);
      this.isViewingLogs = true;
      this.$emit('viewing-logs', { 'isViewingLogs': this.isViewingLogs, 'participants': this.participantLogs });
    } catch (exception) {
      this.$buefy.notification.open({
        message: 'An error occurred while getting the logs for this initiative.',
        type: 'is-danger'
      });
    }
    this.isLoading = false;
  }
}   
</script>
