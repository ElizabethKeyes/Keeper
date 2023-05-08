<template>
  <nav class="nav-bg mx-0 d-flex row align-items-center">
    <div class="d-flex col-4">
      <router-link :to="{ name: 'Home' }">
        <button class="btn btn-home rounded-pill my-font">Home</button>
      </router-link>

      <div class="dropdown ms-2 my-font">
        <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown"
          aria-expanded="false">
          Create
        </button>
        <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
          <li><a class="dropdown-item" href="#">new keep</a></li>
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
  <Modal id="createVaultModal">
    <template #header>
      Add your vault
    </template>
    <template #body>
      <CreateVaultForm />
    </template>
  </Modal>
</template>

<script>
import CreateVaultForm from "./CreateVaultForm.vue";
import Login from './Login.vue'
import Modal from "./Modal.vue";
export default {
  setup() {
    return {}
  },
  components: { Login, Modal, CreateVaultForm }
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
}
</style>
