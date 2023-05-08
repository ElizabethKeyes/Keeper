<template>
  <nav class="nav-bg mx-0 d-flex row align-items-center desktop-nav">
    <div class="d-flex col-4">
      <router-link :to="{ name: 'Home' }">
        <button class="btn btn-home rounded-pill my-font">Home</button>
      </router-link>

      <div class="dropdown ms-2 my-font">
        <button v-if="account?.id" class="btn dropdown-toggle" type="button" id="dropdownMenuButton1"
          data-bs-toggle="dropdown" aria-expanded="false">
          Create
        </button>
        <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
          <li><a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#createKeepModal">new keep</a></li>
          <hr>
          <li><a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#createVaultModal">new vault</a>
          </li>
        </ul>
      </div>
    </div>
    <div class="col-4 d-flex justify-content-center">
      <img src="../assets/img/logo.png" alt="the keeper logo" class="keeper-logo">
    </div>
    <div class="col-4 d-flex justify-content-end">
      <Login />
    </div>
  </nav>

  <div class="container-fluid mobile-nav">
    <section class="row">
      <div class="col-4 d-flex align-items-center">
        <img src="../assets/img/logo.png" alt="the keeper logo" class="keeper-logo">
      </div>
      <div class="col-4 d-flex justify-content-center align-items-center">
        <button v-if="account?.id" class="btn btn-create my-font" type="button" id="dropdownMenuButton1"
          data-bs-toggle="dropdown" aria-expanded="false">
          Create
        </button>
        <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
          <li><a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#createKeepModal">new keep</a></li>
          <hr>
          <li><a class="dropdown-item" href="#" data-bs-toggle="modal" data-bs-target="#createVaultModal">new vault</a>
          </li>
        </ul>
      </div>
      <div class="col-4 profile-pic-col">
        <Login />
      </div>
    </section>
  </div>

  <Modal id="createVaultModal">
    <template #header>
      Add your vault
    </template>
    <template #body>
      <CreateVaultForm />
    </template>
  </Modal>

  <Modal id="createKeepModal">
    <template #header>
      Add your keep
    </template>
    <template #body>
      <CreateKeepForm />
    </template>
  </Modal>
</template>

<script>
import { computed } from "vue";
import CreateKeepForm from "./CreateKeepForm.vue";
import CreateVaultForm from "./CreateVaultForm.vue";
import Login from './Login.vue'
import Modal from "./Modal.vue";
import { AppState } from "../AppState.js";
export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  },
  components: { Login, Modal, CreateVaultForm, CreateKeepForm }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-bg {
  background-color: #fcf6f1;
  border-bottom: solid #E5E5E5 2px;
  height: 9vh;
}

.my-font {
  font-family: 'Oxygen', sans-serif;
  font-weight: bold;
}

.btn-home {
  background-color: rgba(233, 216, 214, 1)
}

.keeper-logo {
  height: 85%;
}

.dropdown-menu {
  background-color: rgba(222, 214, 233, 1)
}


@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }

  .mobile-nav {
    display: none;
  }
}

@media screen and (max-width: 768px) {
  .desktop-nav {
    display: none !important;
  }

  .mobile-nav {
    display: block;
    position: fixed;
    bottom: 0px;
    right: 0px;
    z-index: 5;
    background-color: #fcf6f1;
    border-top: solid #E5E5E5 2px;
    height: 9vh;
  }

  .profile-pic-col {
    height: 9vh;
    display: flex;
    justify-content: end;
    align-items: center;
  }

  .profile-pic {
    height: 5vh;
    width: 5vh;
    border-radius: 100%;
    object-fit: cover;
    object-position: center;
  }

  .btn-create {
    background-color: rgba(135, 122, 143, 1);
    color: rgba(254, 246, 240, 1)
  }
}
</style>
