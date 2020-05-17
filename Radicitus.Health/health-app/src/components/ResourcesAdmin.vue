<template>
  <div class="section">
    <b-loading active.sync="isLoading"></b-loading>
    <table class="table is-fullwidth">
      <thead>
        <tr>
          <th>Resource Name</th>
          <th>Description</th>
          <th>Link</th>
          <th class="width-zero"></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="resource in resources" :key="resource.guid">
          <td>{{resource.name}}</td>
          <td>{{resource.description ? resource.description : 'N/A'}}</td>
          <td>{{resource.url ? resource.url : 'N/A'}}</td>
          <td>
            <button @click="remove(resource.guid)" class="button is-outlined is-danger">
              <span class="icon">
                <i class="fas fa-times"></i>
              </span>
              <span>Delete</span>
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import ResourcesService from '../services/resources/resources.service';
import ResourceItem from '../models/resource.model';
@Component
export default class ResourcesAdmin extends Vue {
  public resourcesService = new ResourcesService(this.$http);
  public resources = new Array<ResourceItem>();
  public isLoading = false;
  public async mounted() {
    this.resources = await this.resourcesService.getAllResources();
  }

  public async remove(guid: string) {
    this.isLoading = true
    try {
      this.resourcesService.remove(guid);
      this.$buefy.notification.open({
        message: 'Resource removed successfully!',
        type: 'is-success',
        position: 'is-bottom-right',
        hasIcon: true
      });
      this.resources = await this.resourcesService.getAllResources();
    } catch (exception) {
      this.$buefy.notification.open({
        message: 'An error occurred while removing the resource',
        type: 'is-danger',
        position: 'is-bottom-right',
        hasIcon: true
      });
    } finally {
      this.isLoading = false;
    }
  }
}
</script>
