<template>
  <div class="vault-page container-fluid">
    <div class="row mx-4 mt-4">
      <div class="col-12 mb-4">
        <h1>{{ vault.name }}</h1>
        <h4>{{ vault.description }}</h4>
        <p>Keeps: {{ keeps.length }}</p>
        <button class="btn btn-danger" @click="deleteVault" v-if="profile.id === vault.creatorId">
          Remove Vault
        </button>
      </div>
      <KeepComponent v-for="keep in keeps" :keep-prop="keep" :key="keep.id" />
      <!-- <div v-for="keep in keeps" :key="keep.id"></div> -->
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'
import router from '../router'
import { profileService } from '../services/ProfileService'
export default {
  name: 'VaultPage',
  setup() {
    const route = useRoute()
    onMounted(async() => {
      await profileService.getProfile()
      await vaultsService.getVaultById(route.params.id)
      await vaultsService.getVaultKeeps(route.params.id)
    })
    return {
      keeps: computed(() => AppState.vaultKeeps),
      vault: computed(() => AppState.activeVault),
      profile: computed(() => AppState.profile),
      deleteVault() {
        if (window.confirm('Do you really want to delete the vault?')) {
          vaultsService.delete(route.params.id)
          router.push({ name: 'Profile' })
        }
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
