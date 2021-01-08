<template>
  <div class="vault-component col-2 cursor-point">
    <div class="card text-white bg-light-g mb-3" style="max-width: 18rem;" @click.prevent="toVaultPage">
      <div class="card-body">
        <div class="d-flex h-100 justify-content-center">
          <h5 class="card-title">
            {{ vault.name }}
          </h5>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import router from '../router'
export default {
  name: 'VaultComponent',
  props: ['vaultProp'],
  setup(props) {
    return {
      vault: computed(() => props.vaultProp),
      profile: computed(() => AppState.profile),
      toVaultPage() {
        router.push({ name: 'VaultPage', params: { id: props.vaultProp.id } })
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.bg-light-g{
  background-color: #636e72;
}
.h-small{
  height: 40px;
}
.cursor-point{
  cursor: pointer;
}

</style>
