export const isNullOrUndefined = value => {
  if (
    value == undefined ||
    value === undefined ||
    value == null ||
    value === null
  ) {
    return true;
  }
  return false;
};

export const isNoneEmptyArray = value => {
  if (Array.isArray(value)) {
    if (isNullOrUndefined(value) || value.length === 0) {
      return false;
    } else {
      return true;
    }
  }
};
