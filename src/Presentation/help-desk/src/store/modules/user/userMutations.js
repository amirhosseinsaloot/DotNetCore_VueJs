import localStorageUtils from "@/common/localStorageUtils";

// Mutations
const userMutations = {
  SET_CURRENT_USER(state, user) {
    if (user) {
      state.user = user;
      localStorageUtils.setItem("User", user);
    } else {
      state.user = undefined;
      localStorageUtils.removeItem("User");
    }
  },
  SET_AUTH_TOKEN(state, token) {
    if (token) {
      state.token = token;
      localStorageUtils.setItem("Token", token);
    } else {
      state.token = undefined;
      localStorageUtils.removeItem("Token");
    }
  }
};

export default userMutations;
