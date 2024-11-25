import { FilterParams } from "./pagination";
import { Author } from "./author";
import { Category } from "./category";

export interface Book {
    id: number;
    name: string;
    price: number;
    discount: number;
    quantityInStock: number;
    publicId?: string;
    imageUrl?: string;
}

export interface BookDetail extends Book {
    translator?: string;
    publisher: string;
    publishedYear: number;
    language: string;
    weight: number;
    numberOfPages: number;
    form: string;
    description: string;
    category?: Category;
    author?: Author;
}

export interface BookParams extends FilterParams {
    publishers: string[];
    languages: string[];
    categories: number[];
    authorSearch?: string;
    minPrice?: number;
    maxPrice?: number;
}
