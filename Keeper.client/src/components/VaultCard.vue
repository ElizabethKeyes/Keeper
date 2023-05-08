<template>
  <div class="vault-card elevation-4" :style="{ backgroundImage: `url(${vault.img})` }">
    <div class="d-flex justify-content-between w-100">
      <h4 class="vault-font">{{ vault.name }}</h4>
      <img v-if="vault.isPrivate" class="private-icon" src="../assets/img/isPrivate.svg" title="Private Vault"
        alt="a lock icon, this vault is private">
    </div>
  </div>
</template>


<script>
import { Vault } from "../models/Vault.js";

export default {
  props: {
    vault: { type: Vault, required: true }
  },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
.vault-card {
  height: 25vh;
  margin-bottom: 1em;
  background-position: center;
  background-size: cover;
  border-radius: 8px;
  display: flex;
  align-items: end;
}

.vault-card:hover {
  cursor: pointer;
  transform: scale(1.025)
}

.vault-font {
  font-family: 'Quando', serif;
  color: rgba(255, 255, 255, 1);
  margin-left: .5em;
  text-shadow: 1px 1px 2px black;

}

.private-icon {
  margin-right: .5em;
  filter: drop-shadow(1px 1px 2px black);

}
</style>