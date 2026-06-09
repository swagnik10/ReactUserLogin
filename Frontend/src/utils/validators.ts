export const isValidEmail = (email: string) => {
  return /\S+@\S+\.\S+/.test(email);
};

export const isValidPassword = (password: string) => {
  return password.length >= 6;
};

export const isValidUsername = (username: string) => {
  return username.trim().length >= 3;
};

export const isValidPhoneNumber = (phoneNumber: string) => {
  return phoneNumber.trim().length === 10;
};