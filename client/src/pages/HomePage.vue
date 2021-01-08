<template>
  <div class="container-fluid">
    <div class="home card-columns mt-5">
      <KeepComponent v-for="keep in keeps" :keep-prop="keep" :key="keep.id" />
    </div>
  </div>
</template>

<script>
import { onMounted, computed } from 'vue'
import { keepService } from '../services/KeepService'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
import { profileService } from '../services/ProfileService'

export default {
  name: 'Home',
  setup() {
    onMounted(async() => {
      await profileService.getProfile()
      await keepService.getKeeps()
      await vaultsService.getMyVaults()
      AppState.vaultKeeps = []
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
