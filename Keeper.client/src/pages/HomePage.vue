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
import { computed, onMounted } from "vue";
import { AppState } from "../AppState.js";
import KeepsCard from "../components/KeepsCard.vue"
import ActiveKeepModal from "../components/ActiveKeepModal.vue";

export default {

  setup() {
    let screenWidth = 0
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
  }
}
</style>
