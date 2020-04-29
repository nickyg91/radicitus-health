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
        <h3>Add/Edit Participants</h3>
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
          <b-field label="Starting Weight">
            <b-input
              validation-message="Starting Weight is required!"
              placeholder="100.5"
              v-model.number="participant.startingWeight"
              type="text"
              required
              min="5"
              max="256"
            ></b-input>
          </b-field>
          <b-field label="Goal Weight">
            <b-input
              validation-message="Goal Weight is required!"
              placeholder="100.5"
              v-model.number="participant.individualWeightLossGoal"
              type="text"
              required
              min="5"
              max="256"
            ></b-input>
          </b-field>
          <div class="container has-text-right">
            <button
              v-if="!participant.id"
              :disabled="!validParticipant()"
              @click="addParticipant"
              class="button is-info is-light"
            >
              <span class="icon">
                <i class="fas fa-plus"></i>
              </span>
              <span>Add</span>
            </button>
            <button
              v-if="participant.id > 0"
              :disabled="!validParticipant()"
              @click="updateParticipant"
              class="button is-info is-light"
            >
              <span class="icon">
                <i class="fas fa-plus"></i>
              </span>
              <span>Confirm</span>
            </button>
            &nbsp;
            <button class="button is-light is-danger" @click="cancelEdit">
              <span class="icon">
                <i class="fas fa-ban"></i>
              </span>
              <span>Cancel</span>
            </button>
          </div>
        </div>
      </div>
      <div class="columns">
        <div class="column">
          <div class="table-max-height">
            <table class="is-fullwidth is-hoverable table table-light">
              <thead>
                <tr>
                  <th>Name</th>
                  <th>Starting Weight</th>
                  <th>Weight Loss Goal</th>
                  <th></th>
                  <th></th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(participant, index) in participants" v-bind:key="participant.name">
                  <td>{{participant.name}}</td>
                  <td>{{participant.startingWeight}} lbs.</td>
                  <td>{{participant.individualWeightLossGoal}} lbs.</td>
                  <td class="width-zero">
                    <button @click="editParticipant(participant, index)" class="button is-info">
                      <span class="icon">
                        <i class="fas fa-edit"></i>
                      </span>
                    </button>
                  </td>
                  <td class="width-zero">
                    <button @click="removeParticipant(participant, index)" class="button is-danger">
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
    <hr />
    <div class="has-margin-top-5 has-text-right">
      <button @click="saveChanges" :disabled="participants.length < 1" class="button is-success">
        <span class="icon">
          <i class="fas fa-save"></i>
        </span>
        <span>Save</span>
      </button>
      &nbsp;
      <button @click="close" class="button is-danger">
        <span class="icon">
          <i class="fas fa-ban"></i>
        </span>
        <span>Cancel</span>
      </button>
    </div>
    <b-loading :active.sync="isLoading"></b-loading>
  </div>
</template>
<script lang="ts">
import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import Participant from '@/models/participant.model';
import ParticipantsService from '@/services/leaderboard/participants.service';
import UpdateParticipants from '../models/update-participants.model';

@Component
export default class EditParticipantModal extends Vue {
  @Prop({ default: 0 })
  public id!: number;
  @Prop({ default: () => new Array<Participant>() })
  public participants!: Participant[];
  public participant: Participant = new Participant();
  public isLoading = false;
  public updateParticipantsModel = new UpdateParticipants();
  private service = new ParticipantsService();
  public localParticipants = new Array<Participant>();

  mounted() {
    this.localParticipants = this.participants;
  }

  public editParticipant(participant: Participant) {
    this.participant = participant;
  }

  public async saveChanges() {
    this.isLoading = true;
    try {
      this.updateParticipantsModel.updatedParticipants = this.participants;
      await this.service.saveChangesToParticipants(this.id, this.updateParticipantsModel);
    } catch (exception) {
      this.$buefy.notification.open({
        message: 'An error occurred while saving the participants.',
        type: 'is-danger',
        position: 'is-bottom-right'
      });
    }
    this.isLoading = false;
  }

  public updateParticipant() {
    const index = this.participants.map(x => x.id).indexOf(this.participant.id);
    this.participants[index] = this.participant;
    this.participant = new Participant();
  }

  public addParticipant() {
    this.participant.healthInitiativeId = this.id;
    this.participants.push(this.participant);
    this.updateParticipantsModel.updatedParticipants.push(this.participant);
    this.participant = new Participant();
  }

  public removeParticipant(participant: Participant, index: number) {
    this.updateParticipantsModel.removedParticipants.push(participant);
    this.participants.splice(index, 1);
  }

  public validParticipant() {
    return this.participant.name
      && this.participant.individualWeightLossGoal
      && this.participant.startingWeight;
  }

  public cancelEdit() {
    this.participant = new Participant();
  }

  public close() {
    this.$emit('refresh');
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    (this.$parent as any).close();
  }
}


</script>
