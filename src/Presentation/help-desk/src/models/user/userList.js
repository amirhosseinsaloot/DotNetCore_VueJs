function UserList({
  id,
  username,
  firstname,
  lastname,
  email,
  birthdate,
  phoneNumber,
  gender,
  teamId,
  isActive
}) {
  this.id = id;
  this.username = username;
  this.firstname = firstname;
  this.lastname = lastname;
  this.email = email;
  this.birthdate = birthdate;
  this.phoneNumber = phoneNumber;
  this.gender = gender;
  this.teamId = teamId;
  this.isActive = isActive;
}

export default UserList;
