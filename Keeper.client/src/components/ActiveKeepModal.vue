<template>
  <div class="modal fade" id="activeKeepModal" tabindex="-1" aria-labelledby="activeKeepModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg">
      <div v-if="keep" class="modal-content">
        <div class="modal-body row">
          <div class="col-md-6 p-0">
            <img :src="keep.img" :alt="'a photo of ' + keep.name" class="keep-photo">
          </div>
          <div class="col-md-6 p-0 text-col">
            <div class="text-secondary mt-2 d-flex justify-content-center">
              <h6 class="me-3"><i class="mdi mdi-eye-outline me-1"></i>{{ keep.views }}</h6>
              <img src="../assets/img/keeps.png" alt="keeper logo" class="kept-icon me-1">
              <h6>{{ keep.kept }}</h6>
            </div>
            <div>
              <h2 class="title-text">{{ keep.name }}</h2>
              <p class="description-text">{{ keep.description }}</p>
            </div>
            <div>
              Create
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";

export default {
  setup() {
    return {
      keep: computed(() => AppState.activeKeep)
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
  height: 3vh;
  width: 3vh;
}
</style>