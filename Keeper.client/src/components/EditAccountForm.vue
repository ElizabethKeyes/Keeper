<template>
  <form @submit.prevent="editAccount()">
    <div class="form-floating mb-3">
      <input type="text" class="form-control" id="name" v-model="editable.name">
      <label for="name">Name</label>
    </div>

    <div class="form-floating mb-3">
      <input type="url" class="form-control" id="img" v-model="editable.picture" @input="updateProfileImagePreview()">
      <label for="img">Profile Photo Url</label>
    </div>
    <div class="d-flex justify-content-center my-2">
      <img :src="profileImagePreview" v-if="profileImagePreview" class="profile-pic-preview"
        alt="your account's profile image">
    </div>


    <div class="form-floating mb-3">
      <input type="url" class="form-control" id="img" v-model="editable.coverImg" @input="updateCoverImagePreview()">
      <label for="img">Cover Photo Url</label>
    </div>
    <img :src="coverImagePreview" v-if="coverImagePreview" class="image-preview" alt="your account's cover image">
    <div class="d-flex justify-content-end">
      <button class="btn btn-dark">Save</button>
    </div>
  </form>
</template>


<script>
import { ref, watchEffect } from "vue";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { AppState } from "../AppState.js";
import { accountService } from "../services/AccountService.js";

export default {
  setup() {
    const editable = ref({})
    const profileImagePreview = ref(null)
    const coverImagePreview = ref(null)

    watchEffect(() => {
      if (AppState.account.id) {
        editable.value = { ...AppState.account }
        profileImagePreview.value = AppState.account.picture
      }
    })
    return {
      editable,
      profileImagePreview,
      coverImagePreview,

      updateCoverImagePreview() {
        coverImagePreview.value = editable.value.coverImg
      },

      updateProfileImagePreview() {
        profileImagePreview.value = editable.value.picture
      },

      async editAccount() {
        try {
          const accountData = editable.value
          await accountService.editAccount(accountData)
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

.profile-pic-preview {
  object-fit: cover;
  object-position: center;
  border-radius: 100%;
  height: 15vh;
  width: 15vh;
}

.image-preview {
  object-fit: cover;
  object-position: center;
  width: 100%;
  margin-bottom: 1em;
}
</style>