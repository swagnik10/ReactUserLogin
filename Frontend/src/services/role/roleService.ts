import apiClient from "../apiClient";
import type { RoleDto, RoleSummaryDto } from "../../types/role";

export const getRoles = async (): Promise<RoleSummaryDto[]> => {
    const response = await apiClient.get<RoleSummaryDto[]>("/roles");

    return response.data;
};

export const getRole = async (
    roleName: string
): Promise<RoleDto> => {
    const response = await apiClient.get<RoleDto>(
        `/roles/${roleName}`
    );

    return response.data;
};