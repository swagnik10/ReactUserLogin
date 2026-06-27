export interface PermissionDto {
    name: string;
    category: string;
    granted: boolean;
}

export interface RoleDto {
    name: string;
    permissions: PermissionDto[];
    description: string;
}

export interface RoleSummaryDto {
    name: string;
    permissionCount: number;
    description: string;
}