import { useState } from "react";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { Box, debounce, Paper, TextField } from "@mui/material";
import { setOrderParams } from "./orderSlice";

export default function OrderSearch() {
    const { orderParams } = useAppSelector((state) => state.order);
    const [codeSearch, setCodeSearch] = useState(orderParams.codeSearch);
    const [userSearch, setUserSearch] = useState(orderParams.userSearch);
    const dispatch = useAppDispatch();

    const debouncedCodeSearch = debounce((event: any) => {
        dispatch(setOrderParams({ codeSearch: event.target.value }));
    }, 1000);

    const debouncedUserSearch = debounce((event: any) => {
        dispatch(setOrderParams({ userSearch: event.target.value }));
    }, 1000);

    return (
        <Box>
            <Paper sx={{ mb: 2 }}>
                <TextField
                    label="Tìm kiếm theo mã đơn hàng"
                    variant="outlined"
                    fullWidth
                    value={codeSearch || ""}
                    onChange={(event: any) => {
                        setCodeSearch(event.target.value);
                        debouncedCodeSearch(event);
                    }}
                />
            </Paper>
            <Paper sx={{ mb: 2 }}>
                <TextField
                    label="Tìm kiếm theo tên người mua"
                    variant="outlined"
                    fullWidth
                    value={userSearch || ""}
                    onChange={(event: any) => {
                        setUserSearch(event.target.value);
                        debouncedUserSearch(event);
                    }}
                />
            </Paper>
        </Box>
    );
}
