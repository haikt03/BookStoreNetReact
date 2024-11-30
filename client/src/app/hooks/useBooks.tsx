import { useEffect } from "react";
import {
    bookSelectors,
    getBooksAsync,
    getBookFilterAsync,
    getAuthorsForUpsertBook,
} from "../../features/book/bookSlice";
import { useAppDispatch, useAppSelector } from "../store/configureStore";

export default function useBooks() {
    const books = useAppSelector(bookSelectors.selectAll);
    const {
        booksLoaded,
        filter,
        filtersLoaded,
        authorsForUpsert,
        authorsForUpsertLoaded,
        metaData,
    } = useAppSelector((state) => state.book);
    const dispatch = useAppDispatch();

    useEffect(() => {
        if (!booksLoaded) dispatch(getBooksAsync());
    }, [booksLoaded, dispatch]);

    useEffect(() => {
        if (!filtersLoaded) dispatch(getBookFilterAsync());
    }, [dispatch, filtersLoaded]);

    useEffect(() => {
        if (!authorsForUpsertLoaded) dispatch(getAuthorsForUpsertBook());
    }, [dispatch, authorsForUpsertLoaded]);

    return {
        books,
        booksLoaded,
        filter,
        filtersLoaded,
        authorsForUpsert,
        authorsForUpsertLoaded,
        metaData,
    };
}
