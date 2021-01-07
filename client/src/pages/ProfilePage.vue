<template>
  <div class="about container-fluid">
    <div class="row">
      <div class="col-12">
        <img class="rounded" :src="profile.picture" alt="" />
        <h1> {{ profile.name }}</h1>
      </div>
    </div>
    <p>{{ profile.email }}</p>
    <h1>Vaults:</h1>
    <h1>Keeps:</h1>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
export default {
  name: 'Profile',
  setup() {
    return {
      profile: computed(() => AppState.profile)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
