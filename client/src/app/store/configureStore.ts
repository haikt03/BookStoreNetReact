import { configureStore } from "@reduxjs/toolkit";
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";
import { accountSlice } from "../../features/account/accountSlice";
import { bookSlice } from "../../features/book/bookSlice";
import { authorSlice } from "../../features/author/authorSlice";
import { basketSlice } from "../../features/basket/basketSlice";

export const store = configureStore({
    reducer: {
        account: accountSlice.reducer,
        book: bookSlice.reducer,
        author: authorSlice.reducer,
        basket: basketSlice.reducer,
    },
});

export type State = {
    loading: boolean;
    status: boolean;
    error: string | null;
};

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;
