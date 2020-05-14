<template>
  <div class="card min-card-height">
    <div class="card-content">
      <div class="media">
        <div class="media-content">
          <p class="title is-4">{{resource.name}}</p>
          <p class="subtitle is-6">
            <a target="_blank" :href="resource.url">{{resource.url}}</a>
          </p>
        </div>
      </div>
      <div class="content">{{resource.description}}</div>
      <div class="content">
        <span class="tags">
          <span class="tag is-success" v-for="tag in resource.tags" :key="tag">{{tag}}</span>
        </span>
      </div>
    </div>
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
