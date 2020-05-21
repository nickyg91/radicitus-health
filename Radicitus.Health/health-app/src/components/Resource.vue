<template>
  <div class="card min-card-height">
    <div class="card-content">
      <div class="media">
        <div class="media-content">
          <p class="title is-4">{{resource.name}}</p>
          <p class="subtitle is-6">
            <a target="_blank" :href="resource.url">{{resource.url}}</a>
          </p>
          <div class="content">
            <span class="tags">
              <span class="tag is-success" v-for="tag in resource.tags" :key="tag">{{tag}}</span>
            </span>
          </div>
        </div>
      </div>
      <div class="content">{{resource.description}}</div>
    </div>
  </div>
</template>
<script lang="ts">
import Vue from 'vue'
import { Prop, Component } from 'vue-property-decorator';
import ResourceItem from '@/models/resource.model';
import LinkPreviewService from '@/services/resources/linkpreview.service';
import { AxiosInstance } from 'axios';
import LinkPreview from '@/models/link-preview.model';
@Component
export default class Resource extends Vue {
  @Prop({ default: () => new ResourceItem() })
  public resource!: ResourceItem;
  @Prop({ default: () => new LinkPreviewService({} as AxiosInstance) })
  public linkPreviewService!: LinkPreviewService;
  public linkPreview!: LinkPreview;
  public isLoading = false;
  public showPreview = false;
  async mounted() {
    try {
      this.isLoading = true;
      this.linkPreview = await this.linkPreviewService.getLinkPreview(this.resource.guid);
      this.showPreview = true;
    } catch (error) {
      this.showPreview = false;
    } finally {
      this.isLoading = false;
    }
  }
}
</script>
