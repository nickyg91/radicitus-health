<template>
  <div class="box">
    <div class="title">Add Resource</div>
    <div class="modal-content">
      <b-field label="Name">
        <b-input placeholder="Name" required v-model="model.name" type="text" min="5" max="256"></b-input>
      </b-field>
      <b-field>
        <b-input placeholder="Description">
          <b-input type="text-area" v-model="model.desription"></b-input>
        </b-input>
      </b-field>
      <b-field label="Url">
        <b-input type="text" v-model="model.url"></b-input>
      </b-field>
      <b-field></b-field>
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
  public loading = false;
  //   public onUrlCompleted(url: string) {
  //     console.log(url);
  //     if (url.length > 0) {
  //       this.loading = true;
  //       this.$http.get(url, {
  //         headers: {
  //           'Accept': '*/*',
  //           'Content-Type': 'appllication/x-www-form-url-encoded',

  //         }
  //       }).then(response => {
  //         console.log(response);
  //         this.loading = false;
  //         this.showPreview = true;
  //       });
  //     } else {
  //       this.loading = false;
  //       this.showPreview = false;
  //     }
  //   }

}
</script>
