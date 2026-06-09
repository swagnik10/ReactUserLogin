export interface User {
    userId: number;
    username: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    isActive: boolean;
    role: string;
}

export interface UserInformation {
    userId: number;
    username: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    isActive: boolean;
    createdAt: string;
    role: string;
}

export interface UpdateUserRequest {
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    isActive: boolean;
}

export interface UpdateUserRoleRequest {
    roleName: string;
}