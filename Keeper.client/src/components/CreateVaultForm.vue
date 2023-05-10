<template>
  <form @submit.prevent="createVault()">
    <div class="form-floating mb-3">
      <input type="text" class="form-control" id="vaultName" minlength="3" maxlength="20" required
        v-model="editable.name">
      <label for="vaultName">Name</label>
    </div>
    <div class="form-floating mb-3">
      <input type="url" class="form-control" id="vaultImg" minlength="15" maxlength="400" required v-model="editable.img"
        @input="updateImagePreview()">
      <label for="vaultImg">Image Url</label>
    </div>

    <img :src="imagePreview" v-if="imagePreview" class="image-preview" alt="a preview of your vault's image">

    <div class="form-floating mb-3">
      <textarea name="vaultDescription" id="vaultDescription" class="form-control" minlength="5" maxlength="250" required
        v-model="editable.description"></textarea>
      <label for="vaultDescription">Description</label>
    </div>
    <div class="d-flex flex-column align-items-end my-4">
      <small class="text-secondary">Private Vaults can only be seen by you</small>
      <div class="d-flex">
        <input type="checkbox" class="me-2" id="isPrivate" v-model="editable.isPrivate">
        <label for="isPrivate">Make Vault Private?</label>
      </div>
    </div>
    <div class="d-flex justify-content-end">
      <button class="btn btn-dark">Create</button>
    </div>
  </form>
</template>


<script>
import { ref } from "vue";
import { logger } from "../utils/Logger.js";
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";
import { Modal } from "bootstrap";

export default {
  setup() {
    const editable = ref({})
    const imagePreview = ref(null)

    return {
      editable,
      imagePreview,

      async createVault() {
        try {
          const vaultData = editable.value
          vaultData.isPrivate = vaultData.isPrivate ? vaultData.isPrivate : false
          await vaultsService.createVault(vaultData)
          imagePreview.value = null
          editable.value = {}
          Modal.getOrCreateInstance("#createVaultModal").hide()
          Pop.toast(`${vaultData.name} has been created!`, "success", "top")
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
      },

      updateImagePreview() {
        imagePreview.value = editable.value.img
      }
    }
  }
}
</script>


<style lang="scss" scoped>
input {
  background-color: rgba(249, 246, 250, 1);
  border: none !important;
  border-bottom: black 1px solid !important;
  border-radius: 0px;
  border-radius: 0px;
}

textarea {
  background-color: rgba(249, 246, 250, 1);
  border: none !important;
  border-bottom: black 1px solid !important;
  border-radius: 0px;
  border-radius: 0px;
  height: 150px !important;
}

.image-preview {
  object-fit: cover;
  object-position: center;
  width: 100%;
  margin-bottom: 1em;
}
</style>