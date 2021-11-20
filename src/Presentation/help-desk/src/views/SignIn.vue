<template>
  <div class="container">
    <br />
    <h2 class="text-md-center">Welcome to our HelpDesk 😍</h2>
    <hr />
    <div class="row justify-content-center">
      <div class="col-md-6">
        <div class="card">
          <!-- Card header -->
          <header class="card-header">
            <a
              class="float-end btn btn-outline-primary"
              v-if="authMode == 'Login'"
              @click="formSwitching()"
              >Register</a
            >
            <a
              class="float-end btn btn-outline-primary"
              v-else
              @click="formSwitching()"
              >Login</a
            >
            <h4 class="card-title mt-2">{{ authMode }}</h4>
          </header>

          <!-- Card Body -->
          <article class="card-body">
            <!-- Login section -->
            <div v-if="authMode == 'Login'">
              <div class="mb-3">
                <label class="form-label">Username</label>
                <input
                  type="text"
                  class="form-control"
                  v-model="login.username"
                  @change="checkUsername()"
                  placeholder=""
                />
                <small class="validation">{{
                  validations.loginUsername
                }}</small>
              </div>
              <div class="mb-3">
                <label class="form-label">Password</label>
                <input
                  type="password"
                  class="form-control"
                  v-model="login.password"
                  @change="checkPassword()"
                />
                <small class="validation">{{
                  validations.loginPassword
                }}</small>
              </div>
            </div>

            <!-- Register section -->
            <div v-if="authMode == 'Register'">
              <!-- Username and Password -->
              <div class="row">
                <div class="mb-3 col-sm-6">
                  <label class="form-label">Username</label>
                  <input
                    type="text"
                    class="form-control"
                    v-model="register.username"
                    @change="checkUsername()"
                    placeholder=""
                  />
                  <small class="validation">{{
                    validations.registerUsername
                  }}</small>
                </div>
                <div class="mb-3 col-sm-6">
                  <label class="form-label">Password</label>
                  <input
                    type="text"
                    class="form-control"
                    v-model="register.password"
                    @change="checkPassword()"
                  />
                  <small class="validation">{{
                    validations.registerPassword
                  }}</small>
                </div>
              </div>

              <!-- Firstname and Lastname -->
              <div class="row">
                <div class="mb-3 col-sm-6">
                  <label class="form-label">Firstname</label>
                  <input
                    type="text"
                    class="form-control"
                    v-model="register.firstname"
                    @change="checkFirstname()"
                    placeholder=""
                  />
                  <small class="validation">{{ validations.firstname }}</small>
                </div>
                <div class="mb-3 col-sm-6">
                  <label class="form-label">Lastname</label>
                  <input
                    type="text"
                    class="form-control"
                    v-model="register.lastname"
                    @change="checkLastname()"
                  />
                  <small class="validation">{{ validations.lastname }}</small>
                </div>
              </div>

              <!-- Email and Phone Number -->
              <div class="row">
                <div class="mb-3 col-sm-6">
                  <label class="form-label">Email</label>
                  <input
                    type="email"
                    class="form-control"
                    v-model="register.email"
                    @change="checkEmail()"
                    placeholder=""
                  />
                  <small class="validation">{{ validations.email }}</small>
                </div>
                <div class="mb-3 col-sm-6">
                  <label class="form-label">Phone Number</label>
                  <input
                    type="text"
                    class="form-control"
                    v-model="register.phoneNumber"
                    @change="checkPhonenumber()"
                  />
                  <small class="validation">{{
                    validations.phoneNumber
                  }}</small>
                </div>
              </div>

              <!-- Birthdate and Gender -->
              <div class="row">
                <div class="mb-3 col-sm-6">
                  <label class="form-label">Birthdate</label>
                  <input
                    type="date"
                    class="form-control"
                    v-model="register.birthdate"
                    placeholder=""
                  />
                </div>
                <div class="mb-3 col-sm-6">
                  <label class="form-label">Gender</label>
                  <div>
                    <div class="form-check form-check-inline">
                      <label class="form-check-label" for="inlineRadioMale"
                        >Male</label
                      >
                      <input
                        id="inlineRadioMale"
                        type="radio"
                        name="gender"
                        value="1"
                        v-model="register.gender"
                        class="form-check-input"
                        checked
                      />
                    </div>

                    <div class="form-check form-check-inline">
                      <label class="form-check-label" for="inlineRadioFemale"
                        >Female</label
                      >
                      <input
                        id="inlineRadioFemale"
                        type="radio"
                        name="gender"
                        value="2"
                        v-model="register.gender"
                        class="form-check-input"
                      />
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <!-- Submit button -->
            <div class="row">
              <button
                type="submit"
                @click="authorizeAction()"
                class="btn btn-primary"
              >
                {{ btnSignInTxt }}
              </button>
            </div>
          </article>

          <br />

          <!-- Footer -->
          <footer class="card-footer text-center">
            {{ footerAnchorTagTxt }}
            <a
              v-if="authMode == 'Login'"
              @click="formSwitching()"
              class="text-decoration-underline"
              >Register</a
            >
            <a v-else @click="formSwitching()" class="text-decoration-underline"
              >Login</a
            >
          </footer>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import authMode from "@/common/enums/authMode";
import User from "@/models/user/user";
import Login from "@/models/authentication/login";
import validationUtils from "@/common/validationUtils";
import isNullOrEmpty from "@/common/stringUtils";
import store from "@/store";
import router from "@/router";
import routesNames from "@/router/routesNames";

