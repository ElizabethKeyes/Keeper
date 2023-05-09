<template>
  <img :src="keep.img" :alt="'a photo of ' + keep.name" class="keeps-photos">
  <div class="title-container">
    <h5 class="keeps-title">{{ keep.name }}</h5>
    <img :src="keep.creator.picture" :alt="'a photo of ' + keep.creator.name" :title="keep.creator.name">
  </div>
</template>


<script>
import { Keep } from "../models/Keep.js";

export default {
  props: {
    keep: { type: Keep, required: true }
  },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
.keeps-photos {
  object-fit: cover;
  object-position: center;
  width: 100%;
  border-radius: 8px;
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
  .title-container {
    padding-bottom: .4em;
    font-size: xx-small;
  }

  .title-container img {
    display: none;
  }

}
</style>