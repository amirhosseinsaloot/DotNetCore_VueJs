module.exports = {
  root: true,
  env: {
    node: true
  },
  extends: ["plugin:vue/vue3-essential", "@vue/standard"],
  parserOptions: {
    parser: "babel-eslint"
  },
  rules: {
    "no-console": process.env.NODE_ENV === "production" ? "warn" : "off",
    "no-debugger": process.env.NODE_ENV === "production" ? "warn" : "off",
    quotes: ["error", "double"],
    semi: ["error", "always"],
    eqeqeq: "off",
    "space-before-function-paren": "off",
    "no-multiple-empty-lines": [2, { max: 99999, maxEOF: 1 }]
  }
};
