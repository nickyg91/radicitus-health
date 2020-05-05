<template>
  <div class="card">
    <div class="card-header">{{resource.name}}</div>
    <div class="card-content">{{resource.url}}</div>
    <div class="card-footer"></div>
  </div>
</template>
<script lang="ts">
import Vue from 'vue'
import { Prop, Component } from 'vue-property-decorator';
import ResourceItem from '@/models/resource.model';
@Component
export default class Resource extends Vue {
  @Prop({ default: () => new ResourceItem() })
  public resource!: ResourceItem;
}
</script>
