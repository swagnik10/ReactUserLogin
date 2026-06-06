import type { User } from "./types";

const STORAGE_KEY = "auth_user";

export const saveUser = (user: User) => {
  localStorage.setItem(
    STORAGE_KEY,
    JSON.stringify(user)
  );
};

export const getStoredUser = (): User | null => {
  const data = localStorage.getItem(STORAGE_KEY);

  if (!data) {
    return null;
  }

  return JSON.parse(data);
};

export const clearStoredUser = () => {
  localStorage.removeItem(STORAGE_KEY);
};