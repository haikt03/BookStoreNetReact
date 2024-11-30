import { useState } from "react";
import useAuthors from "../../../app/hooks/useAuthors";
import { useAppDispatch } from "../../../app/store/configureStore";
import LoadingComponent from "../../../app/layout/LoadingComponent";
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
import AppPagination from "../../../app/components/AppPagination";
import { removeAuthor, setAuthorPageIndex } from "../authorSlice";
import { Delete, Edit } from "@mui/icons-material";
import { LoadingButton } from "@mui/lab";
import AuthorFilter from "../AuthorFilter";
import { Author } from "../../../app/models/author";
import agent from "../../../app/api/agent";
import AuthorForm from "./AuthorForm";

export default function ManageAuthor() {
    const [editMode, setEditMode] = useState(false);
    const { authors, filtersLoaded, filter, metaData } = useAuthors();
    const [selectedAuthor, setSelectedAuthor] = useState<Author | undefined>(
        undefined
    );
    const [loading, setLoading] = useState(false);
    const [target, setTarget] = useState(0);
    const dispatch = useAppDispatch();

    function handleSelectAuthor(author: Author) {
        setSelectedAuthor(author);
        setEditMode(true);
    }

    function cancelEdit() {
        if (selectedAuthor) setSelectedAuthor(undefined);
        setEditMode(false);
    }

    function handleDeleteAuthor(id: number) {
        setLoading(true);
        setTarget(id);
        agent.author
            .deleteAuthor(id)
            .then(() => dispatch(removeAuthor(id)))
            .catch((error) => console.log(error))
            .finally(() => setLoading(false));
    }

    if (editMode)
        return (
            <AuthorForm cancelEdit={cancelEdit} authorId={selectedAuthor?.id} />
        );

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
                <TableContainer component={Paper}>
                    <Table sx={{ minWidth: 650 }} aria-label="simple table">
                        <TableHead>
                            <TableRow>
                                <TableCell>#</TableCell>
                                <TableCell align="left">Tác giả</TableCell>
                                <TableCell align="center">Quốc tịch</TableCell>
                                <TableCell align="right"></TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {authors.map((author) => (
                                <TableRow
                                    key={author.id}
                                    sx={{
                                        "&:last-child td, &:last-child th": {
                                            border: 0,
                                        },
                                    }}
                                >
                                    <TableCell component="th" scope="row">
                                        {author.id}
                                    </TableCell>
                                    <TableCell align="left">
                                        <Box display="flex" alignItems="center">
                                            <img
                                                src={
                                                    author.imageUrl ||
                                                    "/images/default-author.jpg"
                                                }
                                                alt={author.fullName}
                                                style={{
                                                    height: 50,
                                                    marginRight: 20,
                                                }}
                                            />
                                            <span>{author.fullName}</span>
                                        </Box>
                                    </TableCell>
                                    <TableCell align="center">
                                        {author.country}
                                    </TableCell>
                                    <TableCell align="right">
                                        <Button
                                            startIcon={<Edit />}
                                            onClick={() =>
                                                handleSelectAuthor(author)
                                            }
                                        />
                                        <LoadingButton
                                            loading={
                                                loading && target === author.id
                                            }
                                            startIcon={<Delete />}
                                            color="error"
                                            onClick={() =>
                                                handleDeleteAuthor(author.id)
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
                            dispatch(setAuthorPageIndex({ pageIndex: page }))
                        }
                    />
                )}
            </Grid>
        </Grid>
    );
}
