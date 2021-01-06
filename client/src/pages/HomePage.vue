<template>
  <div class="home flex-grow-1 d-flex flex-column align-items-center justify-content-center">
    <img src="https://bcw.blob.core.windows.net/public/img/8600856373152463" alt="CodeWorks Logo">
    <h1 class="my-5 bg-dark text-light p-3 rounded d-flex align-items-center">
      <span class="mx-2 text-white">Vue 3 Starter</span>
    </h1>
    <keep v-for="keep in keeps" :keep-prop="keep" :key="keep.id" />
  </div>
</template>

<script>
import { onMounted, computed } from 'vue'
import { keepService } from '../services/KeepService'
import { AppState } from '../AppState'

export default {
  name: 'Home',
  setup() {
    onMounted(() => {
      keepService.getKeeps()
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
</style>
