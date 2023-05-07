<template>
  <div class="container-fluid">
    <section class="row mt-3 overflow-hidden">
      <div class="masonry-with-columns">
        <div v-for="k in keeps" :key="k.id" class="keeps-card">
          <img :src="k.img" :alt="'a photo of ' + k.name" class="keeps-photos">
          <div class="title-container">
            <h5 class="keeps-title">{{ k.name }}</h5>
            <img :src="k.creator.picture" :alt="'a photo of ' + k.creator.name">
          </div>
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
    }
  }
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
  }
}


.keeps-photos {
  object-fit: cover;
  object-position: center;
  width: 100%;
  border-radius: 8px;
}

.keeps-card {
  position: relative;
}

.title-container {
  position: absolute;
  bottom: 0px;
  left: 0px;
  right: 0px;
  display: flex;
  justify-content: space-between;
  padding-left: .5em;
  padding-right: .5em;
  padding-bottom: .25em;
  align-items: center;
}

.title-container img {
  height: 5vh;
  width: 5vh;
  border-radius: 100%;
  object-fit: cover;
  object-position: center;
}

.keeps-title {
  font-family: 'Marko One', serif;
  margin-bottom: 0px;
  text-shadow: 1px 1px 2px black;
  color: rgba(255, 255, 255);

}


@media screen and (max-width: 768px) {
  .masonry-with-columns {
    columns: 2 100px;
  }
}
</style>
