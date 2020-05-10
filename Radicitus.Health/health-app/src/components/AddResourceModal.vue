<template>
  <div class="box">
    <div class="modal-content"></div>
  </div>
</template>
<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import ResourceItem from '../models/resource.model';

@Component
export default class AddResourceModal extends Vue {

  public model = new ResourceItem();

  public onUrlCompleted(url: string) {
    this.$http.post(url).then(response => {
      console.log(response);
    });
  }

}
</script>
