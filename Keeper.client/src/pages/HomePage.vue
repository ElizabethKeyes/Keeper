<template>
  <div class="container-fluid">
    <section class="row mt-3 overflow-hidden">
      <div class="masonry-with-columns">
        <div v-for="k in keeps" :key="k.id" class="keeps-card elevation-4" @click="setActiveKeep(k.id)"
          data-bs-toggle="modal" data-bs-target="#activeKeepModal">
          <KeepsCard :keep="k" />
        </div>
      </div>
    </section>
  </div>
  <ActiveKeepModal />
</template>

<script>
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js"
import { vaultsService } from "../services/VaultsService.js"
import { computed, onMounted, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import KeepsCard from "../components/KeepsCard.vue"
import ActiveKeepModal from "../components/ActiveKeepModal.vue";
import { vaultKeepsService } from "../services/VaultKeepsService.js";

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

    async function getMyVaultKeeps() {
      try {
        await vaultKeepsService.getMyVaultKeeps()
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
        getMyVaultKeeps()
      }
    })

    return {
      keeps: computed(() => AppState.keeps),
      async setActiveKeep(keepId) {
        await keepsService.setActiveKeep(keepId)
      }
    }
  },
  components: { KeepsCard, ActiveKeepModal }
}
</script>

<style scoped lang="scss">
.masonry-with-columns {
  columns: 4 200px;
  column-gap: 2rem;
  padding-left: 3em;
  padding-right: 3em;
  padding-top: 1em;

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
