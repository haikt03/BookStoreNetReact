import { Address } from "./address";
import { FilterParams } from "./pagination";

export interface User {
    id: number;
    fullName: string;
    userName: string;
    email: string;
    phoneNumber: string;
    publicId: string;
    imageUrl: string;
}

export interface UserDetail extends User {
    dateOfBirth: string;
    emailConfirmed: boolean;
    phoneNumberConfirmed: boolean;
    address: Address;
}

export interface UserParams extends FilterParams {
    fullNameSearch?: string;
    emailSearch?: string;
    phoneNumberSearch?: string;
}
