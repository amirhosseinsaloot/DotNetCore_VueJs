function TokenRequest({
  grantType,
  username,
  password,
  refreshToken,
  accessToken
}) {
  this.grantType = grantType;
  this.username = username;
  this.password = password;
  this.refreshToken = refreshToken;
  this.accessToken = accessToken;
}

export default TokenRequest;
