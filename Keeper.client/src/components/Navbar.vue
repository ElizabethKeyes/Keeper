<template>
  <nav class="nav-bg px-3 d-flex row align-items-center">
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
          <li><a class="dropdown-item" href="#">new vault</a></li>
        </ul>
      </div>
    </div>
    <div class="col-4 d-flex justify-content-center">
      <img src="../assets/img/logo.png" alt="the keeper logo" class="keeper-logo">
    </div>
    <!-- <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto"> -->
    <!-- <li>
          <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
            About
          </router-link>
        </li> -->
    <!-- </ul> -->
    <!-- LOGIN COMPONENT HERE -->
    <div class="col-4 d-flex justify-content-end">
      <Login />
    </div>
    <!-- </div> -->
  </nav>

  <!--   
  <nav class="navbar navbar-expand-lg nav-bg px-3 d-flex">
    <router-link :to="{ name: 'Home' }">
      <button class="btn btn-home rounded-pill fw-bold my-font">Home</button>
    </router-link>
    <img src="../assets/img/logo.png" alt="the keeper logo" class="keeper-logo">
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto"> -->
  <!-- <li>
          <router-link :to="{ name: 'About' }" class="btn text-success lighten-30 selectable text-uppercase">
            About
          </router-link>
        </li> -->
  <!-- </ul> -->
  <!-- LOGIN COMPONENT HERE -->
  <!-- <Login />
    </div>
  </nav> -->
</template>

<script>
import Login from './Login.vue'
export default {
  setup() {
    return {}
  },
  components: { Login }
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

/* .nav-link {
  text-transform: uppercase;
} */

.keeper-logo {
  height: 85%;
}

.dropdown-menu {
  background-color: rgba(222, 214, 233, 1)
}

/* .navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
} */

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
