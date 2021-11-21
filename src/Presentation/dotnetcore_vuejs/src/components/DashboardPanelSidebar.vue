<template>
  <div
    class="bg-dark dp-sidebar"
    :class="
      isDefaultState ? 'dp-sidebar-default-state' : 'dp-sidebar-toggled-state'
    "
  >
    <div
      class="dp-sidebar-header py-4 fs-4 text-uppercase text-white text-center border-bottom"
    >
      <i class="fas fa-tasks me-2"></i>Help Desk
    </div>
    <div class="list-group list-group-flush my-3">
      <a
        class="list-group-item list-group-item-action bg-transparent text-white text-start"
        ><i class="fas fa-tachometer-alt me-2"></i>Dashboard</a
      >
      <a
        class="list-group-item list-group-item-action bg-transparent text-white text-start"
        ><i class="fas fa-power-off me-2"></i>Logout</a
      >
    </div>
  </div>
</template>

<script>
export default {
  name: "DashboardPanelSidebar",
  mounted() {
    this.emitter.on("toggle-sidebar", () => {
      this.isDefaultState = !this.isDefaultState;
    });
  },
  data() {
    return { isDefaultState: true };
  }
};
</script>

<style scoped>
.dp-sidebar {
  min-height: 100vh;
  width: 15rem;
}

/* sidebar transitions */
.dp-sidebar {
  -webkit-transition: margin 0.25s ease-out;
  -moz-transition: margin 0.25s ease-out;
  -o-transition: margin 0.25s ease-out;
  transition: margin 0.25s ease-out;
}

.dp-sidebar-default-state {
  margin-left: -15rem;
}

.dp-sidebar-toggled-state {
  margin-left: 0;
}

@media (min-width: 768px) {
  .dp-sidebar-default-state {
    margin-left: 0;
  }
  .dp-sidebar-toggled-state {
    margin-left: -15rem;
  }
}

.dp-sidebar-header {
  padding: 0.875rem 1.25rem;
  font-size: 1.2rem;
}

.list-group {
  width: 15rem;
}

.list-group-item {
  border: none;
  padding: 20px 30px;
}

.list-group-item.active {
  background-color: transparent;
  color: var(--main-text-color);
  font-weight: bold;
  border: none;
}
</style>
