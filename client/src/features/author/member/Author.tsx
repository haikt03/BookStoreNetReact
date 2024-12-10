import useAuthors from "../../../app/hooks/useAuthors";
import { useAppDispatch } from "../../../app/store/configureStore";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { Grid } from "@mui/material";
import AppPagination from "../../../app/components/AppPagination";
import { setAuthorPageIndex } from "../authorSlice";
import AuthorList from "./AuthorList";
import AuthorFilter from "../AuthorFilter";

export default function Author() {
    const { authors, filterLoaded, filter, metaData } = useAuthors();

    const dispatch = useAppDispatch();

    if (!filterLoaded) return <LoadingComponent />;

    return (
        <Grid container columnSpacing={4}>
            <Grid item xs={3}>
                <AuthorFilter filter={filter} />
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
                            dispatch(setAuthorPageIndex({ pageIndex: page }))
                        }
                    />
                )}
                <AuthorList authors={authors} />
                {metaData && (
                    <AppPagination
                        metaData={metaData}
                        onPageChange={(page: number) =>
                            dispatch(setAuthorPageIndex({ pageIndex: page }))
                        }
                    />
                )}
            </Grid>
        </Grid>
    );
}
