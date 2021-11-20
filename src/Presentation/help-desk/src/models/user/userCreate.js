function UserCreate({
  username,
  password,
  firstname,
  lastname,
  email,
  birthdate,
  phoneNumber,
  gender,
  teamId
}) {
  this.username = username;
  this.password = password;
  this.firstname = firstname;
  this.lastname = lastname;
  this.email = email;
  this.birthdate = birthdate;
  this.phoneNumber = phoneNumber;
  this.gender = gender;
  this.teamId = teamId;
}

export default UserCreate;
