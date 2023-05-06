<template>
  <div class="container-fluid">
    <section class="row">
      <div v-for="k in keeps" :key="k.id" class="col-6 col-md-3">
        <div class="keeps-card">
          <img :src="k.img" :alt="'a photo of ' + k.name" class="keeps-img">
        </div>
      </div>
    </section>
  </div>
</template>

<script>
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js"
import { computed, onMounted } from "vue";
import { AppState } from "../AppState.js";

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

    onMounted(() => {
      GetAllKeeps()
    })

    return {
      keeps: computed(() => AppState.keeps),

    }
  }
}
</script>

<style scoped lang="scss">
.keeps-card {
  border-radius: 5px;
}

.keeps-img {
  object-fit: cover;
  object-position: center;
  width: 100%;

}
</style>
