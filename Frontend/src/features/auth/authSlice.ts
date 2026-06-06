import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import type { User } from "./types";
import { getStoredUser } from "./authStorage";

interface AuthState {
  user: User | null;
  token: string | null;
  isAuthenticated: boolean;
}
interface LoginPayload {
  user: User;
  token: string | null;
}

const storedUser = getStoredUser();
const initialState: AuthState = {
  user: storedUser,
  token: null,
  isAuthenticated: !!storedUser,
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    loginSuccess: (state, action: PayloadAction<LoginPayload>) => {
      state.user = action.payload.user;
      state.token = action.payload.token;
      state.isAuthenticated = true;
    },

    logout: (state) => {
      state.user = null;
      state.token = null;
      state.isAuthenticated = false;
    },
  },
});

export const { loginSuccess, logout } = authSlice.actions;

export default authSlice.reducer;