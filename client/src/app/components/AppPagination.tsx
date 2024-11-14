import { Box, Typography, Pagination } from "@mui/material";
import { MetaData } from '../models/pagination';
import { useState } from 'react';

interface Props {
    metaData: MetaData,
    onPageChange: (page: number) => void;
}

export default function AppPagination({metaData, onPageChange}: Props) {
    const {pageSize, pageIndex, totalCount, totalPages} = metaData;
    const [pageNumber, setPageNumber] = useState(pageIndex);

    function handlePageChange(page: number) {
        setPageNumber(page);
        onPageChange(page);
    }

    return (
        <Box display='flex' justifyContent='space-between' alignItems='center' sx={{ marginBottom: 3 }}>
            <Typography variant='body1'>
                Displaying {(pageIndex-1)*pageSize+1}-
                    {pageIndex*pageSize > totalCount!
                        ? totalCount
                        : pageIndex * pageSize
                    } of {totalCount} results
            </Typography>
            <Pagination
                color='secondary'
                size='large'
                count={totalPages}
                page={pageNumber}
                onChange={(_e, page) => handlePageChange(page)}
            />
        </Box>
    )
}