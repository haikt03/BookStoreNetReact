export interface Category {
    id: number;
    name: string;
    pId: number;
}

export interface DetailCategory {
    id: number;
    name: string;
    pCategory: Category;
}