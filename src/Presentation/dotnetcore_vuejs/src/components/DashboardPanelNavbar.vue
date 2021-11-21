<template>
  <div class="dp-navbar">
    <nav class="navbar navbar-expand-lg navbar-light py-4 px-4">
      <div class="d-flex align-items-center">
        <i
          class="fas fa-align-left text-black fs-4 me-3"
          id="menu-toggle"
          @click="toggleSidebar()"
        ></i>
        <h2 class="fs-2 m-0">Content</h2>
      </div>

      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarSupportedContent"
        aria-controls="navbarSupportedContent"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span class="navbar-toggler-icon"></span>
      </button>

      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav ms-auto mb-2 mb-lg-0">
          <li class="nav-item dropdown">
            <a
              class="nav-link dropdown-toggle second-text fw-bold"
              id="navbarDropdown"
              role="button"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            >
              <i class="fas fa-user me-2"></i>John Doe
            </a>
            <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
              <li><a class="dropdown-item">Profile</a></li>
              <li><a class="dropdown-item">Settings</a></li>
              <li><a class="dropdown-item">Logout</a></li>
            </ul>
          </li>
        </ul>
      </div>
    </nav>
  </div>
</template>

<script>
export default {
  name: "DashboardPanelNavbar",
  methods: {
    toggleSidebar() {
      this.emitter.emit("toggle-sidebar");
    }
  }
};
</script>

<style scoped>
.dp-navbar {
  min-width: 100vw;
}

@media (min-width: 768px) {
  .dp-navbar {
    min-width: 0;
    width: 100%;
  }
}

.rounded-full {
  border-radius: 100%;
}

#menu-toggle {
  cursor: pointer;
}
</style>
