import { Box, debounce, Paper, TextField } from "@mui/material";
import {
    useAppDispatch,
    useAppSelector,
} from "../../../app/store/configureStore";
import { useState } from "react";
import { setBookParams } from "../bookSlice";

export default function BookSearch() {
    const { bookParams } = useAppSelector((state) => state.book);
    const [search, setSearch] = useState(bookParams.search);
    const [authorSearch, setAuthorSearch] = useState(bookParams.authorSearch);
    const dispatch = useAppDispatch();

    const debouncedSearch = debounce((event: any) => {
        dispatch(setBookParams({ search: event.target.value }));
    }, 1000);

    const debouncedAuthorSearch = debounce((event: any) => {
        dispatch(setBookParams({ authorSearch: event.target.value }));
    }, 1000);

    return (
        <Box sx={{ mb: 2 }}>
            <Paper sx={{ mb: 2 }}>
                <TextField
                    label="Tìm kiếm theo tên sách"
                    variant="outlined"
                    fullWidth
                    value={search || ""}
                    onChange={(event: any) => {
                        setSearch(event.target.value);
                        debouncedSearch(event);
                    }}
                />
            </Paper>
            <Paper>
                <TextField
                    label="Tìm kiếm theo tên tác giả"
                    variant="outlined"
                    fullWidth
                    value={authorSearch || ""}
                    onChange={(event: any) => {
                        setAuthorSearch(event.target.value);
                        debouncedAuthorSearch(event);
                    }}
                />
            </Paper>
        </Box>
    );
}
