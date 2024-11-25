import { debounce, TextField } from "@mui/material";
import {
    useAppDispatch,
    useAppSelector,
} from "../../../app/store/configureStore";
import { useState } from "react";
import { setAuthorParams } from "../authorSlice";

export default function AuthorSearch() {
    const { authorParams } = useAppSelector((state) => state.author);
    const [search, setSearch] = useState(authorParams.search);
    const dispatch = useAppDispatch();

    const debouncedSearch = debounce((event: any) => {
        dispatch(setAuthorParams({ search: event.target.value }));
    }, 1000);

    return (
        <TextField
            label="Tìm kiếm theo tên tác giả"
            variant="outlined"
            fullWidth
            value={search || ""}
            onChange={(event: any) => {
                setSearch(event.target.value);
                debouncedSearch(event);
            }}
        />
    );
}
