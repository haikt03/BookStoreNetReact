import { Grid } from "@mui/material";
import useBooks from "../../../app/hooks/useBooks";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { useAppDispatch } from "../../../app/store/configureStore";
import AppPagination from "../../../app/components/AppPagination";
import { setBookPageIndex } from "../bookSlice";
import BookList from "./BookList";
import BookFilter from "../BookFilter";

export default function Book() {
    const { books, filtersLoaded, filter, metaData } = useBooks();
    const dispatch = useAppDispatch();

    if (!filtersLoaded) return <LoadingComponent />;

    return (
        <Grid container columnSpacing={4}>
            <Grid item xs={3}>
                <BookFilter filter={filter} />
            </Grid>
            <Grid
                item
                xs={9}
                sx={{ display: "flex", flexDirection: "column", gap: 2 }}
            >
                {metaData && (
                    <AppPagination
                        metaData={metaData}
                        onPageChange={(page: number) =>
                            dispatch(setBookPageIndex({ pageIndex: page }))
                        }
                    />
                )}
                <BookList books={books} />
                {metaData && (
                    <AppPagination
                        metaData={metaData}
                        onPageChange={(page: number) =>
                            dispatch(setBookPageIndex({ pageIndex: page }))
                        }
                    />
                )}
            </Grid>
        </Grid>
    );
}
