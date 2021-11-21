const isNullOrEmpty = str => {
  if (str == undefined || str == "" || str.length == 0 || !str) {
    return true;
  }

  return false;
};

export default isNullOrEmpty;
