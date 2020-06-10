<style lang="scss" scoped>
.is-horizontal-center {
  justify-content: center;
}
</style>
<template>
  <section class="box section">
    <div class="container">
      <div class="title">{{linkPreview.title}}</div>
      <div class="content">
        <div class="is-flex is-horizontal-center mb-4">
          <figure class="image is-128x128">
            <img class="image is-rounded" v-bind:src="linkPreview.imageUrl" alt="Playlist Art" />
          </figure>
        </div>
        <div class="content">
          <div
            class="box is-4 is-offset-4"
            v-for="(track, index) in linkPreview.tracks"
            :key="index"
          >
            <div class="columns is-mobile">
              <div class="column is-2">
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
              </div>
              <div class="column has-text-left">
                <span>
                  <p class="is-size-4">{{index + 1}}). {{track.name}}</p>
                </span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>
<script lang="ts">
import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import LinkPreview from '@/models/link-preview.model';
@Component
export default class TracksModal extends Vue {
  @Prop({ default: () => { return new LinkPreview(); } })
  public linkPreview!: LinkPreview;
  public audio = new Audio();
  public isPlaying = false;
  public currentSongPlayingIndex = -1;
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
