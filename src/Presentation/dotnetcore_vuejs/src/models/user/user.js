function User({
  id,
  username,
  firstname,
  lastname,
  email,
  birthdate,
  phoneNumber,
  gender,
  roles,
  teamId,
  profilePictureId,
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
  this.profilePictureId = profilePictureId;
  this.roles = roles;
  this.isActive = isActive;
}

export default User;
