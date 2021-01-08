<template>
  <!-- <div class="col-lg-3 col-6"> -->
  <div
    class="keep-component keep-img card p-2 shadow-lg"
    type="button"
    data-toggle="modal"
    :data-target="'#KeepModalCenter-'+keep.id"
  >
    <div class="card bg-light-g text-white" @click="increaseView">
      <img :src="keep.img" class="card-img img-fluid" alt="...">
      <div class="card-img-overlay">
        <div :id="'keep-'+ keep.id"></div>
        <div class="d-flex h-100 justify-content-between align-items-end">
          <h5 class="card-title">
            {{ keep.name }}
          </h5>
          <img :src="keep.creator.picture" alt="..." class="h-small rounded-circle">
        </div>
      </div>
    </div>
  </div>
  <!-- </div> -->

  <!-- Modal -->
  <div class="modal fade"
       :id="'KeepModalCenter-'+keep.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="KeepModalCenter"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
      <div class="modal-content">
        <div class=" bg-light">
          <div class="modal-body">
            <div class="container-fluid">
              <button type="button" class="close text-danger" data-dismiss="modal" aria-label="Close">
                <span aria-hidden="true">&times;</span>
              </button>
              <div class="row">
                <div class="col-6 h-40 d-flex align-items-center">
                  <img :src="keep.img" alt="..." class="img-fluid w-100">
                </div>
                <div class="col-6 h-40 d-flex flex-column justify-content-between align-items-center">
                  <br>
                  <div>
                    <i class="fas fa-eye mr-2 green-color"> <span class="text-dark ml-1">{{ keep.views }} </span></i> <i class="fas fa-key mr-2 green-color"> <span class="text-dark ml-1">{{ keep.keeps }}</span></i>  <i class="fas fa-envelope green-color"> <span class="text-dark ml-1">{{ keep.shares }}</span></i>
                  </div>
                  <h1 class="text-gray">
                    {{ keep.name }}
                  </h1>
                  <p class="text-gray">
                    {{ keep.description }}
                  </p>
                  <p class="border border-dark w-75 align-self-center text-gray"></p>
                  <div class="align-self-center">
                    <div class="btn-group mr-4" v-if="vaultKeeps.length === 0 || profile.id !== keep.creatorId">
                      <button type="button" class="btn btn-success dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        Add to Vault
                      </button>
                      <div class="dropdown-menu">
                        <a class="dropdown-item" v-for="vault in vaults" :key="vault.id" data-dismiss="modal" @click="addToVault(vault.id)">@{{ vault.name }} </a>
                      </div>
                    </div>
                    <div v-else>
                      <button class="btn btn-danger" @click="removeFromVault" data-dismiss="modal">
                        Remove from Vault
                      </button>
                    </div>
                    <i class="fas fa-trash mr-4 cursor-point" v-if="profile.id === keep.creatorId" @click="deleteKeep" data-dismiss="modal"></i>
                    <img :src="keep.creator.picture" alt="..." class="h-25 rounded-circle cursor-point" data-dismiss="modal" @click="visitProfile">
                    <span class="cursor-point" data-dismiss="modal" @click="visitProfile">{{ keep.creator.name }}</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { onMounted, computed } from 'vue'
import { AppState } from '../AppState'
import { keepService } from '../services/KeepService'
import { vaultsService } from '../services/VaultsService'
import { useRoute } from 'vue-router'
import router from '../router'
export default {
  name: 'KeepComponent',
  props: ['keepProp'],
  setup(props) {
    const route = useRoute()
    onMounted(() => {

    })
    return {
      keep: computed(() => props.keepProp),
      vaults: computed(() => AppState.vaults),
      profile: computed(() => AppState.profile),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      deleteKeep() {
        if (window.confirm('Are you sure you want to delete?')) {
          keepService.delete(props.keepProp.id)
        }
      },
      addToVault(id) {
        const vaultKeepObject = {}
        vaultKeepObject.keepId = props.keepProp.id
        vaultKeepObject.vaultId = id
        vaultKeepObject.creatorId = AppState.profile.id
        vaultsService.createVaultKeep(vaultKeepObject)

        const newKeep = {}
        newKeep.keeps = props.keepProp.keeps + 1
        keepService.edit(props.keepProp.id, newKeep)
      },
      removeFromVault() {
        if (window.confirm('Are you sure you want to delete?')) {
          vaultsService.removeFromVault(route.params.id, props.keepProp)
          const newKeep = props.keepProp
          newKeep.keeps--
          keepService.edit(props.keepProp.id, newKeep)
        }
      },
      increaseView() {
        const newKeep = props.keepProp
        newKeep.views++
        keepService.edit(props.keepProp.id, newKeep)
      },
      visitProfile() {
        router.push({ name: 'VisitedProfile', params: { id: props.keepProp.creatorId } })
      }
    }
  },
  components: {}
}
</script>

<style lang="scss" scoped>
.keep-img{
  background-image: url(https://flif.info/example-images/fish-flif40100.png);
  background-repeat: no-repeat;
  background-size: cover;
}

.modal-size{
  height: 40vh;
  width: 60vh;
}

.h-40{
  height: 40vh;
}

.text-gray{
  color: rgb(83, 83, 83);
}

.h-small{
  height: 40px;
}

.bg-light-g{
  background-color: #636e72;
}
.green-color{
  color: #55efc4;
}
.cursor-point{
  cursor: pointer;
}
</style>
