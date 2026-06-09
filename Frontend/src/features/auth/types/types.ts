export type UserRole = "Admin" | "User";

export interface User {
    userId: number;
    username: string;
    firstName: string;
    lastName: string;
    role: string;
    emailId: string;
}
export interface LoginRequest {
    email: string;
    password: string;
}

export interface RegisterRequest {
    username: string;
    email: string;
    password: string;
    phoneNumber: string;
}