import { FilterParams } from "./pagination";

export interface Category {
    id: number;
    name: string;
    pId: number;
}

export interface CategoryDetail {
    id: number;
    name: string;
    pCategory: Category;
}

export interface CategoryParams extends FilterParams {
    pId: number;
}
