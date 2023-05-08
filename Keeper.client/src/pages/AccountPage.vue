<template>
  <div class="container-fluid">
    <section class="row justify-content-center">
      <div class="col-md-8 p-0 mt-md-5 anchor">
        <img :src="account.coverImg" :alt="'cover image for ' + account.name" class="cover-img">
        <div class="profile-pic-container">
          <img :src="account.picture" :alt="'a photo of ' + account.name" class="profile-pic">
        </div>
      </div>
      <div class="col-md-10 d-flex flex-column align-items-center buffer">
        <h2>{{ account.name }}</h2>
        <h5>{{ myVaults.length }} Vaults | {{ myKeeps.length }} Keeps</h5>
        <button class="btn btn-outline-dark edit-profile-btn" data-bs-toggle="modal"
          data-bs-target="#editProfileModal">Edit Profile</button>
      </div>
      <div class="col-md-10 mt-3">
        <h3 class="mb-3">Vaults</h3>
        <section class="row">
          <div v-for="v in myVaults" :key="v.id" class="col-md-3 col-6">
            <VaultCard :vault="v" />
          </div>
        </section>
      </div>
      <div class="col-md-10 mt-5">
        <h3 class="mb-3">Keeps</h3>
        <section class="row">
          <div class="masonry-with-columns">
            <div v-for="k in myKeeps" :key="k.id" class="keeps-card elevation-4" @click="setActiveKeep(k.id)"
              data-bs-toggle="modal" data-bs-target="#activeKeepModal">
              <KeepsCard :keep="k" />
            </div>
          </div>
        </section>
      </div>
    </section>
  </div>
  <ActiveKeepModal />

  <Modal id="editProfileModal">
    <template #header>
      Edit your account
    </template>
    <template #body>
      <EditAccountForm />
    </template>
  </Modal>
</template>

<script>
import { computed, onMounted, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from "../services/KeepsService.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { vaultsService } from "../services/VaultsService.js"
import VaultCard from "../components/VaultCard.vue"
import KeepsCard from "../components/KeepsCard.vue"
import ActiveKeepModal from "../components/ActiveKeepModal.vue"
import Modal from "../components/Modal.vue"
import EditAccountForm from "../components/EditAccountForm.vue"

export default {
  setup() {
    async function GetAllKeeps() {
      try {
        await keepsService.GetAllKeeps()
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }

    async function GetMyVaults() {
      try {
        const userId = AppState.account.id
        await vaultsService.GetMyVaults(userId)
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }

    onMounted(() => {
      GetAllKeeps()
    })

    watchEffect(() => {
      if (AppState.account.id) {
        GetMyVaults()
      }
    })
    return {
      account: computed(() => AppState.account),
      myVaults: computed(() => AppState.myVaults),
      myKeeps: computed(() => AppState.keeps.filter(k => k.creatorId == AppState.account.id)),

      async setActiveKeep(keepId) {
        await keepsService.setActiveKeep(keepId)
      },
    }
  },
  components: { VaultCard, KeepsCard, ActiveKeepModal, Modal, EditAccountForm }
}
</script>

<style scoped>
.anchor {
  position: relative;
}

.cover-img {
  object-fit: cover;
  object-position: center;
  width: 100%;
  max-height: 40vh;
}

.profile-pic-container {
  position: absolute;
  bottom: -60px;
  width: 100%;
  display: flex;
  justify-content: center;
}

.profile-pic {
  object-fit: cover;
  object-position: center;
  border-radius: 100%;
  height: 15vh;
  width: 15vh;
  border: solid white 2px;
}

.buffer {
  margin-top: 8vh;
}


.masonry-with-columns {
  columns: 4 200px;
  column-gap: 2rem;
  padding-top: 1em;
}

.keeps-card {
  width: 150px;
  color: white;
  margin: 0 1rem 1rem 0;
  display: inline-block;
  width: 100%;
  text-align: center;
  font-family: system-ui;
  font-weight: 900;
  font-size: 2rem;
  border-radius: 8px;
}

.keeps-card {
  position: relative;
}

.keeps-card:hover {
  cursor: pointer;
  transform: scale(1.025);
}


@media screen and (max-width: 768px) {
  .masonry-with-columns {
    columns: 2 100px;
    padding-left: 1em;
    padding-right: 1em;
  }
}
</style>
