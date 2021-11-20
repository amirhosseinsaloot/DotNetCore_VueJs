import AuthenticationService from "@/services/authentication/authenticationService";
import UserService from "@/services/user/userService";

// Actions
const userActions = {
  async loginAsync({ commit }, login) {
    const response = await AuthenticationService.login(login);
    commit("SET_CURRENT_USER", response.userViewModel);
    commit("SET_AUTH_TOKEN", response.token);
  },
  async registerAsync({ commit }, register) {
    const response = await AuthenticationService.register(register);
    commit("SET_CURRENT_USER", response.userViewModel);
    commit("SET_AUTH_TOKEN", response.token);
  },
  async updateAsync({ commit }, accountSettingStoreModel) {
    const user = await UserService.updateUser(
      accountSettingStoreModel.userId,
      accountSettingStoreModel.user,
      accountSettingStoreModel.profilePicture
    );
    commit("SET_CURRENT_USER", user);
  },
  async logoutAsync({ commit }, register) {
    await AuthenticationService.logout(register);
    commit("SET_CURRENT_USER", undefined);
    commit("SET_AUTH_TOKEN", undefined);
  }
};

export default userActions;
