import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import type { User } from "./types/types";
import { getStoredUser } from "./authStorage";
import type { LoginResponse } from "./types/auth";

interface AuthState {
  user: User | null;
  token: string | null;
  isAuthenticated: boolean;
}
// interface LoginPayload {
//   user: User;
//   token: string | null;
// }
const storedToken = localStorage.getItem("token");

const storedUser = getStoredUser();
const initialState: AuthState = {
  user: storedUser,
  token: storedToken,
  isAuthenticated: !!storedUser,
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    loginSuccess: (
      state,
      action: PayloadAction<LoginResponse>
    ) => {
      state.user = {
        userId: action.payload.userId,
        username: action.payload.username,
        firstName: action.payload.firstName,
        lastName: action.payload.lastName,
        role: action.payload.role,
        emailId: action.payload.emailId,
      };

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