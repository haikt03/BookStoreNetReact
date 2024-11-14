import { Address } from "./address";

export interface Account {
    id: number;
    userName: string;
    email: string;
    emailConfirmed: boolean;
    phoneNumber: string;
    phoneNumberConfirmed: boolean;
    fullName: string;
    dateOfBirth: string;
    publicId: string | null;
    imageUrl: string | null;
    address: Address | null;
    roles: string[];
}

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

export interface UpdateMeRequest {
    userName: string | null;
    email: string | null;
    phoneNumber: string | null;
    password: string | null;
    fullName: string | null;
    dateOfBirth: string | null;
    file: File | null;
}

export interface UpdateUserAddressRequest {
    city: string | null;
    district: string | null;
    ward: string | null;
    street: string | null;
    alley: string | null;
    houseNumber: string | null;
}

export interface ChangePasswordRequest {
    currentPassword: string;
    newPassword: string;
}

export interface ConfirmEmailQuery {
    userId: string;
    token: string;
}
