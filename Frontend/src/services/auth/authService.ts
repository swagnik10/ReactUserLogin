import apiClient  from "../apiClient";
import type {
    LoginRequest,
    LoginResponse,
    RegisterRequest
} from "../../features/auth/types/auth";
import axios from "axios";

export const login = async (
  data: LoginRequest
): Promise<LoginResponse> => {
  try {
    const response = await apiClient.post<LoginResponse>(
      "/Auth/login",
      data
    );

    return response.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      throw new Error(
        error.response?.data?.Message ||
        error.response?.data?.message ||
        "Login failed"
      );
    }

    throw new Error("Login failed");
  }
};

export const register = async (
    data: RegisterRequest
): Promise<void> => {
    try{
        
        await apiClient.post("/Auth/register", data);
    }
    catch (error) {
        if (axios.isAxiosError(error)) {
            throw new Error(
                error.response?.data?.Message ||
                error.response?.data?.message ||
                "Registration failed"
            );
        }

        throw new Error("Registration failed");
    }
};