export default {
  name: "SignIn",
  data() {
    return {
      authMode: authMode.LOGIN,
      btnSignInTxt: "Login",
      footerAnchorTagTxt: "Don't have an account? Sign up here",
      login: new Login({ username: "", password: "" }),
      register: new User({
        id: null,
        username: "",
        password: "",
        firstname: "",
        lastname: "",
        email: "",
        birthdate: "",
        phoneNumber: "",
        gender: "",
        teamId: null,
        roles: []
      }),
      validations: {
        loginUsername: "",
        loginPassword: "",
        registerUsername: "",
        registerPassword: "",
        firstname: "",
        lastname: "",
        email: "",
        phoneNumber: ""
      }
    };
  },
  methods: {
    formSwitching() {
      this.authMode =
        this.authMode === authMode.LOGIN ? authMode.REGISTER : authMode.LOGIN;
      if (this.authMode == authMode.LOGIN) {
        this.btnSignInTxt = "Login";
        this.footerAnchorTagTxt = "Don't have an account? Sign up here";
      } else {
        this.btnSignInTxt = "Sign up";
        this.footerAnchorTagTxt = "Already have an account? Sign in here";
      }
    },
    checkUsername() {
      switch (this.authMode) {
        case authMode.LOGIN:
          if (!validationUtils.isValidUsername(this.login.username)) {
            this.validations.loginUsername = "Username is invalid";
          } else {
            this.validations.loginUsername = "";
          }
          break;

        case authMode.REGISTER:
          if (!validationUtils.isValidUsername(this.register.username)) {
            this.validations.registerUsername = "Username is invalid";
          } else if (this.register.username.length == 0) {
            this.validations.registerUsername = "Username is invalid";
          } else if (this.register.username.length > 15) {
            this.validations.registerUsername =
              "Username should be less than 15 characters";
          } else {
            this.validations.registerUsername = "";
          }
          break;
      }
    },
    checkPassword() {
      switch (this.authMode) {
        case authMode.LOGIN:
          if (this.login?.password?.length == 0) {
            this.validations.loginPassword = "Password is necessary";
          } else if (this.register?.password?.length > 127) {
            this.validations.loginPassword = "Password not valid.";
          } else {
            this.validations.loginPassword = "";
          }
          break;
        case authMode.REGISTER:
          if (this.register?.password?.length == 0) {
            this.validations.registerPassword = "Password is necessary";
          } else if (this.register?.password?.length > 127) {
            this.validations.registerPassword =
              "Password should be less than 127 characters";
          } else {
            this.validations.registerPassword = "";
          }
          break;
      }
    },
    checkFirstname() {
      if (this.register.firstname.length == 0) {
        this.validations.firstname = "Firstname is necessary";
      } else if (this.register.firstname.length > 35) {
        this.validations.firstname =
          "Firstname should be less than 35 characters";
      } else {
        this.validations.firstname = "";
      }
    },
    checkLastname() {
      if (this.register.lastname.length == 0) {
        this.validations.lastname = "Lastname is necessary";
      } else if (this.register.firstname.length > 35) {
        this.validations.lastname =
          "Lastname should be less than 35 characters";
      } else {
        this.validations.lastname = "";
      }
    },
    checkEmail() {
      if (this.register.email.length == 0) {
        this.validations.email = "Email is necessary";
      } else if (!validationUtils.isValidEmail(this.register.email)) {
        this.validations.email = "Email is invalid";
      } else {
        this.validations.email = "";
      }
    },
    checkPhonenumber() {
      if (this.register.phoneNumber.length == 0) {
        this.validations.phoneNumber = "Phone Number is necessary";
      } else {
        this.validations.phoneNumber = "";
      }
    },
    ValidateForm() {
      switch (this.authMode) {
        case authMode.LOGIN:
          this.checkUsername();
          this.checkPassword();
          if (
            this.login.username == "" ||
            !isNullOrEmpty(this.validations.loginUsername)
          ) {
            return false;
          }

          if (this.login.password == "") {
            return false;
          }

          break;

        case authMode.REGISTER:
          this.checkUsername();
          this.checkPassword();
          this.checkFirstname();
          this.checkLastname();
          this.checkEmail();
          this.checkPhonenumber();

          if (
            this.register.username == "" ||
            !isNullOrEmpty(this.validations.registerUsername)
          ) {
            return false;
          }

          if (
            this.register.password == "" ||
            !isNullOrEmpty(this.validations.registerPassword)
          ) {
            return false;
          }

          if (
            this.register.firstname == "" ||
            !isNullOrEmpty(this.validations.firstname)
          ) {
            return false;
          }

          if (
            this.register.lastname == "" ||
            !isNullOrEmpty(this.validations.lastname)
          ) {
            return false;
          }

          if (
            this.register.email == "" ||
            !isNullOrEmpty(this.validations.email)
          ) {
            return false;
          }

          if (
            this.register.phoneNumber == "" ||
            !isNullOrEmpty(this.validations.phoneNumber)
          ) {
            return false;
          }
          break;
      }

      return true;
    },
    async authorizeAction() {
      if (!this.ValidateForm()) {
        this.$notify({
          type: "warn",
          title: "Validation",
          text: "Please enter all fields correctly"
        });
      } else {
        if (this.authMode == authMode.LOGIN) {
          await store.dispatch("user/loginAsync", this.login);
          router.push({ name: routesNames.dashboard });
        } else {
          await store.dispatch("user/registerAsync", this.register);
          router.push({ name: routesNames.dashboard });
        }
      }
    }
  }
};
</script>

<style scoped>
.validation {
  color: red;
}
</style>
