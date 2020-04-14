<template>
  <section class="section">
    <b-tabs v-model="activeTab" position="is-centered" class="block">
      <b-tab-item label="Create"></b-tab-item>
      <b-tab-item label="View"></b-tab-item>
    </b-tabs>
  </section>
</template>
<script lang="ts">
import Vue from 'vue';
import AdminService from '@/services/admin/admin.service';
import HealthInitiative from '../models/initiative.model';
import Component from 'vue-class-component';
@Component
export default class HealthInitiativesAdmin extends Vue {
  public service = new AdminService();
  public initiatives = new Array<HealthInitiative>();
  public activeTab = 0;
  async mounted() {
    this.initiatives = await this.service.getInitiatives();
  }
}
</script>
