import {
    Box,
    Button,
    Grid,
    Paper,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
} from "@mui/material";
import { useState } from "react";
import { currencyFormat } from "../../../app/utils/utils";
import { LoadingButton } from "@mui/lab";
import AppPagination from "../../../app/components/AppPagination";
import useBooks from "../../../app/hooks/useBooks";
import { useAppDispatch } from "../../../app/store/configureStore";
import { Delete, Edit } from "@mui/icons-material";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { removeBook, setBookPageIndex } from "../bookSlice";
import BookFilter from "../BookFilter";
import { Book } from "../../../app/models/book";
import agent from "../../../app/api/agent";
import BookForm from "./BookForm";

export default function ManageBook() {
    const [editMode, setEditMode] = useState(false);
    const { books, filtersLoaded, filter, metaData } = useBooks();
    const [selectedBook, setSelectedBook] = useState<Book | undefined>(
        undefined
    );
    const [loading, setLoading] = useState(false);
    const [target, setTarget] = useState(0);
    const dispatch = useAppDispatch();

    function handleSelectBook(book: Book) {
        setSelectedBook(book);
        setEditMode(true);
    }

    function cancelEdit() {
        if (selectedBook) setSelectedBook(undefined);
        setEditMode(false);
    }

    function handleDeleteBook(id: number) {
        setLoading(true);
        setTarget(id);
        agent.book
            .deleteBook(id)
            .then(() => dispatch(removeBook(id)))
            .catch((error) => console.log(error))
            .finally(() => setLoading(false));
    }

    if (editMode)
        return <BookForm cancelEdit={cancelEdit} bookId={selectedBook?.id} />;

    if (!filtersLoaded) return <LoadingComponent />;

    return (
        <Grid container columnSpacing={4}>
            <Grid item xs={3}>
                <Button
                    size="large"
                    variant="contained"
                    onClick={() => setEditMode(true)}
                    sx={{ mb: 2 }}
                >
                    Thêm mới
                </Button>
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
                <TableContainer component={Paper}>
                    <Table sx={{ minWidth: 650 }} aria-label="simple table">
                        <TableHead>
                            <TableRow>
                                <TableCell>#</TableCell>
                                <TableCell align="left">Sách</TableCell>
                                <TableCell align="center">Giá</TableCell>
                                <TableCell align="center">Giảm giá</TableCell>
                                <TableCell align="center">
                                    Số lượng trong kho
                                </TableCell>
                                <TableCell align="right"></TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {books.map((book) => (
                                <TableRow
                                    key={book.id}
                                    sx={{
                                        "&:last-child td, &:last-child th": {
                                            border: 0,
                                        },
                                    }}
                                >
                                    <TableCell component="th" scope="row">
                                        {book.id}
                                    </TableCell>
                                    <TableCell align="left">
                                        <Box display="flex" alignItems="center">
                                            <img
                                                src={
                                                    book.imageUrl ||
                                                    "/images/default-book.jpg"
                                                }
                                                alt={book.name}
                                                style={{
                                                    height: 50,
                                                    marginRight: 20,
                                                }}
                                            />
                                            <span>{book.name}</span>
                                        </Box>
                                    </TableCell>
                                    <TableCell align="center">
                                        {currencyFormat(book.price)}
                                    </TableCell>
                                    <TableCell align="center">
                                        {book.discount}
                                    </TableCell>
                                    <TableCell align="center">
                                        {book.quantityInStock}
                                    </TableCell>
                                    <TableCell align="right">
                                        <Button
                                            startIcon={<Edit />}
                                            onClick={() =>
                                                handleSelectBook(book)
                                            }
                                        />
                                        <LoadingButton
                                            loading={
                                                loading && target === book.id
                                            }
                                            startIcon={<Delete />}
                                            color="error"
                                            onClick={() =>
                                                handleDeleteBook(book.id)
                                            }
                                        />
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
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
