<template>
  <form @submit.prevent="createKeep()">
    <div class="form-floating mb-3">
      <input type="text" class="form-control" id="name" v-model="editable.name">
      <label for="name">Name</label>
    </div>
    <div class="form-floating mb-3">
      <input type="url" class="form-control" id="img" v-model="editable.img" @input="updateImagePreview()">
      <label for="img">Image Url</label>
    </div>

    <img :src="imagePreview" v-if="imagePreview" class="image-preview" alt="a preview of your keep's image">

    <div class="form-floating mb-3">
      <textarea name="description" id="description" class="form-control" v-model="editable.description"></textarea>
      <label for="description">Description</label>
    </div>
    <div class="d-flex justify-content-end">
      <button class="btn btn-dark">Create</button>
    </div>
  </form>
</template>


<script>
import { ref } from "vue";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";

export default {
  setup() {
    const editable = ref({})
    const imagePreview = ref(null)
    return {
      editable,
      imagePreview,

      updateImagePreview() {
        imagePreview.value = editable.value.img
      },

      async createKeep() {
        try {
          const keepData = editable.value
          await keepsService.createKeep(keepData)
          Pop.toast(`${keepData.name} has been created!`, "success", "top")
          imagePreview.value = null
          editable.value = {}
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