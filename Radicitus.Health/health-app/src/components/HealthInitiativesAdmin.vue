<template>
  <section class="section">
    <div v-for="initiative in initiatives" v-bind:key="initiative.id">{{initiative.name}}</div>
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
  async mounted() {
    this.initiatives = await this.service.getInitiatives();
  }
}
</script>
