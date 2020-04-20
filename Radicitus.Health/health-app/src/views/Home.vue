<template>
  <div class="home">
    <current-health-initiative></current-health-initiative>
  </div>
</template>

<script>
// @ is an alias to /src
import CurrentHealthInitiative from '@/components/CurrentHealthInitiative.vue';

export default {
  name: 'Home',
  components: {
    CurrentHealthInitiative
  }
}
</script>
