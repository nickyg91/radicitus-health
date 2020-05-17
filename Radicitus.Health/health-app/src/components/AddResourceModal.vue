<template>
  <div class="box">
    <div class="title">Add Resource</div>
    <div class="modal-content">
      <b-field label="Name">
        <b-input
          placeholder="Name"
          required
          v-model="model.name"
          type="text"
          min="5"
          max="256"
          validation-message="Name is required."
        ></b-input>
      </b-field>
      <b-field label="Description">
        <b-input type="textarea" placeholder="Description" v-model="model.description"></b-input>
      </b-field>
      <b-field label="Url" placeholder="Link">
        <b-input type="text" v-model="model.url"></b-input>
      </b-field>
      <b-field label="Tags">
        <b-taginput
          v-model="model.tags"
          :data="filteredTags"
          autocomplete
          :allow-new="true"
          :open-on-focus="true"
          icon="tag"
          placeholder="Add a tag"
          @typing="getFilteredTags"
          type="is-success"
        ></b-taginput>
      </b-field>
      <p
        v-if="!model.tags || model.tags.length < 1"
        class="has-text-danger"
      >Must have at least one tag selected.</p>
    </div>
    <div class="has-margin-top-5 has-text-right">
      <button @click="onSubmitClicked" class="button is-success">
        <span class="icon">
          <i class="fas fa-save"></i>
        </span>
        <span>Save</span>
      </button>
      &nbsp;
      <button @click="$parent.close()" class="button is-danger">
        <span class="icon">
          <i class="fas fa-ban"></i>
        </span>
        <span>Cancel</span>
      </button>
    </div>
  </div>
</template>
<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import ResourceItem from '../models/resource.model';
import ResourcesService from '../services/resources/resources.service';

@Component
export default class AddResourceModal extends Vue {
  private resourceService = new ResourcesService(this.$http);
  public model = new ResourceItem();
  public showPreview = false;
  public filteredTags = new Array<string>();
  public tags = new Array<string>();
  public loading = false;
  async mounted() {
    const data = await this.resourceService.getAllTags();
    this.filteredTags = data;
    this.tags = data;
  }

  public getFilteredTags(text: string) {
    this.filteredTags = this.tags.filter(x => x.toLowerCase().indexOf(text.toLowerCase()) > -1);
  }

  public async onSubmitClicked() {
    try {
      this.loading = true;
      await this.resourceService.submitResource(this.model);
      this.loading = false;
      this.$buefy.notification.open({
        message: 'Your Resource has been added successfully!',
        type: 'is-success',
        position: 'is-bottom-right'
      });
      this.$emit('resource-added', this.model);
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      (this.$parent as any).close()
    } catch (error) {
      this.loading = false;
      this.$buefy.notification.open({
        message: 'An error has occurred while submitting the resource.',
        type: 'is-danger',
        position: 'is-bottom-right'
      });
    }
  }
}
</script>
