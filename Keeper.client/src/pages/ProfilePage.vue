<template>
  <div class="container-fluid">
    <section v-if="profile" class="row justify-content-center">
      <div class="col-md-8 p-0 mt-md-5 anchor">
        <img :src="profile.coverImg" :alt="'cover image for ' + profile.name" class="cover-img">
        <div class="profile-pic-container">
          <img :src="profile.picture" :alt="'a photo of ' + profile.name" class="profile-pic">
        </div>
      </div>
      <div class="col-md-10 d-flex flex-column align-items-center buffer">
        <h1 class="fs-2">{{ profile.name }}</h1>
        <h2 class="fs-5">{{ vaults.length }} Vaults | {{ keeps.length }} Keeps</h2>
      </div>
      <div class="col-md-10 mt-3">
        <h3 class="mb-3">Vaults</h3>
        <section class="row">
          <div v-for="v in vaults" :key="v.id" class="col-md-3 col-6">
            <VaultCard :vault="v" />
          </div>
        </section>
      </div>
      <div class="col-md-10 mt-5">
        <h3 class="mb-3">Keeps</h3>
        <section class="row">
          <div class="masonry-with-columns">
            <div v-for="k in keeps" :key="k.id" class="keeps-card elevation-4" @click="setActiveKeep(k.id)"
              data-bs-toggle="modal" data-bs-target="#activeKeepModal">
              <KeepsCard :keep="k" />
            </div>
          </div>
        </section>
      </div>
    </section>
  </div>
  <ActiveKeepModal />
</template>


<script>
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { profilesService } from "../services/ProfilesService.js"
import { computed, onMounted, watchEffect } from "vue";
import { useRoute } from "vue-router";
import { vaultsService } from "../services/VaultsService.js";
import { AppState } from "../AppState.js";
import VaultCard from "../components/VaultCard.vue";
import { keepsService } from "../services/KeepsService.js";

export default {
  setup() {
    const route = useRoute();
    async function getProfile() {
      try {
        const profileId = route.params.profileId;
        await profilesService.getProfile(profileId);
      }
      catch (error) {
        logger.log(error);
        Pop.error(error.message);
      }
    }
    async function getVaultsByProfileId() {
      try {
        const profileId = route.params.profileId;
        await vaultsService.getVaultsByProfileId(profileId);
      } catch (error) {
        logger.log(error)
        Pop.error(error.message)
      }
    }
    async function getKeepsByProfileId() {
      try {
        const profileId = route.params.profileId
        await keepsService.getKeepsByProfileId(profileId)
      } catch (error) {
        logger.log(error)
        Pop.error(error.message)
      }
    }
    async function getMyVaults() {
      try {
        const userId = AppState.account.id
        await vaultsService.GetMyVaults(userId)
      } catch (error) {
        logger.log(error)
        Pop.error(error.message)
      }
    }


    onMounted(() => {
      getProfile();
      getVaultsByProfileId();
      getKeepsByProfileId();
    });

    watchEffect(() => {
      if (AppState.account.id) {
        getMyVaults();
      }
    })

    return {
      profile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.profileVaults),
      keeps: computed(() => AppState.profileKeeps),

      async setActiveKeep(keepId) {
        try {
          await keepsService.setActiveKeep(keepId)
        } catch (error) {
          logger.log(error)
          Pop.error(error.message)
        }
      },
    };
  },
  components: { VaultCard }
}
</script>


<style lang="scss" scoped>
.anchor {
  position: relative;
}

.cover-img {
  object-fit: cover;
  object-position: center;
  width: 100%;
  max-height: 40vh;
}

.profile-pic-container {
  position: absolute;
  bottom: -60px;
  width: 100%;
  display: flex;
  justify-content: center;
}

.profile-pic {
  object-fit: cover;
  object-position: center;
  border-radius: 100%;
  height: 15vh;
  width: 15vh;
  border: solid white 2px;
}

.buffer {
  margin-top: 8vh;
}


.masonry-with-columns {
  columns: 4 200px;
  column-gap: 2rem;
  padding-top: 1em;
}

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
    padding-left: 1em;
    padding-right: 1em;
    margin-bottom: 3em;
  }
}
</style>