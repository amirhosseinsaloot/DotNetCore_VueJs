const emailRegex = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

const isValidEmail = email => {
  if (email.length >= 320) {
    return false;
  } else {
    return emailRegex.test(email);
  }
};

const isValidUsername = username => {
  if (
    typeof username == "undefined" ||
    username == "" ||
    username.includes(" ")
  ) {
    return false;
  }

  return true;
};

export default { isValidEmail, isValidUsername };
