import { debounce, TextField } from "@mui/material";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { useState } from "react";
import { setAuthorParams } from "./authorSlice";

export default function AuthorSearch() {
    const { authorParams } = useAppSelector((state) => state.author);
    const [fullNameSearch, setFullNameSearch] = useState(
        authorParams.fullNameSearch
    );
    const dispatch = useAppDispatch();

    const debouncedFullNameSearch = debounce((event: any) => {
        dispatch(setAuthorParams({ fullNameSearch: event.target.value }));
    }, 1000);

    return (
        <TextField
            label="Tìm kiếm theo tên tác giả"
            variant="outlined"
            fullWidth
            value={fullNameSearch || ""}
            onChange={(event: any) => {
                setFullNameSearch(event.target.value);
                debouncedFullNameSearch(event);
            }}
        />
    );
}
