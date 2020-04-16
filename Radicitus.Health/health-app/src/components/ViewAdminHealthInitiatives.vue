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
              </tr>
            </thead>
            <tbody>
              <tr v-for="initiative in healthInitiatives" v-bind:key="initiative.id">
                <td>{{initiative.name}}</td>
                <td>{{initiative.participants.length}}</td>
                <td>{{initiative.startDateTime | date}}</td>
                <td>{{initiative.endDateTime | date}}</td>
                <td>{{initiative.totalWeightLossGoal}} lbs.</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator'
import HealthInitiative from '../models/initiative.model';
@Component
export default class ViewAdminHealthInitiatives extends Vue {
  @Prop({ default: () => { new Array<HealthInitiative>() } })
  healthInitiatives!: Array<HealthInitiative>;
}   
</script>
