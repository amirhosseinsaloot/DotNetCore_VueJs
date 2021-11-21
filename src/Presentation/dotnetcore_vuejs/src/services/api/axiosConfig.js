import axios from "axios";
import httpStatusCodes from "@/common/enums/httpStatusCodes";
import apiResultBodyCode from "@/common/enums/apiResultBodyCode";
import API_ROUTES from "@/services/api/apiRoutes";
import localStorageUtils from "@/common/localStorageUtils";
import router from "@/router";
import routesNames from "@/router/routesNames";
import authenticationService from "@/services/authentication/authenticationService";
import { notify } from "@kyvg/vue3-notification";

const instance = axios.create({
  baseURL: process.env.VUE_APP_API_BASE_URL,
  timeout: 5000
});

instance.defaults.headers.get.Accepts = "application/json";
instance.defaults.headers.common["Access-Control-Allow-Origin"] = "*";

// Auth Interceptor
instance.interceptors.request.use((request) => {
  const token = localStorageUtils.getItem("Token");
  if (
    request.url == API_ROUTES.LOGIN.toString() ||
    request.url == API_ROUTES.REFRESH_TOKEN.toString()
  ) {
    request.headers.Authorization = null;
  } else {
    request.headers.Authorization = `Bearer ${token.accessToken}`;
  }

  return request;
});

// Response Success and Response Failure Interceptors
instance.interceptors.response.use(
  // On Response Success
  (response) => {
    return response;
  },

  // On Response Failure
  (error) => {
    const httpStatusCode = error?.response?.status;
    const serverResponse = error?.response?.data;
    const requestUrl = error?.config?.url;

    switch (httpStatusCode) {
      // 400
      case httpStatusCodes.BADREQUEST:
        notify({
          type: "error",
          text: serverResponse.message
        });
        break;

      // 401
      case httpStatusCodes.UNAUTHORIZED:
        if (
          serverResponse.statusCode == apiResultBodyCode.ExpiredSecurityToken
        ) {
          if (requestUrl == API_ROUTES.REFRESH_TOKEN.toString()) {
            router.push({ name: routesNames.signIn });
          } else {
            authenticationService.setTokenWithRefreshToken();
          }
        }
        break;

      // 403
      case httpStatusCodes.FORBIDDEN:
        notify({
          type: "error",
          text: "Access to this resource is forbidden."
        });
        break;

      // 404
      case httpStatusCodes.NOT_FOUND:
        notify({
          type: "error",
          text: serverResponse.message
        });
        break;

      // 500
      case httpStatusCodes.INTERNAL_SERVER_ERROR:
        notify({
          type: "error",
          title: "Server error occured",
          text: serverResponse.message
        });
        break;

      // Unknown
      default:
        notify({
          type: "error",
          text: "Unknown error occurred, please try again later."
        });
        break;
    }

    return Promise.reject(error);
  }
);

export default instance;
