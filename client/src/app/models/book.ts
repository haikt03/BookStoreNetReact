import { PaginationParams } from "./pagination";
import { Author } from "./author";
import { Category } from "./category";

export interface Book {
    id: number;
    name: string;
    price: number;
    discount: number;
    quantityInStock: number;
    publicId: string | null;
    imageUrl: string | null;
}

export interface DetailBook extends Book {
    translator: string | null;
    publisher: string;
    publishedYear: number;
    language: string;
    weight: number;
    numberOfPages: number;
    form: string;
    description: string;
    category: Category | null;
    author: Author | null;
}

export interface BookParams extends PaginationParams {
    search: string;
    publishers: string[];
    languages: string[];
    minPrice: number;
    maxPrice: number;
    sort: string;
}
