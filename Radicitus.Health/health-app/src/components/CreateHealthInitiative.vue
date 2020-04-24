<style scoped>
.has-margin-top {
  margin-top: 5px;
}
</style>
<template>
  <div class="box">
    <div class="columns">
      <div class="column is-one-half">
        <b-loading :is-full-page="false" :active.sync="isLoading" :can-cancel="true"></b-loading>
        <form>
          <b-field label="Name">
            <b-input
              validation-message="Name is required!"
              placeholder="Name"
              v-model="initiative.name"
              type="text"
              required
              min="5"
              max="256"
            ></b-input>
          </b-field>
          <b-field label="Total Weight Loss Goal">
            <b-input
              placeholder="100.5"
              validation-message="Please enter a number between 0.0 and 999.9!"
              v-model.number="initiative.totalWeightLossGoal"
              type="text"
              required
              min="0"
              max="999.9"
            ></b-input>
          </b-field>
          <b-field label="Start Date">
            <b-datepicker
              icon-pack="fas"
              v-model="initiative.startDateTime"
              placeholder="Click to select..."
              icon="calendar"
              trap-focus
            ></b-datepicker>
          </b-field>
          <b-field label="End Date">
            <b-datepicker
              icon-pack="fas"
              v-model="initiative.endDateTime"
              placeholder="Click to select..."
              icon="calendar"
              trap-focus
            ></b-datepicker>
          </b-field>
        </form>
      </div>
      <div class="column is-one-half">
        <div class="container has-text-right">
          <button @click="showAddParticipantModal" class="button is-info">
            <span class="icon">
              <i class="fas fa-plus"></i>
            </span>
            <span>Add Participants</span>
          </button>
        </div>
        <div class="has-margin-top container">
          <table class="is-fullwidth is-hoverable table table-light">
            <thead>
              <tr>
                <th>Participant</th>
                <th>Goal</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              <tr
                v-for="(participant, index) in initiative.participants"
                v-bind:key="participant.id + participant.name"
              >
                <td>{{participant.name}}</td>
                <td>{{participant.individualWeightLossGoal}}</td>
                <td>
                  <button @click="remove(index)" class="button is-danger">
                    <span class="icon">
                      <i class="fas fa-times" />
                    </span>
                  </button>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
    <div class="has-text-right">
      <button :disabled="!validInitiative()" @click="submit()" class="button button-big is-primary">
        <span class="icon">
          <i class="fas fa-save"></i>
        </span>
        <span>Submit</span>
      </button>
    </div>

    <b-modal :active.sync="isAddParticipantModalShown">
      <add-participant-modal
        v-bind:participants="initiative.participants"
        v-on:add-ok-clicked="modalOkClicked"
      ></add-participant-modal>
    </b-modal>
  </div>
</template>
<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import HealthInitiative from '../models/initiative.model';
import AdminService from '../services/admin/admin.service';
import AddParticipantModal from '@/components/AddParticipantModal.vue';
import Participant from '@/models/participant.model';
@Component({
  components: {
    AddParticipantModal
  }
})
export default class CreateHealthInitiative extends Vue {
  private initiative: HealthInitiative = new HealthInitiative();
  private service = new AdminService();
  public isLoading = false;
  public isAddParticipantModalShown = false;
  async submit() {
    try {
      this.isLoading = true;
      await this.service.createInitative(this.initiative);
      this.initiative = new HealthInitiative();
      this.$buefy.notification.open({
        duration: 5000,
        message: 'The initiative has been created successfully!',
        type: 'is-success',
        hasIcon: true,
        position: "is-bottom-right"
      });
      this.$emit('created-initative');
      this.isLoading = false;
    } catch (ex) {
      this.$buefy.notification.open({
        duration: 5000,
        message: 'An error has ocurred while attempting to create the initiative!',
        type: 'is-danger',
        hasIcon: true,
        position: "is-bottom-right"
      });
      this.isLoading = false;
    }
  }
  showAddParticipantModal() {
    this.isAddParticipantModalShown = true;
  }
  modalOkClicked(participants: Array<Participant>) {
    this.initiative.participants = participants;
    this.isAddParticipantModalShown = false;
  }
  validInitiative() {
    return this.initiative.name
      && this.initiative.totalWeightLossGoal
      && this.initiative.startDateTime
      && this.initiative.endDateTime
      && this.initiative.participants
      && this.initiative.participants.length > 0;
  }
}
</script>
