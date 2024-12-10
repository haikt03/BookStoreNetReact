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
        filterLoaded,
        authorsForUpsert,
        authorsForUpsertLoaded,
        metaData,
    } = useAppSelector((state) => state.book);
    const dispatch = useAppDispatch();

    useEffect(() => {
        if (!booksLoaded) dispatch(getBooksAsync());
    }, [booksLoaded, dispatch]);

    useEffect(() => {
        if (!filterLoaded) dispatch(getBookFilterAsync());
    }, [dispatch, filterLoaded]);

    useEffect(() => {
        if (!authorsForUpsertLoaded) dispatch(getAuthorsForUpsertBook());
    }, [dispatch, authorsForUpsertLoaded]);

    return {
        books,
        booksLoaded,
        filter,
        filterLoaded,
        authorsForUpsert,
        authorsForUpsertLoaded,
        metaData,
    };
}
