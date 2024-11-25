import { FilterParams } from "./pagination";

export interface Author {
    id: number;
    fullName: string;
    country: string;
    publicId?: string;
    imageUrl?: string;
}

export interface AuthorDetail extends Author {
    biography: string;
}

export interface AuthorParams extends FilterParams {
    countries: string[];
}
