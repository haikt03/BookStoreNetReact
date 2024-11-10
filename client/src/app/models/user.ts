import { Address } from "./address";

export interface LoginRequest {
    userName: string;
    password: string;
}

export interface RegisterRequest {
    userName: string;
    email: string;
    phoneNumber: string;
    password: string;
    fullName: string;
    dateOfBirth: string;
}

export interface LogoutRequest {
    refreshToken: string;
}

export interface RefreshRequest {
    refreshToken: string;
}

export interface User {
    id: number;
    userName: string;
    email: string;
    emailConfirmed: boolean;
    phoneNumber: string;
    phoneNumberConfirmed: boolean;
    fullName: string;
    dateOfBirth: string;
    publicId: string;
    imageUrl: string;
    address: Address;
    accessToken: string;
    roles: string[];
}
