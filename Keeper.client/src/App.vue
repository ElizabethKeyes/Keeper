<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer>
    Made with 📸 by Elizabeth
  </footer>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

main {
  background-color: #fcf6f1;
}


footer {
  display: grid;
  place-content: center;
  height: 48px;
  background-color: #fcf6f1;
  border-top: solid #E5E5E5 2px;
  font-family: 'Oxygen', sans-serif;

}
</style>
