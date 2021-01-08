<template>
  <div class="visited-profile-page container-fluid">
    <div class="row mb-5 ml-3">
      <div class="col-12 d-flex align-items-center mt-4">
        <img class="rounded mr-4" :src="profile.picture" alt="" />
        <div v-if="profile">
          <h2> {{ profile.name }}</h2>
          <h4>Vaults: <span v-if="vaults">{{ vaults.length }}</span></h4>
          <h4>Keeps: <span v-if="keeps">{{ keeps.length }}</span></h4>
        </div>
      </div>
    </div>
    <div class="row ml-3">
      <div class="col-12 d-flex">
        <h1>Vaults </h1>
      </div>
      <VaultComponent v-for="vault in vaults" :vault-prop="vault" :key="vault.id" />
    </div>
    <div class="row ml-3">
      <div class="col-12 mb-3">
        <h1>Keeps </h1>
      </div>
    </div>
    <div class="card-columns">
      <KeepComponent v-for="keep in keeps" :keep-prop="keep" :key="keep.id" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { keepService } from '../services/KeepService'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
export default {
  name: 'VisitedProfilePage',
  setup() {
    const route = useRoute()
    onMounted(async() => {
      await keepService.getVisitedKeeps(route.params.id)
      await vaultsService.getVisitedVaults(route.params.id)
      AppState.vaultKeeps = []
    })
    return {
      profile: computed(() => AppState.keeps[0].creator),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.myKeeps)
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
