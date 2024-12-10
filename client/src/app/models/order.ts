import { Address } from "./address";
import { Book } from "./book";
import { FilterParams } from "./pagination";
import { User } from "./user";

export interface OrderItem {
    id: number;
    quantity: number;
    book: Book;
}

export interface Order {
    id: number;
    code: string;
    amount: number;
    paymentStatus: string;
    orderStatus: string;
    orderDate: string;
    user: User;
}

export interface OrderDetail extends Order {
    subtotal: number;
    deliveryFee: number;
    paymentIntentId: string;
    shippingAddress: Address;
    items: OrderItem[];
}

export interface OrderParams extends FilterParams {
    paymentStatuses: string[];
    orderStatuses: string[];
    codeSearch?: string;
    userSearch?: string;
    minAmount?: number;
    maxAmount?: number;
    orderDateStart?: string;
    orderDateEnd?: string;
}
