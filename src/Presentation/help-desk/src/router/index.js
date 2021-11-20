import { createRouter, createWebHistory } from "vue-router";
import routesNames from "@/router/routesNames";
import SingIn from "@/views/SignIn.vue";
import Dashboard from "@/views/DashboardPanel.vue";

const routes = [
  {
    path: "/",
    name: routesNames.signIn,
    component: SingIn
  },
  {
    path: "/dashboard-panel",
    name: routesNames.dashboard,
    component: Dashboard
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

export default router;
