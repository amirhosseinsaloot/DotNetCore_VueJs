function UserUpdate({
  username,
  firstname,
  lastname,
  email,
  birthdate,
  phoneNumber,
  gender,
  profilePictureId,
  teamId
}) {
  this.username = username;
  this.firstname = firstname;
  this.lastname = lastname;
  this.email = email;
  this.birthdate = birthdate;
  this.phoneNumber = phoneNumber;
  this.gender = gender;
  this.profilePictureId = profilePictureId;
  this.teamId = teamId;
}

export default UserUpdate;
