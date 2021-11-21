function Token({
  accessToken,
  accessTokenExpiresIn,
  refreshToken,
  refreshTokenExpiresIn,
  tokenType
}) {
  this.accessToken = accessToken;
  this.accessTokenExpiresIn = accessTokenExpiresIn;
  this.refreshToken = refreshToken;
  this.refreshTokenExpiresIn = refreshTokenExpiresIn;
  this.tokenType = tokenType;
}

export default Token;
