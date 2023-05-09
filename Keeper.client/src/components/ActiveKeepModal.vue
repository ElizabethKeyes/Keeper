<template>
  <div class="modal fade" id="activeKeepModal" tabindex="-1" aria-labelledby="activeKeepModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg">
      <div v-if="keep" class="modal-content">
        <div class="modal-body row">
          <button @click="deleteKeep(keep.id, keep.name)" v-if="account.id == keep.creator.id" class="btn delete-btn"
            title="Delete Keep"><i class="mdi mdi-delete text-danger"></i></button>
          <button class="btn remove-btn" title="Remove From Vault" @click="removeKeepFromVault(keep.id, keep.name)"
            v-if="account.id && route.name == 'VaultDetailsPage'"><i
              class="mdi mdi-bookmark-remove text-danger"></i></button>
          <div class="col-md-6 p-0">
            <img :src="keep.img" :alt="'a photo of ' + keep.name" class="keep-photo">
          </div>
          <div class="col-md-6 p-0 text-col">
            <div class="mt-4 d-flex align-items-center justify-content-center icon-row">
              <h6 class="me-3 mb-0"><i class="mdi mdi-eye-outline me-1"></i>{{ keep.views }}</h6>
              <img src="../assets/img/keeps.png" alt="keeper logo" class="kept-icon me-1">
              <h6 class="mb-0">{{ keep.kept }}</h6>
            </div>
            <div>
              <h2 class="title-text">{{ keep.name }}</h2>
              <p class="description-text">{{ keep.description }}</p>
            </div>
            <section class="mb-4 row justify-content-between">
              <div v-if="dictionary && keep" class="col-7">
                <form v-if="account.id && route.name == 'Home' || route.name == 'ProfilePage'" name="addToVault"
                  class="d-flex ms-3" @submit.prevent="addToVault()">
                  <select name="vault" id="vault" class="form-control my-input me-2" required v-model="editable.vault">
                    <!-- <option hidden selected disabled>-select a vault-</option> -->
                    <option v-for="v in availableVaults" :value="v.id" v-if="availableVaults.length > 0">{{ v.name }}
                    </option>
                    <option disabled selected v-else>No available vaults</option>
                  </select>
                  <button type="submit" class="btn save-btn text-light">save</button>
                </form>
                <small v-if="route.name == 'Home' || route.name == 'ProfilePage'" class="gray-text ms-3">Select a vault to
                  save
                  this keep</small>
              </div>
              <div class="d-flex align-items-center justify-content-end me-3 col-4" data-bs-dismiss="modal">
                <router-link :to="{ name: 'ProfilePage', params: { profileId: keep.creator.id } }">
                  <div class="d-flex align-items-center text-dark">
                    <img :src="keep.creator.picture" :alt="'a photo of ' + keep.creator.name" class="profile-pic">
                    <h6 class="creator-name">{{ keep.creator.name }}</h6>
                  </div>
                </router-link>
              </div>
            </section>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, onMounted, ref } from "vue";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { keepsService } from "../services/KeepsService.js";
import { useRoute } from "vue-router";

export default {
  setup() {
    const editable = ref({})
    const route = useRoute()

    return {
      editable,
      route,
      keep: computed(() => AppState.activeKeep),
      dictionary: computed(() => AppState.vaultKeepDictionary),
      vaults: computed(() => AppState.myVaults),
      account: computed(() => AppState.account),
      availableVaults: computed(() => {
        if (AppState.vaultKeepDictionary[AppState.activeKeep.id]) {
          let vaultIds = AppState.vaultKeepDictionary[AppState.activeKeep.id]
          let availableVaults = AppState.myVaults.filter(v => v == v)
          for (let i = 0; i < vaultIds.length; i++) {
            let foundIndex = availableVaults.findIndex(v => v.id == vaultIds[i])
            if (foundIndex > -1) {
              availableVaults.splice(foundIndex, 1)
            }
          }
          return availableVaults

        } else return AppState.myVaults
      }),

      async addToVault() {
        try {
          const vaultId = editable.value.vault
          await vaultKeepsService.addToVault(vaultId, AppState.activeKeep.id)
          Pop.toast("Keep has been added to your vault!", "success", "top")
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
      },

      async deleteKeep(keepId, keepName) {
        try {
          if (await Pop.confirm("Are you sure you want to delete this keep?", "This cannot be undone", "Yes, I'm sure", "warning")) {
            await keepsService.deleteKeep(keepId)
            Pop.toast(`"${keepName}" has been deleted`, "success", "top")
          }
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      },

      async removeKeepFromVault(keepId, keepName) {
        try {
          if (await Pop.confirm("Are you sure you want to remove this keep from your vault?", "You can always add it again later if you change your mind.", "Yes, I'm sure", "warning")) {
            const vaultKeep = AppState.keepsInVault.find(vk => vk.id == keepId)
            const vaultKeepId = vaultKeep.vaultKeepId
            const vaultId = route.params.vaultId
            let revisedVaultIds = AppState.vaultKeepDictionary[keepId].filter(i => i != vaultId)
            AppState.vaultKeepDictionary[keepId] = revisedVaultIds
            await vaultKeepsService.removeKeepFromVault(vaultKeepId, keepId)
            Pop.toast(`"${keepName}" has been removed`, "success", "top")
          }
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.modal-body {
  padding: 0px;
  position: relative;
}

.delete-btn {
  position: absolute;
  top: 0px;
  right: 0px;
  width: 5vh !important;
}

.remove-btn {
  position: absolute;
  top: 0px;
  right: 30px;
  width: 5vh !important;
}

.keep-photo {
  object-fit: cover;
  object-position: center;
  width: 100%;
  border-top-left-radius: .5rem;
  border-bottom-left-radius: .5rem;
  height: 60vh;
}

.text-col {
  background-color: rgba(254, 246, 240, 1);
  border-top-right-radius: .5rem;
  border-bottom-right-radius: .5rem;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

.title-text {
  font-family: "Marko One", serif;
  text-align: center;
}

.description-text {
  margin-left: 2em;
  margin-right: 2em;
}

.kept-icon {
  object-fit: cover;
  object-position: center;
  height: 2vh;
  width: 2vh;
}

.save-btn {
  background-color: #74647d;
}

.save-btn:hover {
  border: #74647d solid 1px;
  color: black !important
}

.profile-pic {
  height: 5vh;
  width: 5vh;
  object-fit: cover;
  object-position: center;
  border-radius: 100%;
}

.creator-name {
  margin-bottom: 0px;
  margin-left: .5em;
  font-weight: bold;
}

.my-input {
  border: none !important;
  border-bottom: gray 2px solid !important;
  background-color: rgba(254, 246, 240, 1);
  border-radius: 0px
}

.icon-row {
  color: #59727e
}

.gray-text {
  color: #59727e
}

@media screen and (max-width: 768px) {
  .keep-photo {
    border-top-left-radius: 0px;
    border-bottom-left-radius: 0px;
    max-height: 50vh;
  }

  .text-col {
    border-top-right-radius: 0px;
    border-bottom-right-radius: 0px;
    min-height: 40vh;
  }

  .icon-row {
    margin-bottom: .75em;
  }
}
</style>