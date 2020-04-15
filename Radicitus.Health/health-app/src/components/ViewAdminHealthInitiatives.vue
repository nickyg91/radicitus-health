<template>
  <div>
    <div class="has-text-centered" v-if="healthInitiatives.length < 1">
      <h2 class="is-size-2">No Health Initiatives have been added!.</h2>
    </div>
    <div v-if="healthInitiatives.length > 0">
      <div class="columns">
        <div class="column is-one-third"></div>
        <div class="column">
          <table class="is-bordered is-fullwidth is-hoverable table table-light">
            <thead>
              <tr>
                <th>Name</th>
                <th># of Participants</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="initative in healthInitiatives" v-bind:key="initative.id">
                <td>{{initative.name}}</td>
                <td class="has-text-right">{{initative.participants.length}}</td>
              </tr>
            </tbody>
          </table>
        </div>
        <div class="column is-one-third"></div>
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
