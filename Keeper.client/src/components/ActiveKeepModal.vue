<template>
  <div class="modal fade" id="activeKeepModal" tabindex="-1" aria-labelledby="activeKeepModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg">
      <div v-if="keep" class="modal-content">
        <div class="modal-body row">
          <div class="col-md-6 p-0">
            <img :src="keep.img" :alt="'a photo of ' + keep.name" class="keep-photo">
          </div>
          <div class="col-md-6 p-0 text-col">
            <div class="text-secondary mt-4 d-flex align-items-center justify-content-center icon-row">
              <h6 class="me-3 mb-0"><i class="mdi mdi-eye-outline me-1"></i>{{ keep.views }}</h6>
              <img src="../assets/img/keeps.png" alt="keeper logo" class="kept-icon me-1">
              <h6 class="mb-0">{{ keep.kept }}</h6>
            </div>
            <div>
              <h2 class="title-text">{{ keep.name }}</h2>
              <p class="description-text">{{ keep.description }}</p>
            </div>
            <section class="mb-4 row justify-content-between">
              <div class="col-7">
                <form v-if="account.id" name="addToVault" class="d-flex ms-3" @submit.prevent="addToVault()">
                  <select name="vault" id="vault" class="form-control my-input me-2" required v-model="editable.vault">
                    <!-- <option hidden selected disabled>-select a vault-</option> -->
                    <option v-for="v in vaults" :value="v.id">{{ v.name }}</option>
                  </select>
                  <button type="submit" class="btn save-btn text-light">save</button>
                </form>
              </div>
              <div class="d-flex align-items-center justify-content-end me-3 col-4">
                <img :src="keep.creator.picture" :alt="'a photo of ' + keep.creator.name" class="profile-pic">
                <h6 class="creator-name">{{ keep.creator.name }}</h6>
              </div>
            </section>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed, ref } from "vue";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";

export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      keep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.myVaults),
      account: computed(() => AppState.account),

      async addToVault() {
        try {
          const vaultId = editable.value.vault
          await vaultKeepsService.addToVault(vaultId, AppState.activeKeep.id)
          Pop.toast("Keep has been added to your vault!", "success", "top")
        } catch (error) {
          logger.log(error)
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
  background-color: rgba(135, 122, 143, 1);
}

.save-btn:hover {
  border: rgba(135, 122, 143, 1) solid 1px;
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
  font-weight: bold;
}

.my-input {
  border: none !important;
  border-bottom: gray 2px solid !important;
  background-color: rgba(254, 246, 240, 1);
  border-radius: 0px
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