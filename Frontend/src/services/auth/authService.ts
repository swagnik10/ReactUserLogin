import apiClient  from "../apiClient";
import type {
    LoginRequest,
    LoginResponse,
    RegisterRequest
} from "../../features/auth/types/auth";

export const login = async (
    data: LoginRequest
): Promise<LoginResponse> => {
    const response = await apiClient.post<LoginResponse>(
        "/Auth/login",
        data
    );

    return response.data;
};

export const register = async (
    data: RegisterRequest
): Promise<void> => {
    await apiClient.post("/Auth/register", data);
};