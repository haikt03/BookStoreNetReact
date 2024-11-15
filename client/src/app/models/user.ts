import { Address } from "./address";
import { PaginationParams } from "./pagination";

export interface User {
    id: number;
    userName: string;
    email: string;
    phoneNumber: string;
    fullName: string;
    publicId: string | null;
    imageUrl: string | null;
}

export interface DetailUser extends User {
    emailConfirmed: boolean;
    phoneNumberConfirmed: boolean;
    dateOfBirth: string;
    address: Address | null;
}

export interface UserParams extends PaginationParams {
    search: string;
    sort: string;
}

export interface UpdateUserRequest {
    userName: string | null;
    email: string | null;
    phoneNumber: string | null;
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
