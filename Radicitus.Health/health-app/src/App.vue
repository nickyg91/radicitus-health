<style lang="css" scoped>
.header-image {
  width: 15%;
  height: 15%;
}
#app {
  height: 100%;
}
html {
  min-height: 100%;
}
body {
  min-height: 100%;
}
.brand-logo {
  width: 32px;
  height: 32px;
}
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
    <section class="has-background-dark section">
      <router-view class="has-background-dark has-text-white" />
    </section>
  </section>
</template>
