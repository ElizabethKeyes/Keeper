<template>
  <div class="container-fluid">
    <section v-if="vault" class="row justify-content-center">
      <div class="col-md-5 mt-md-4 px-0">
        <button v-if="vault.creatorId == account.id" class="btn btn-outline-danger delete-btn"
          @click="deleteVault()">Delete Vault</button>
        <div class="vault-banner" :style="{ backgroundImage: `url(${vault.img})` }">
          <h1>{{ vault.name }}</h1>
          <h2 class="fs-4">by {{ vault.creator.name }}</h2>
        </div>
        <div class="d-flex justify-content-center">
          <h3 class="keeps-count fs-5">{{ vaultKeeps.length }} Keeps</h3>
        </div>
      </div>
      <div class="col-md-10 mb-3">
        <section class="row">
          <div class="masonry-with-columns">
            <div v-for="k in vaultKeeps" :key="k.id" class="keeps-card elevation-4" @click="setActiveKeep(k.id)"
              data-bs-toggle="modal" data-bs-target="#activeKeepModal">
              <KeepsCard :keep="k" />
            </div>
          </div>
        </section>
      </div>
    </section>
  </div>
  <ActiveKeepModal :vault="vault" />
</template>


<script>
import { useRoute, useRouter } from "vue-router";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { computed, onMounted, watchEffect } from "vue";
import { vaultsService } from "../services/VaultsService.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import KeepsCard from "../components/KeepsCard.vue"
import { keepsService } from "../services/KeepsService.js";
import ActiveKeepModal from "../components/ActiveKeepModal.vue";

export default {
  setup() {
    const route = useRoute()
    const router = useRouter()

    async function setActiveVault() {
      try {
        const vaultId = route.params.vaultId
        await vaultsService.setActiveVault(vaultId)
      } catch (error) {
        let errorMessage = error.response.data
        if (errorMessage == "This vault is private!") {
          logger.error(error)
          Pop.toast("That vault is private", "error", "top")
          router.push({ name: "Home" })
        } else {
          logger.log(error)
          Pop.error(error.message)
        }
      }
    }

    async function getKeepsInVault() {
      try {
        const vaultId = route.params.vaultId
        await vaultKeepsService.getKeepsInVault(vaultId)
      } catch (error) {
        let errorMessage = error.response.data
        if (errorMessage == "This vault is private!") {
          logger.error(error)
          Pop.toast("That vault is private", "error", "top")
          router.push({ name: "Home" })
        } else {
          logger.log(error)
          Pop.error(error.message)
        }
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
      setActiveVault()
    })

    watchEffect(() => {
      if (AppState.account.id) {
        GetMyVaults()
        getMyVaultKeeps()
      }
    })

    watchEffect(() => {
      if (AppState.activeVault) {
        getKeepsInVault()
      }
    })

    watchEffect(() => {
      route.params.vaultId
    })
    return {
      vault: computed(() => AppState.activeVault),
      vaultKeeps: computed(() => AppState.keepsInVault),
      account: computed(() => AppState.account),
      route,

      async setActiveKeep(keepId) {
        try {
          await keepsService.setActiveKeep(keepId)
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
      },

      async deleteVault() {
        try {
          if (await Pop.confirm("Are you sure you'd like to delete this vault?", "This action cannot be undone", "Yes, I'm sure", "warning")) {
            const vaultId = route.params.vaultId
            await vaultsService.deleteVault(vaultId)
            router.push({ name: "Account" })

          }
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
      }
    }
  },
  components: { KeepsCard, ActiveKeepModal }
}
</script>


<style lang="scss" scoped>
.vault-banner {
  background-size: cover;
  background-position: center;
  height: 33vh;
  width: 100%;
  border-radius: 8px;
  font-family: 'Quando', serif;
  color: rgba(255, 255, 255, 1);
  text-shadow: 1px 1px 2px black;
  text-align: center;
  display: flex;
  flex-direction: column;
  justify-content: end;
}

.keeps-count {
  background-color: rgba(222, 214, 233, 1);
  display: inline;
  margin-top: .75em;
  border-radius: 5px;
  padding-left: .2em;
  padding-right: .2em;
  padding-top: .1em;
  padding-bottom: .1em;
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

.delete-btn {
  position: absolute;
  top: 91px;
  right: 15px;
  color: #b81020 !important;
  border: 1px solid #b81020 !important
}

.delete-btn:hover {
  color: white !important
}


@media screen and (max-width: 768px) {
  .masonry-with-columns {
    columns: 2 100px;
    padding-left: 1em;
    padding-right: 1em;
    margin-bottom: 2em;
  }

  .vault-banner {
    border-top-left-radius: 0px;
    border-top-right-radius: 0px;
  }

  .delete-btn {
    top: 290px;
    right: 15px;
  }
}
</style>