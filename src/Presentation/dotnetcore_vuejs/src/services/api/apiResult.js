function ApiResult(response) {
  this.isSuccess = response.data.isSuccess;
  this.code = response.data.code;
  this.message = response.data.message;
  this.data = response.data.data;
}

export default ApiResult;
