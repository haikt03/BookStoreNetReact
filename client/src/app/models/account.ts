import { DetailUser } from "./user";

export interface Account extends DetailUser {
    role: string;
}
