import { isNullOrUndefined } from "@/common/objectUtils";

// Getter functions
const userGetters = {
  getCurrentUser(state) {
    return state.user;
  },
  getToken(state) {
    return state.token;
  },
  isLoggedIn(state) {
    return !isNullOrUndefined(state.user);
  }
};

export default userGetters;
