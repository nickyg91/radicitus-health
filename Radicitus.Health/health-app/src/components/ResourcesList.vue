<style scoped>
.min-card-height {
  min-height: 250px;
  max-height: 250px;
  overflow-y: scroll;
}
.card-button-overflow {
  overflow-y: hidden;
}
.center-add-icon {
  margin-top: 10%;
}
</style>
<template>
  <div class="container">
    <div v-if="resources.length > 0">
      <div class="content">
        <div class="title is-size-2">Health Resources</div>
        <div class="columns">
          <div class="column is-9-desktop">
            <b-field>
              <b-taginput
                :data="tags"
                autocomplete
                :allow-new="true"
                :open-on-focus="true"
                icon="tag"
                placeholder="Add a filter"
                @typing="getFilteredTags"
                v-model="filteredTags"
                type="is-success"
              ></b-taginput>
            </b-field>
            <b-field>
              <b-input @keyup.native="search($event)" type="text" placeholder="Search Resources"></b-input>
            </b-field>
          </div>
          <div class="column is-3-desktop">
            <div class="is-hidden-mobile buttons">
              <button @click="filterResources" class="button is-outlined is-info">
                <span class="icon">
                  <i class="fas fa-filter"></i>
                </span>
                <span>Filter</span>
              </button>
              <button @click="clearFilter" class="button is-dark is-outlined">
                <span class="icon">
                  <i class="fas fa-tags"></i>
                </span>
                <span>Clear</span>
              </button>
            </div>
            <div class="columns is-hidden-desktop">
              <div class="column">
                <button
                  @click="filterResources"
                  class="button is-fullwidth is-large is-outlined is-info"
                >
                  <span class="icon">
                    <i class="fas fa-filter"></i>
                  </span>
                  <span>Filter</span>
                </button>
              </div>
              <div class="column">
                <button
                  @click="clearFilter"
                  class="button is-fullwidth is-large is-dark is-outlined"
                >
                  <span class="icon">
                    <i class="fas fa-tags"></i>
                  </span>
                  <span>Clear</span>
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="columns is-multiline">
        <div
          @click="addResource"
          class="card-button-overflow column min-card-height is-light button is-primary is-4-desktop is-12-mobile"
        >
          <div class="is-size-4 has-text-centered">Create a Resource</div>
          <div class="center-add-icon has-text-centered">
            <i class="fa fa-plus fa-4x"></i>
          </div>
        </div>
        <div
          class="column is-4-desktop is-12-mobile"
          v-for="resource in filteredResources"
          :key="resource.guid"
        >
          <resource :resource="resource"></resource>
        </div>
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
          <span>Create</span>
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
  public tags = new Array<string>();
  public filteredTags = new Array<string>();
  public filteredResources = new Array<ResourceItem>();
  async mounted() {
    this.resources = await this.resourcesService.getAllResources();
    this.filteredResources = this.resources;
    this.tags = await this.resourcesService.getAllTags();
  }

  public getFilteredTags(text: string) {
    this.filteredTags = this.tags.filter(x => x.toLowerCase().indexOf(text.toLowerCase()) > -1);
  }

  public addResource() {
    this.$buefy.modal.open({
      component: AddResourceModal,
      parent: this,
      trapFocus: true,
      events: {
        'resource-added': (item: ResourceItem) => {
          this.resources.push(item);
        }
      }
    });
  }

  public async filterResources() {
    this.filteredResources = await this.resourcesService.filterResources(this.filteredTags);
  }

  public clearFilter() {
    this.filteredResources = this.resources;
  }

  public search(input: KeyboardEvent) {
    const text = (input.target as HTMLInputElement).value;
    if (text.length > 0) {
      this.filteredResources = this.filteredResources.filter(x => `${x.url.toLowerCase()}${x.description.toLowerCase()}${x.name.toLowerCase()}`.indexOf(text.toLowerCase()) > -1);
    }
    if (text.length === 0) {
      this.filteredResources = this.resources;
    }
  }
}
</script>
