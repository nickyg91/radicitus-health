<template>
  <section class="section">
    <b-tabs v-model="activeTab" class="block" position="is-centered">
      <b-tab-item label="View">
        <view-admin-health-initiatives v-bind:healthInitiatives="initiatives"></view-admin-health-initiatives>
      </b-tab-item>
      <b-tab-item label="Create">
        <create-health-initiative></create-health-initiative>
      </b-tab-item>
    </b-tabs>
  </section>
</template>
<script lang="ts">
import Vue from 'vue';
import AdminService from '@/services/admin/admin.service';
import HealthInitiative from '../models/initiative.model';
import Component from 'vue-class-component';
import ViewAdminHealthInitiatives from '@/components/ViewAdminHealthInitiatives.vue';
import CreateHealthInitiative from '@/components/CreateHealthInitiative.vue'
@Component({
  components: {
    ViewAdminHealthInitiatives,
    CreateHealthInitiative
  }
})
export default class HealthInitiativesAdmin extends Vue {
  public service = new AdminService();
  public initiatives = new Array<HealthInitiative>();
  public activeTab = 0;
  async mounted() {
    this.initiatives = await this.service.getInitiatives();
  }
}
</script>
