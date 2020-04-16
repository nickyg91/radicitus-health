<style>
.table-max-height {
  overflow-y: scroll;
  max-height: 300px;
}
.has-margin-top-5 {
  margin-top: 5px;
}
</style>
<template>
  <div class="box">
    <div>
      <div class="title">
        <h3>Add Participants</h3>
      </div>
      <div class="columns">
        <div class="column">
          <b-field label="Name">
            <b-input
              validation-message="Name is required!"
              placeholder="Name"
              v-model="participant.name"
              type="text"
              required
              min="5"
              max="256"
            ></b-input>
          </b-field>
          <b-field label="Goal">
            <b-input
              validation-message="Name is required!"
              placeholder="Goal"
              v-model.number="participant.individualWeightLossGoal"
              type="text"
              required
              min="5"
              max="256"
            ></b-input>
          </b-field>
          <div class="container has-text-right">
            <button
              :disabled="!validParticipant()"
              @click="addParticipant"
              class="button is-info is-light"
            >
              <span class="icon">
                <i class="fas fa-plus"></i>
              </span>
              <span>Add</span>
            </button>
          </div>
        </div>
        <div class="column">
          <div class="table-max-height">
            <table class="is-fullwidth is-hoverable table table-light">
              <thead>
                <tr>
                  <th>Name</th>
                  <th>Weight Loss Goal</th>
                  <th></th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(participant, index) in participants" v-bind:key="participant.name">
                  <td>{{participant.name}}</td>
                  <td>{{participant.individualWeightLossGoal}} lbs.</td>
                  <td class="has-text-centered">
                    <button @click="removeParticipant(index)" class="button is-danger">
                      <span class="icon">
                        <i class="fas fa-times"></i>
                      </span>
                    </button>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>
    <div class="has-margin-top-5 has-text-right">
      <button @click="acceptClicked" :disabled="participants.length < 1" class="button is-success">
        <span class="icon">
          <i class="fas fa-check"></i>
        </span>
        <span>Accept</span>
      </button>
      &nbsp;
      <button @click="$parent.close()" class="button is-danger">
        <span class="icon">
          <i class="fas fa-ban"></i>
        </span>
        <span>Cancel</span>
      </button>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import Participant from '@/models/participant.model';

@Component
export default class AddParticpantModal extends Vue {
  @Prop({ default: () => new Array<Participant>() })
  public participants!: Array<Participant>;
  public participant: Participant = new Participant();

  addParticipant() {
    this.participants.push(this.participant);
    this.participant = new Participant();
  }

  removeParticipant(index: number) {
    this.participants.splice(index, 1);
  }

  acceptClicked() {
    this.$emit('add-ok-clicked', this.participants);
  }

  validParticipant() {
    return this.participant.name && this.participant.individualWeightLossGoal;
  }
}
</script>
