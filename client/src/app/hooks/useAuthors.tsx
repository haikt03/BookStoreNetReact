import { useEffect } from "react";
import {
    authorSelectors,
    getAuthorFilterAsync,
    getAuthorsAsync,
} from "../../features/author/authorSlice";
import { useAppDispatch, useAppSelector } from "../store/configureStore";

export default function useAuthors() {
    const authors = useAppSelector(authorSelectors.selectAll);

    const { authorsLoaded, filtersLoaded, filter, metaData } = useAppSelector(
        (state) => state.author
    );
    const dispatch = useAppDispatch();

    useEffect(() => {
        if (!authorsLoaded) dispatch(getAuthorsAsync());
    }, [authorsLoaded, dispatch]);

    useEffect(() => {
        if (!filtersLoaded) dispatch(getAuthorFilterAsync());
    }, [dispatch, filtersLoaded]);

    return {
        authors,
        authorsLoaded,
        filtersLoaded,
        filter,
        metaData,
    };
}
