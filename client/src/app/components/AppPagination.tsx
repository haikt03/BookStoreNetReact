import { Box,  Pagination } from "@mui/material";
import { MetaData } from "../models/pagination";
import { useState } from "react";

interface Props {
    metaData: MetaData;
    onPageChange: (page: number) => void;
}

export default function AppPagination({ metaData, onPageChange }: Props) {
    const { pageIndex, totalPages } = metaData;
    const [pageNumber, setPageNumber] = useState(pageIndex);

    function handlePageChange(page: number) {
        setPageNumber(page);
        onPageChange(page);
    }

    return (
        <Box display="flex" justifyContent="center" alignItems="center">
            <Pagination
                color="secondary"
                size="large"
                count={totalPages}
                page={pageNumber}
                onChange={(_e, page) => handlePageChange(page)}
            />
        </Box>
    );
}
