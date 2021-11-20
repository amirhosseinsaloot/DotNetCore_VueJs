import ApiResult from "@/services/api/apiResult";
import apiInstance from "@/services/api/axiosConfig";

const HttpService = {
  async get(url) {
    const result = await apiInstance.get(url);
    return new ApiResult(result);
  },

  async post(url, value) {
    const result = await apiInstance.post(url, value);
    return new ApiResult(result);
  },

  async postFile(url, file) {
    const formData = new FormData();
    formData.append("file", file);
    const config = {
      headers: {
        "Content-Type": "multipart/form-data"
      }
    };

    formData.set("formFile", file);

    const result = apiInstance.post(url, formData, config);

    return new ApiResult(result);
  },

  async put(url, value) {
    const result = await apiInstance.put(url, value);
    return new ApiResult(result);
  },

  async delete(url) {
    const result = await apiInstance.delete(url);
    return new ApiResult(result);
  }
};

export default HttpService;
