import { createApp } from "vue";
import App from "./App.vue";
import router from "@/router";
import store from "@/store";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";
import "bootstrap-icons/font/bootstrap-icons.css";
import Notifications from "@kyvg/vue3-notification";
import mitt from "mitt";
const emitter = mitt();

const app = createApp(App);
app.config.globalProperties.emitter = emitter;

app
  .use(store)
  .use(router)
  .use(Notifications)
  .mount("#app");
