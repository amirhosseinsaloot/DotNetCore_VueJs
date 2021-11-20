import HttpService from "@/services/api/httpService";
import API_ROUTES from "@/services/api/apiRoutes";
import { isNullOrUndefined } from "@/common/objectUtils";

const UserService = {
  async getAllUsers() {
    const response = await HttpService.get(API_ROUTES.USERS);
    return response.data;
  },
  async getUserById(id) {
    const response = await HttpService.get(`${API_ROUTES.USERS}/${id}`);
    return response.data;
  },
  async createUser(userCreate) {
    const response = await HttpService.post(API_ROUTES.USERS, userCreate);
    return response.data;
  },
  async updateUser(id, userUpdate, profilePicture) {
    if (!isNullOrUndefined(profilePicture)) {
      const fileResponse = await HttpService.postFile(
        API_ROUTES.FILES,
        profilePicture
      );
      userUpdate.profilePictureId = fileResponse.data.id;
    }

    await HttpService.put(`${API_ROUTES.USERS}/${id}`, userUpdate);
  }
};

export default UserService;
