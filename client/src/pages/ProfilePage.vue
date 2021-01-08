<template>
  <div class="about container-fluid">
    <div class="row mb-5 ml-3">
      <div class="col-12 d-flex align-items-center mt-4">
        <img class="rounded mr-4" :src="profile.picture" alt="" />
        <div>
          <h2> {{ profile.name }}</h2>
          <h4>Vaults: <span v-if="vaults">{{ vaults.length }}</span></h4>
          <h4>Keeps: <span v-if="keeps">{{ keeps.length }}</span></h4>
        </div>
      </div>
    </div>
    <div class="row ml-3">
      <div class="col-12 d-flex">
        <h1>Vaults <i class="fas fa-plus green-color ml-2 cursor-point" v-if="profile.email === user.email" data-toggle="modal" data-target="#vaultModal"></i></h1>
      </div>
      <VaultComponent v-for="vault in vaults" :vault-prop="vault" :key="vault.id" />
    </div>
    <div class="row ml-3">
      <div class="col-12 mb-3">
        <h1>Keeps <i class="fas fa-plus green-color ml-2 cursor-point" v-if="profile.email === user.email" data-toggle="modal" data-target="#keepModal"></i></h1>
      </div>
      <KeepComponent v-for="keep in keeps" :keep-prop="keep" :key="keep.id" />
    </div>
  </div>

  <!-- Vault Modal -->
  <div class="modal fade"
       id="vaultModal"
       tabindex="-1"
       role="dialog"
       aria-labelledby="vaultModalTitle"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-body">
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
          <h2>New Vault</h2>
          <form>
            <div class="form-group">
              <label for="vault-name" class="col-form-label">Vault Name:</label>
              <input type="text" class="form-control" id="vault-name" v-model="state.newVault.name">
            </div>
            <div class="form-group">
              <label for="message-text" class="col-form-label">Description:</label>
              <textarea class="form-control" id="message-text" v-model="state.newVault.description"></textarea>
            </div>
            <div class="form-check">
              <input class="form-check-input" type="checkbox" value="" v-model="state.newVault.isPrivate" id="defaultCheck1">
              <label class="form-check-label" for="defaultCheck1">
                {{ state.newVault.isPrivate ? "Private" : "Public" }}
              </label>
            </div>
          </form>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-success" data-dismiss="modal" @click="createVault">
            Add Vault
          </button>
        </div>
      </div>
    </div>
  </div>

  <!-- Keep Modal -->
  <div class="modal fade"
       id="keepModal"
       tabindex="-1"
       role="dialog"
       aria-labelledby="keepModalTitle"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-body">
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
          <h2>New Keep</h2>
          <form>
            <div class="form-group">
              <label for="keep-name" class="col-form-label">Keep Name:</label>
              <input type="text" class="form-control" id="keep-name" v-model="state.newKeep.name">
            </div>
            <div class="form-group">
              <label for="keep-img" class="col-form-label">Img Url:</label>
              <input type="text" class="form-control" id="keep-img" v-model="state.newKeep.img">
            </div>
            <div class="form-group">
              <label for="message-text" class="col-form-label">Description:</label>
              <textarea class="form-control" id="message-text" v-model="state.newKeep.description"></textarea>
            </div>
          </form>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-success" data-dismiss="modal" @click="createKeep">
            Add Keep
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted, computed, reactive } from 'vue'
import { AppState } from '../AppState'
import { keepService } from '../services/KeepService'
import { vaultsService } from '../services/VaultsService'
import { profileService } from '../services/ProfileService'

export default {
  name: 'Profile',
  setup() {
    const state = reactive({
      newVault: {
        name: '',
        description: '',
        isPrivate: false
      },
      newKeep: {
        name: '',
        img: '',
        description: '',
        views: 0,
        keeps: 0,
        shares: 0
      }
    })
    onMounted(async() => {
      await profileService.getProfile()
      await keepService.getMyKeeps()
      await vaultsService.getMyVaults()
      AppState.vaultKeeps = []
    })
    return {
      state,
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps),
      user: computed(() => AppState.user),
      createKeep() {
        keepService.create(state.newKeep)
      },
      createVault() {
        vaultsService.create(state.newVault)
      }
    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
.green-color{
  color: #55efc4;
}
.cursor-point{
  cursor: pointer;
}

</style>
