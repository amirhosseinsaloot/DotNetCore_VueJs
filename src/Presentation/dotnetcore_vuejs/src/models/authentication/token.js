function Token({
  accessToken,
  refreshToken,
  refreshTokenExpiresIn,
  tokenType
}) {
  this.accessToken = accessToken;
  this.refreshToken = refreshToken;
  this.refreshTokenExpiresIn = refreshTokenExpiresIn;
  this.tokenType = tokenType;
}

export default Token;
