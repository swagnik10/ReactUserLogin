export interface LoginRequest {
    email: string;
    password: string;
}

export interface LoginResponse {
    token: string;
    userId: number;
    username: string;
    firstName: string;
    lastName: string;
    role: string;
    emailId: string;
}

export interface RegisterRequest {
    username: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    password: string;
}