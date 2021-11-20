<template>
  <div class="d-flex dp">
    <DashboardPanelSidebar />
    <div class="flex-row w-100 navbar-content-section">
      <DashboardPanelNavbar />
      <DashboardPanelContent />
    </div>
  </div>
</template>

<script>
import DashboardPanelSidebar from "@/components/DashboardPanelSidebar.vue";
import DashboardPanelNavbar from "@/components/DashboardPanelNavbar.vue";
import DashboardPanelContent from "@/components/DashboardPanelContent.vue";

export default {
  name: "DashboardPanel",
  components: {
    DashboardPanelSidebar,
    DashboardPanelNavbar,
    DashboardPanelContent
  }
};
</script>

<style scoped>
.dp {
  /* background-image: linear-gradient(
    to right,
    #baf3d7,
    #c2f5de,
    #cbf7e4,
    #d4f8ea,
    #ddfaef
  ); */
}
.navbar-content-section {
  max-height: 100vh;
  overflow-y: hidden;
}
</style>
