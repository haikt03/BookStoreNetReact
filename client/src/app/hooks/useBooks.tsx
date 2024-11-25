import { useEffect } from "react";
import {
    bookSelectors,
    getBooksAsync,
    getBookFilterAsync,
} from "../../features/book/bookSlice";
import { useAppDispatch, useAppSelector } from "../store/configureStore";

export default function useBooks() {
    const books = useAppSelector(bookSelectors.selectAll);
    const { booksLoaded, filtersLoaded, filter, metaData } = useAppSelector(
        (state) => state.book
    );
    const dispatch = useAppDispatch();

    useEffect(() => {
        if (!booksLoaded) dispatch(getBooksAsync());
    }, [booksLoaded, dispatch]);

    useEffect(() => {
        if (!filtersLoaded) dispatch(getBookFilterAsync());
    }, [dispatch, filtersLoaded]);

    return {
        books,
        booksLoaded,
        filtersLoaded,
        filter,
        metaData,
    };
}
