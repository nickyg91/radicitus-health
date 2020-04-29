<style scoped>
.max-height {
  height: 500px;
}
</style>
<template>
  <section>
    <b-tabs :position="'is-left'" vertical :size="'is-medium'" :type="'is-boxed'">
      <b-tab-item
        v-for="participant in participants"
        :key="participant.id"
        v-bind:label="participant.name"
      >
        <div v-if="participant.participantLogs.length > 0">
          <b-steps :size="'is-small'" :position="'bottom'" :vertical="true">
            <b-step-item
              v-for="log in participant.participantLogs"
              :key="log.id"
              v-bind:label="log.dateSubmitted | date"
            >
              <div class="columns max-height">
                <div class="column is-offset-one-fifth">
                  <div class="card">
                    <div class="card-image" v-if="log.photoBase64">
                      <figure class="image is-square">
                        <img v-bind:src="'data:image/png;base64, ' + log.photoBase64 " alt />
                      </figure>
                    </div>
                    <div class="card-content">
                      <div class="content">
                        <h3>Current Weight</h3>
                        <span>{{log.currentWeight}} lbs</span>
                      </div>
                      <div class="content">
                        <h3>Points</h3>
                        <span>{{log.points}}</span>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="column"></div>
              </div>
            </b-step-item>
          </b-steps>
        </div>
        <div class="has-text-centered" v-else>
          <h2 class="is-size-2">No logs entered</h2>
        </div>
      </b-tab-item>
    </b-tabs>
  </section>
</template>
<script lang="ts">
import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import Participant from '../models/participant.model';
@Component
export default class InitiativeLogs extends Vue {

  @Prop({ default: () => new Array<Participant>() })
  public participants!: Array<Participant>;
}
</script>
