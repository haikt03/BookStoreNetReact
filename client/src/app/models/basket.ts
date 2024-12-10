import { Book } from "./book";

export interface BasketItem {
    id: number;
    quantity: number;
    book: Book;
}

export interface Basket {
    id: number;
    paymentIntentId: string;
    clientSecret: string;
    items: BasketItem[];
}
