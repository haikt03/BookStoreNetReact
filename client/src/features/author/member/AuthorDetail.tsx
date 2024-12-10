import { useNavigate, useParams } from "react-router-dom";
import {
    useAppDispatch,
    useAppSelector,
} from "../../../app/store/configureStore";
import { useEffect } from "react";
import { getAuthorAsync } from "../authorSlice";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import NotFound from "../../../app/errors/NotFound";
import {
    Button,
    Divider,
    Grid,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableRow,
    Typography,
} from "@mui/material";
import { setBookParams } from "../../book/bookSlice";

export default function AuthorDetail() {
    const { id } = useParams<{ id: string }>();
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const { role } = useAppSelector((state) => state.account);

    useEffect(() => {
        dispatch(getAuthorAsync(parseInt(id!)));
    }, []);

    const {
        status: authorStatus,
        authorDetail,
        authorDetailLoaded,
    } = useAppSelector((state) => state.author);

    if (authorStatus.includes("pending") || !authorDetailLoaded)
        return <LoadingComponent />;

    if (!authorDetail) return <NotFound />;

    return (
        <Grid container spacing={6} mb={5}>
            <Grid item xs={6}>
                <img
                    src={authorDetail.imageUrl || "/images/default-author.jpg"}
                    alt={authorDetail.fullName}
                    style={{ width: "100%" }}
                />
            </Grid>
            <Grid item xs={6}>
                <Typography variant="h3" color="primary.main" mb={2}>
                    {authorDetail.fullName}
                </Typography>
                <Divider sx={{ mb: 2 }} />
                <TableContainer>
                    <Table>
                        <TableBody sx={{ fontSize: "1.1em" }}>
                            <TableRow>
                                <TableCell sx={{ whiteSpace: "nowrap" }}>
                                    Quốc tịch
                                </TableCell>
                                <TableCell>{authorDetail.country}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell sx={{ whiteSpace: "nowrap" }}>
                                    Tiểu sử
                                </TableCell>
                                <TableCell>{authorDetail.biography}</TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
                </TableContainer>
                {role === "Admin" && (
                    <Button
                        size="large"
                        variant="outlined"
                        color="secondary"
                        onClick={() => navigate(-1)}
                        sx={{ mr: 2 }}
                    >
                        Quay lại
                    </Button>
                )}
                <Button
                    size="large"
                    variant="contained"
                    onClick={() => {
                        navigate(role === "Admin" ? "/manage/book" : "/book");
                        dispatch(
                            setBookParams({
                                authorSearch: authorDetail.fullName,
                            })
                        );
                    }}
                >
                    Khám phá sách của tác giả
                </Button>
            </Grid>
        </Grid>
    );
}
