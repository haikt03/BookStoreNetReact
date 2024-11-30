import { Address } from "./address";
import { FilterParams } from "./pagination";

export interface User {
    id: number;
    userName: string;
    email: string;
    phoneNumber: string;
    fullName: string;
    publicId?: string;
    imageUrl?: string;
}

export interface UserDetail extends User {
    emailConfirmed: boolean;
    phoneNumberConfirmed: boolean;
    dateOfBirth: string;
    address: Address;
}

export interface UserParams extends FilterParams {}
