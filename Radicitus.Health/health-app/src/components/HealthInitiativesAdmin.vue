<style scoped>
.mt-5 {
  margin-top: 5px;
}
</style>
<template>
  <section class="section">
    <div v-if="isViewingLogs">
      <div class="container has-text-left">
        <button @click="backClicked" class="button is-primary is-outlined">
          <span class="icon">
            <i class="fas fa-arrow-left"></i>
          </span>
          <span>Back</span>
        </button>
      </div>
      <initiative-logs class="mt-5" :participants="participants"></initiative-logs>
    </div>
    <div v-if="!isViewingLogs">
      <b-tabs v-model="activeTab" class="block" position="is-centered">
        <b-tab-item label="View">
          <view-admin-health-initiatives
            v-on:viewing-logs="logsViewed($event)"
            v-bind:healthInitiatives="initiatives"
          ></view-admin-health-initiatives>
        </b-tab-item>
        <b-tab-item label="Create">
          <create-health-initiative v-on:created-initative="refresh"></create-health-initiative>
        </b-tab-item>
      </b-tabs>
    </div>
  </section>
</template>
<script lang="ts">
import Vue from 'vue';
import AdminService from '@/services/admin/admin.service';
import HealthInitiative from '../models/initiative.model';
import Component from 'vue-class-component';
import ViewAdminHealthInitiatives from '@/components/ViewAdminHealthInitiatives.vue';
import CreateHealthInitiative from '@/components/CreateHealthInitiative.vue';
import InitiativeLogs from '@/components/InitiativeLogs.vue';
import Logs from '@/models/logs.model';
import Participant from '../models/participant.model';

@Component({
  components: {
    ViewAdminHealthInitiatives,
    CreateHealthInitiative,
    InitiativeLogs
  }
})
export default class HealthInitiativesAdmin extends Vue {
  public service = new AdminService();
  public initiatives = new Array<HealthInitiative>();
  public activeTab = 0;
  public isViewingLogs = false;
  public participants!: Array<Participant>;

  async mounted() {
    this.initiatives = await this.service.getInitiatives();
  }
  async refresh() {
    this.initiatives = await this.service.getInitiatives();
  }

  public backClicked() {
    this.isViewingLogs = false;
  }

  public logsViewed($event: Logs) {
    this.isViewingLogs = $event.isViewingLogs;
    this.participants = $event.participants;
  }
}
</script>
