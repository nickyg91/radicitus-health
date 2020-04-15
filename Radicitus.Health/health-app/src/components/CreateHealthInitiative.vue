<template>
  <div class="columns">
    <div class="column is-one-third"></div>
    <div class="column is-one-third">
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
            v-model="initiative.totalWeightLossGoal"
            type="text"
            required
            pattern="(\d.\d{1})"
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
        <div>
          <button @click="submit()" class="button button-big is-fullwidth is-primary">Submit</button>
        </div>
      </form>
    </div>
    <div class="column is-one-third"></div>
  </div>
</template>
<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import HealthInitiative from '../models/initiative.model';
import AdminService from '../services/admin/admin.service';
@Component
export default class CreateHealthInitiative extends Vue {
  private initiative: HealthInitiative = new HealthInitiative();
  private service = new AdminService();
  public isLoading = false;
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
}
</script>
