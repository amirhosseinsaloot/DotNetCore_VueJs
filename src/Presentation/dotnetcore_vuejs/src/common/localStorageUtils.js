const setItem = (key, value) => {
  const parsedValue = JSON.stringify(value);
  localStorage.setItem(key, parsedValue);
};

const getItem = key => {
  const item = localStorage.getItem(key) || "{}";
  const parsedValue = JSON.parse(item);
  return parsedValue;
};

const removeItem = key => {
  localStorage.removeItem(key);
};

const updateItem = (key, value) => {
  removeItem(key);
  setItem(key, value);
};

export default {
  setItem,
  getItem,
  removeItem,
  updateItem
};
