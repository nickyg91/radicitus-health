<style lang="scss">
.header-image {
  width: 15%;
  height: 15%;
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
    <section class="hero is-dark">
      <div class="hero-body">
        <div class="container has-text-centered">
          <img class="header-image" src="@/assets/Logo_Radicitus_White.png" />
          <h1 class="title is-size-3">Radicitus Health Initiative</h1>
        </div>
      </div>
    </section>
    <nav class="navbar has-background-success" role="navigation">
      <div class="navbar-brand">
        <img class="navbar-item" width="72" height="28" src="@/assets/Logo_Radicitus_White.png" />
        <a role="button" class="navbar-burger burger" aria-label="menu" aria-expanded="false">
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
        </a>
      </div>
      <div class="navbar-menu">
        <div class="navbar-start">
          <router-link
            class="is-expanded navbar-item is-size-4 is-one-third"
            :active="'is-active'"
            to="/"
          >Point System</router-link>
          <router-link
            class="navbar-item is-size-4 is-one-third"
            :active="'is-active'"
            to="/initiatives"
          >Initiatives</router-link>
          <router-link
            class="navbar-item is-size-4 is-one-third"
            :active="'is-active'"
            to="/initiatives"
          >Resources</router-link>
        </div>

        <a role="button" class="navbar-burger" aria-label="menu" aria-expanded="false">
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
        </a>
      </div>
    </nav>
    <section class="section">
      <router-view />
    </section>
  </section>
</template>
