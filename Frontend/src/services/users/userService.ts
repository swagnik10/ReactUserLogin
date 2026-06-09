import apiClient from "../apiClient";
import type {
    User,
    UserInformation,
    UpdateUserRequest,
    UpdateUserRoleRequest
} from "../../features/auth/types/user";

export const getUsers = async (): Promise<User[]> => {
    const response = await apiClient.get<User[]>("/Users");
    return response.data;
};

export const getUserById = async (
    id: number
): Promise<UserInformation> => {
    const response = await apiClient.get<UserInformation>(
        `/Users/${id}`
    );

    return response.data;
};

export const updateUser = async (
  id: number,
  data: UpdateUserRequest
): Promise<void> => {
  await apiClient.put(
    `/Users/${id}`,
    data
  );
};

export const deleteUser = async (
  id: number
): Promise<void> => {
  await apiClient.delete(`/Users/${id}`);
};

export const updateUserRole = async (
    id: number,
    data: UpdateUserRoleRequest
): Promise<void> => {
    await apiClient.put(
        `/Users/${id}/role`,
        data
    );
};

