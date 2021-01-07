<template>
  <div class="col-3 col-6-sm">
    <div :id="'keep-'+ keep.id"
         class="keep-component keep-img card p-2 shadow-lg"
         type="button"
         data-toggle="modal"
         :data-target="'#KeepModalCenter-'+keep.id"
    >
      <div class="card bg-light-g text-white">
        <img :src="keep.img" class="card-img img-fluid" alt="...">
        <div class="card-img-overlay">
          <div class="d-flex h-100 justify-content-between align-items-end">
            <h5 class="card-title">
              {{ keep.name }}
            </h5>
            <img :src="keep.creator.picture" alt="..." class="h-small rounded-circle">
          </div>
        </div>
      </div>
    </div>
  </div>

  <!-- Modal -->
  <div class="modal fade"
       :id="'KeepModalCenter-'+keep.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="KeepModalCenterTitle"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="container-fluid modal-size bg-light">
          <button type="button" class="close text-danger" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
          <div class="row">
            <div class="col-6 h-40 d-flex align-items-center">
              <img :src="keep.img" alt="..." class="img-fluid w-100">
            </div>
            <div class="col-6 h-40 d-flex flex-column justify-content-between">
              <br>
              <div>
                <i class="fas fa-eye mr-2"> {{ keep.views }}</i> <i class="fas fa-key mr-2"> {{ keep.keeps }}</i>  <i class="fas fa-envelope    "> {{ keep.shares }}</i>
              </div>
              <h1 class="text-gray">
                {{ keep.name }}
              </h1>
              <p class="text-gray">
                {{ keep.description }}
              </p>
              <p class="border border-dark w-75 align-self-center text-gray"></p>
              <div class="align-self-center">
                <button class="btn btn-success mr-4">
                  Add to Vault
                </button>
                <i class="fas fa-trash mr-4 " v-if="profile.id === keep.creatorId"></i>
                <img :src="keep.creator.picture" alt="..." class="h-25 rounded-circle">
                {{ keep.creator.name }}
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
export default {
  name: 'KeepComponent',
  props: ['keepProp'],
  setup(props) {
    onMounted(() => {

    })
    return {
      keep: computed(() => props.keepProp),
      profile: computed(() => AppState.profile)
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
  width: 80vh;
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
</style>
