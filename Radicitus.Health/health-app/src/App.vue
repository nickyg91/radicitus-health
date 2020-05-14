<style lang="scss">
.header-image {
  width: 12%;
}
.width-zero {
  width: 0px;
}
.has-background-image {
  position: relative;
  overflow: hidden;
}

.bg-photo {
  object-fit: cover;
  object-position: center center;
  position: absolute;
  height: 100%;
  width: 100%;
  opacity: 0.3;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s;
}
.fade-enter, .fade-leave-to /* .fade-leave-active below version 2.1.8 */ {
  opacity: 0;
}

.is-nav-centered {
  margin-left: 44%;
}
// Import Bulma's core
@import "~bulma/sass/utilities/_all";

// Set your colors
$primary: $blue;
$primary-invert: findColorInvert($primary);
$twitter: #4099ff;
$twitter-invert: findColorInvert($twitter);

// Setup $colors to use as bulma classes (e.g. 'is-twitter')
$colors: (
  "white": (
    $white,
    $black
  ),
  "black": (
    $black,
    $white
  ),
  "light": (
    $light,
    $light-invert
  ),
  "dark": (
    $dark,
    $dark-invert
  ),
  "primary": (
    $primary,
    $primary-invert
  ),
  "info": (
    $info,
    $info-invert
  ),
  "success": (
    $success,
    $success-invert
  ),
  "warning": (
    $warning,
    $warning-invert
  ),
  "danger": (
    $danger,
    $danger-invert
  ),
  "twitter": (
    $twitter,
    $twitter-invert
  )
);
$colors: mergeColorMaps(
  (
    "white": (
      $white,
      $black
    ),
    "black": (
      $black,
      $white
    ),
    "light": (
      $light,
      $light-invert
    ),
    "dark": (
      $dark,
      $dark-invert
    ),
    "primary": (
      $primary,
      $primary-invert,
      $primary-light,
      $primary-dark
    ),
    "link": (
      $link,
      $link-invert,
      $link-light,
      $link-dark
    ),
    "info": (
      $info,
      $info-invert,
      $info-light,
      $info-dark
    ),
    "success": (
      $success,
      $success-invert,
      $success-light,
      $success-dark
    ),
    "warning": (
      $warning,
      $warning-invert,
      $warning-light,
      $warning-dark
    ),
    "danger": (
      $danger,
      $danger-invert,
      $danger-light,
      $danger-dark
    )
  ),
  $custom-colors
);
$shades: mergeColorMaps(
  (
    "black-bis": $black-bis,
    "black-ter": $black-ter,
    "grey-darker": $grey-darker,
    "grey-dark": $grey-dark,
    "grey": $grey,
    "grey-light": $grey-light,
    "grey-lighter": $grey-lighter,
    "white-ter": $white-ter,
    "white-bis": $white-bis
  ),
  $custom-shades
);
// Links
$link: $primary;
$link-invert: $primary-invert;
$link-focus-border: $primary;
// Import Bulma and Buefy styles
@import "~bulma";
@import "~buefy/src/scss/buefy";
</style>
<template>
  <section id="app">
    <section class="hero is-success is-small has-background-image">
      <img class="bg-photo" src="@/assets/header.png" />
      <div class="hero-head">
        <nav class="navbar is-hidden-desktop">
          <div class="navbar-brand">
            <a
              v-bind:class="{'is-active': isMenuOpen}"
              @click="isOpen"
              role="button"
              class="navbar-burger burger has-text-white"
              aria-label="menu"
              aria-expanded="false"
            >
              <span class="has-text-white" aria-hidden="true"></span>
              <span class="has-text-white" aria-hidden="true"></span>
              <span class="has-text-white" aria-hidden="true"></span>
            </a>
          </div>
          <div class="navbar-menu">
            <div class="navbar-end">
              <router-link
                class="navbar-item"
                active-class="is-active"
                to="/points"
                exact
              >Point System</router-link>
              <router-link class="navbar-item" active-class="is-active" to="/players" exact>Players</router-link>
              <router-link
                class="navbar-item"
                active-class="is-active"
                to="/resources"
                exact
              >Resources</router-link>
            </div>
          </div>
        </nav>
      </div>
      <div class="hero-body">
        <div class="container has-text-centered">
          <img class="header-image" src="@/assets/Logo_Radicitus_White.png" />
          <h1 class="title is-size-3">Radicitus Health Initiative</h1>
        </div>
      </div>
    </section>
    <div class="has-background-success is-hidden-mobile">
      <div class="navbar-menu" v-bind:class="{'is-active': isMenuOpen}">
        <div class="navbar-start is-nav-centered">
          <router-link
            class="is-tab navbar-item"
            active-class="is-active has-text-white"
            to="/points"
            exact
          >Point System</router-link>
          <router-link
            class="is-tab navbar-item"
            active-class="is-active has-text-white"
            to="/players"
            exact
          >Players</router-link>
          <router-link
            class="is-tab navbar-item"
            active-class="is-active has-text-white"
            to="/resources"
            exact
          >Resources</router-link>
        </div>
      </div>
    </div>
    <section class="section">
      <transition
        name="fade"
        enter-active-class="fade-enter-active"
        leave-active-class="fade-leave-active"
        mode="out-in"
      >
        <router-view />
      </transition>
    </section>
  </section>
</template>
<script lang="ts">
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
@Component
export default class App extends Vue {
  public isMenuOpen = false;
  public isOpen() {
    this.isMenuOpen = !this.isMenuOpen;
  }

}
</script>
