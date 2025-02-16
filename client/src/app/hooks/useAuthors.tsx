import { useEffect } from "react";
import {
    authorSelectors,
    getAuthorFilterAsync,
    getAuthorsAsync,
} from "../../features/author/authorSlice";
import { useAppDispatch, useAppSelector } from "../store/configureStore";

export default function useAuthors() {
    const authors = useAppSelector(authorSelectors.selectAll);

    const { authorsLoaded, filterLoaded, filter, metaData } = useAppSelector(
        (state) => state.author
    );
    const dispatch = useAppDispatch();

    useEffect(() => {
        if (!authorsLoaded) dispatch(getAuthorsAsync());
    }, [authorsLoaded, dispatch]);

    useEffect(() => {
        if (!filterLoaded) dispatch(getAuthorFilterAsync());
    }, [dispatch, filterLoaded]);

    return {
        authors,
        authorsLoaded,
        filterLoaded,
        filter,
        metaData,
    };
}
