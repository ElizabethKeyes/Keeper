<template>
  <div class="container-fluid">
    <section class="row mt-3">
      <div class="col-3">
        <div v-for="k in keeps1[1]" :key="k.id" class="keeps-card">
          <img :src="k.img" :alt="'a photo of ' + k.name" class="keeps-img">
        </div>
      </div>
      <div class="col-3">
        <div v-for="k in keeps1[2]" :key="k.id" class="keeps-card">
          <img :src="k.img" :alt="'a photo of ' + k.name" class="keeps-img">
        </div>
      </div>
      <div class="col-3">
        <div v-for="k in keeps1[3]" :key="k.id" class="keeps-card">
          <img :src="k.img" :alt="'a photo of ' + k.name" class="keeps-img">
        </div>
      </div>
      <div class="col-3">
        <div v-for="k in keeps1[4]" :key="k.id" class="keeps-card">
          <img :src="k.img" :alt="'a photo of ' + k.name" class="keeps-img">
        </div>
      </div>
    </section>
    {{ keeps1 }}
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
      keeps1: computed(() => {
        const length = AppState.keeps.length
        const keeps = {
          1: [],
          2: [],
          3: [],
          4: []
        }
        for (let i = 0; i < (length); i++) {
          if (i < length / 4) {
            keeps[1].push(AppState.keeps[i])
          } else if (i < length / 2) {
            keeps[2].push(AppState.keeps[i])
          } else if (i < length / 1.33) {
            keeps[3].push(AppState.keeps[i])
          } else {
            keeps[4].push(AppState.keeps[i])
          }
        }
        logger.log('[KEEPS OBJECT]', keeps)
        return keeps
      }),

    }
  }
}
</script>

<style scoped lang="scss">
.keeps-card {
  border-radius: 5px;
  margin-bottom: 1em;
}

.keeps-img {
  object-fit: cover;
  object-position: center;
  width: 100%;
  border-radius: 5px;

}
</style>
