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
      <div v-if="linkPreview.tracks">
        <div class="media">
          <div class="media-left">
            <figure class="image is-48x48">
              <img v-bind:src="linkPreview.imageUrl" alt="album art" />
            </figure>
          </div>
          <div class="media-content">{{linkPreview.title}}</div>
        </div>
        <div class="content">
          <div class="content" v-for="(track, index) in linkPreview.tracks" :key="index">
            <span
              v-if="!isPlaying"
              @click="playOrPause(track.playUrl, index)"
              class="button button-small is-info is-outlined"
            >
              <span class="icon is-small">
                <i class="fa fa-play"></i>
              </span>
            </span>
            <span
              v-if="isPlaying && currentSongPlayingIndex !== index"
              @click="playOrPause(track.playUrl, index)"
              class="button button-small is-info is-outlined"
            >
              <span class="icon is-small">
                <i class="fa fa-play"></i>
              </span>
            </span>
            <span
              v-if="isPlaying && currentSongPlayingIndex === index"
              @click="pauseAudio()"
              class="button button-small is-info is-outlined"
            >
              <span class="icon is-small">
                <i class="fa fa-pause"></i>
              </span>
            </span>
            {{track.name}}
          </div>
        </div>
      </div>
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
  public linkPreview = new LinkPreview();
  public isLoading = false;
  public showPreview = false;
  public audio = new Audio();
  public isPlaying = false;
  public currentSongPlayingIndex = -1;
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

  public pauseAudio() {
    this.audio.pause();
    this.isPlaying = false;
    this.currentSongPlayingIndex = -1;
  }

  public async playOrPause(url: string, index: number) {
    if (this.isPlaying && this.currentSongPlayingIndex !== index) {
      this.audio.pause();
      this.audio = new Audio(url);
      this.currentSongPlayingIndex = index;
      await this.audio.play();
    } else {
      this.audio = new Audio(url);
      this.currentSongPlayingIndex = index;
      this.isPlaying = true;
      await this.audio.play();
    }
  }
}
</script>
