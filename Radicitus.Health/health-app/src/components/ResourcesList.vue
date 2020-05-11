<template>
  <div class="container">
    <div v-if="resources.length > 0">
      <div v-for="resource in resources" :key="resource.guid" class="container">
        <resource :resource="resource"></resource>
      </div>
    </div>
    <div v-else>
      <div class="is-size-1 has-text-centered">No resources have been added!</div>
      <div class="has-text-centered">
        <button
          @click="addResource"
          class="is-medium is-rounded is-full button is-outlined is-success"
        >
          <span class="icon">
            <i class="fas fa-plus"></i>
          </span>
          <span>Add</span>
        </button>
      </div>
    </div>
  </div>
</template>
<script lang="ts">
import Vue from 'vue';
import ResourcesService from '@/services/resources/resources.service';
import ResourceItem from '@/models/resource.model';
import Resource from '@/components/Resource.vue';
import AddResourceModal from '@/components/AddResourceModal.vue';
import { Component } from 'vue-property-decorator';
@Component({
  components: {
    Resource,
    AddResourceModal
  }
})
export default class ResourcesList extends Vue {
  private resourcesService = new ResourcesService(this.$http);
  public resources = new Array<ResourceItem>();

  async mounted() {
    this.resources = await this.resourcesService.getAllResources();
  }

  public addResource() {
    this.$buefy.modal.open({
      component: AddResourceModal,
      parent: this,
      trapFocus: true
    });
  }
}
</script>
