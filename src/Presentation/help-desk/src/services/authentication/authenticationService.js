import httpService from "@/services/api/httpService";
import TokenRequest from "@/models/authentication/tokenRequest";
import localStorageUtils from "@/common/localStorageUtils";
import API_ROUTES from "@/services/api/apiRoutes";

const AuthenticationService = {
  async login(login) {
    const tokenRequest = new TokenRequest({
      grantType: "password",
      username: login.username,
      password: login.password,
      accessToken: "",
      refreshToken: ""
    });

    const response = await httpService.post(API_ROUTES.LOGIN, tokenRequest);
    return response.data;
  },

  async register(register) {
    const response = await httpService.post(API_ROUTES.REGISTER, register);
    return response.data;
  },

  async logout() {
    await httpService.post(API_ROUTES.LOGOUT, null);
  },

  // Get new token and set that in local storage
  async setTokenWithRefreshToken() {
    const token = localStorageUtils.getItem("Token");
    const tokenRequest = new TokenRequest({
      grantType: "refresh_token",
      username: "",
      password: "",
      accessToken: token.accessToken,
      refreshToken: token.refreshToken
    });

    const response = await httpService.post(
      API_ROUTES.REFRESH_TOKEN,
      tokenRequest
    );

    if (response?.isSuccess == true) {
      localStorageUtils.setItem("Token", response.data);
    } else {
      localStorageUtils.removeItem("Token");
    }
  }
};

export default AuthenticationService;
