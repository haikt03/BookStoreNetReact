import { Link, useNavigate, useParams } from "react-router-dom";
import {
    useAppDispatch,
    useAppSelector,
} from "../../../app/store/configureStore";
import { ChangeEvent, useEffect, useState } from "react";
import { getBookAsync } from "../bookSlice";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import NotFound from "../../../app/errors/NotFound";
import {
    Box,
    Button,
    Divider,
    Grid,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableRow,
    TextField,
    Typography,
} from "@mui/material";
import { currencyFormat } from "../../../app/utils/utils";
import { LoadingButton } from "@mui/lab";
import {
    addBasketItemAsync,
    removeBasketItemAsync,
} from "../../basket/basketSlice";

export default function BookDetail() {
    const { id } = useParams<{ id: string }>();
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const { basket, status } = useAppSelector((state) => state.basket);
    const { role } = useAppSelector((state) => state.account);

    useEffect(() => {
        dispatch(getBookAsync(parseInt(id!)));
    }, []);

    const {
        status: bookStatus,
        bookDetail,
        bookDetailLoaded,
    } = useAppSelector((state) => state.book);
    const [quantity, setQuantity] = useState(0);
    const item = basket?.items.find((i) => i.book.id === bookDetail?.id);

    useEffect(() => {
        if (item) setQuantity(item.quantity);
        if (!bookDetail && id) dispatch(getBookAsync(parseInt(id)));
    }, [id, item, bookDetail, dispatch]);

    function handleInputChange(event: ChangeEvent<HTMLInputElement>) {
        if (parseInt(event.currentTarget.value) >= 0)
            setQuantity(parseInt(event.currentTarget.value));
    }

    function handleUpdateCart() {
        if (!bookDetail) return;

        if (!item || quantity > item?.quantity) {
            const updatedQuantity = item ? quantity - item.quantity : quantity;
            dispatch(
                addBasketItemAsync({
                    bookId: bookDetail.id,
                    quantity: updatedQuantity,
                })
            );
        } else {
            const updatedQuantity = item.quantity - quantity;
            dispatch(
                removeBasketItemAsync({
                    bookId: bookDetail.id,
                    quantity: updatedQuantity,
                })
            );
        }
    }

    if (bookStatus.includes("pending") || !bookDetailLoaded)
        return <LoadingComponent />;

    if (!bookDetail) return <NotFound />;

    return (
        <Grid container spacing={6} mb={5}>
            <Grid item xs={6}>
                <img
                    src={bookDetail.imageUrl || "/images/default-book.jpg"}
                    alt={bookDetail.name}
                    style={{ width: "100%" }}
                />
                <Table>
                    <TableBody>
                        <TableRow>
                            <TableCell sx={{ whiteSpace: "nowrap" }}>
                                Mô tả
                            </TableCell>
                            <TableCell>{bookDetail.description}</TableCell>
                        </TableRow>
                    </TableBody>
                </Table>
            </Grid>
            <Grid item xs={6}>
                <Typography variant="h3" color="primary.main" mb={2}>
                    {bookDetail.name}
                </Typography>
                <Divider sx={{ mb: 2 }} />
                <Box
                    sx={{
                        display: "flex",
                        gap: 5,
                        alignItems: "center",
                    }}
                >
                    <Typography
                        gutterBottom
                        color="secondary"
                        variant="h5"
                        component="div"
                        sx={{ fontWeight: "bold", marginBottom: 1 }}
                    >
                        {currencyFormat(
                            bookDetail?.price * (1 - bookDetail.discount / 100)
                        )}
                    </Typography>
                    {bookDetail.discount > 0 && (
                        <Box
                            sx={{
                                display: "flex",
                                gap: 5,
                                alignItems: "center",
                            }}
                        >
                            <Typography
                                gutterBottom
                                color="text.secondary"
                                variant="body1"
                                component="div"
                                sx={{
                                    textDecoration: "line-through",
                                }}
                            >
                                {currencyFormat(bookDetail?.price)}
                            </Typography>

                            <Typography
                                gutterBottom
                                color="text.secondary"
                                variant="body1"
                                component="div"
                            >
                                {`- ${bookDetail.discount}%`}
                            </Typography>
                        </Box>
                    )}
                </Box>
                <TableContainer>
                    <Table>
                        <TableBody sx={{ fontSize: "1.1em" }}>
                            <TableRow>
                                <TableCell sx={{ whiteSpace: "nowrap" }}>
                                    Thể loại
                                </TableCell>
                                <TableCell>{bookDetail.category}</TableCell>
                            </TableRow>
                            <TableRow sx={{ whiteSpace: "nowrap" }}>
                                <TableCell>Tác giả</TableCell>
                                <TableCell>
                                    <Button
                                        component={Link}
                                        to={`/author/${bookDetail.author?.id}`}
                                        size="small"
                                    >
                                        {bookDetail.author?.fullName}
                                    </Button>
                                </TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell sx={{ whiteSpace: "nowrap" }}>
                                    Người dịch
                                </TableCell>
                                <TableCell>{bookDetail.translator}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell sx={{ whiteSpace: "nowrap" }}>
                                    Nhà xuất bản
                                </TableCell>
                                <TableCell>{bookDetail.publisher}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell sx={{ whiteSpace: "nowrap" }}>
                                    Năm xuất bản
                                </TableCell>
                                <TableCell>
                                    {bookDetail.publishedYear}
                                </TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell sx={{ whiteSpace: "nowrap" }}>
                                    Ngôn ngữ
                                </TableCell>
                                <TableCell>{bookDetail.language}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell sx={{ whiteSpace: "nowrap" }}>
                                    Trọng lượng
                                </TableCell>
                                <TableCell>
                                    {`${bookDetail.weight} gram`}
                                </TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell sx={{ whiteSpace: "nowrap" }}>
                                    Số trang
                                </TableCell>
                                <TableCell>
                                    {bookDetail.numberOfPages}
                                </TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell sx={{ whiteSpace: "nowrap" }}>
                                    Hình thức
                                </TableCell>
                                <TableCell>{bookDetail.form}</TableCell>
                            </TableRow>
                        </TableBody>
                    </Table>
                </TableContainer>
                {role === "Admin" ? (
                    <Button
                        onClick={() => navigate(-1)}
                        variant="outlined"
                        color="primary"
                        fullWidth
                    >
                        Quay lại
                    </Button>
                ) : (
                    <Grid container spacing={2} mt={2}>
                        <Grid item xs={6}>
                            <TextField
                                onChange={handleInputChange}
                                variant={"outlined"}
                                type={"number"}
                                label={"Số lượng"}
                                fullWidth
                                value={quantity}
                            />
                        </Grid>
                        <Grid item xs={6}>
                            <LoadingButton
                                disabled={
                                    item?.quantity === quantity ||
                                    (!item && quantity === 0)
                                }
                                loading={status.includes("pending")}
                                onClick={handleUpdateCart}
                                sx={{ height: "55px" }}
                                color={"primary"}
                                size={"large"}
                                variant={"contained"}
                                fullWidth
                            >
                                {item
                                    ? "Cập nhật số lượng"
                                    : "Thêm vào giỏ hàng"}
                            </LoadingButton>
                        </Grid>
                    </Grid>
                )}
            </Grid>
        </Grid>
    );
}
