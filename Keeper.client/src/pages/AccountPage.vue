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
      </div>
      <div class="col-md-10 mt-3">
        <h3 class="mb-3">Vaults</h3>
        <section class="row">
          <div v-for="v in myVaults" :key="v.id" class="col-md-3 col-6">
            <VaultCard :vault="v" />
          </div>
        </section>
      </div>
      <div class="col-md-10 mt-3">
        <h3 class="mb-3">Keeps</h3>
        <section class="row">
          <div v-for="k in myKeeps" :key="k.id" class="col-md-3 col-6">
            <!-- insert keeps component here -->
            <!-- TODO need to make keeps component next -->
          </div>
        </section>
      </div>
    </section>
  </div>
</template>

<script>
import { computed, onMounted, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from "../services/KeepsService.js"
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { vaultsService } from "../services/VaultsService.js"
import VaultCard from "../components/VaultCard.vue"

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
    }
  },
  components: { VaultCard }
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
}

.buffer {
  margin-top: 8vh;
}
</style>
