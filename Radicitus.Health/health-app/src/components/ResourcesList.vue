<template>
  <div class="container">
    <div v-for="resource in resources" :key="resource.guid" class="container">
      <resource :resource="resource"></resource>
    </div>
  </div>
</template>
<script lang="ts">
import Vue from 'vue';
import ResourcesService from '@/services/resources/resources.service';
import ResourceItem from '@/models/resource.model';
import Resource from '@/components/Resource.vue';
import { Component } from 'vue-property-decorator';
@Component({
  components: {
    Resource
  }
})
export default class ResourcesList extends Vue {
  private resourcesService = new ResourcesService(this.$http);
  public resources = new Array<ResourceItem>();

  async mounted() {
    this.resources = await this.resourcesService.getAllResources();
  }
}
</script>
