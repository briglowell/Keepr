<template>
  <div class="vault-page container-fluid">
    <div class="row mx-4 mt-4">
      <div class="col-12 mb-4">
        <h1>{{ vault.name }}</h1>
        <h4>{{ vault.description }}</h4>
        <p>Keeps: {{ keeps.length }}</p>
        <button class="btn btn-danger" @click="deleteVault">
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
export default {
  name: 'VaultPage',
  setup() {
    const route = useRoute()
    onMounted(async() => {
      await vaultsService.getVaultById(route.params.id)
      await vaultsService.getVaultKeeps(route.params.id)
    })
    return {
      keeps: computed(() => AppState.vaultKeeps),
      vault: computed(() => AppState.activeVault),
      deleteVault() {
        if (window.confirm) {
          vaultsService.delete(route.params.id)
        }
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>

</style>
