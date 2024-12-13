import { Link, useNavigate, useParams } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { useEffect } from "react";
import { getOrderAsync } from "./orderSlice";
import LoadingComponent from "../../app/layout/LoadingComponent";
import NotFound from "../../app/errors/NotFound";
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
    TableHead,
    Box,
} from "@mui/material";
import { currencyFormat } from "../../app/utils/utils";

export default function OrderDetail() {
    const { id } = useParams<{ id: string }>();
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const { role } = useAppSelector((state) => state.account);

    useEffect(() => {
        dispatch(getOrderAsync(parseInt(id!)));
    }, [id, dispatch]);

    const { status, orderDetail, orderDetailLoaded } = useAppSelector(
        (state) => state.order
    );

    if (status.includes("pending") || !orderDetailLoaded)
        return <LoadingComponent />;

    if (!orderDetail) return <NotFound />;

    return (
        <Grid container spacing={6} mb={5}>
            <Grid item xs={12}>
                <Typography variant="h3" color="primary.main" mb={2}>
                    Đơn hàng #{orderDetail.code}
                </Typography>
                <Divider sx={{ mb: 2 }} />
                <TableContainer>
                    <Table>
                        <TableBody>
                            <TableRow>
                                <TableCell sx={{ whiteSpace: "nowrap" }}>
                                    Ngày đặt hàng
                                </TableCell>
                                <TableCell>
                                    {new Date(
                                        orderDetail.orderDate
                                    ).toLocaleString("vi-VN", {
                                        hour: "2-digit",
                                        minute: "2-digit",
                                        second: "2-digit",
                                        day: "2-digit",
                                        month: "2-digit",
                                        year: "numeric",
                                    })}
                                </TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell sx={{ whiteSpace: "nowrap" }}>
                                    Trạng thái thanh toán
                                </TableCell>
                                <TableCell>
                                    {orderDetail.paymentStatus}
                                </TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell sx={{ whiteSpace: "nowrap" }}>
                                    Trạng thái đơn hàng
                                </TableCell>
                                <TableCell>{orderDetail.orderStatus}</TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell sx={{ whiteSpace: "nowrap" }}>
                                    Thành tiền
                                </TableCell>
                                <TableCell>
                                    {currencyFormat(orderDetail.amount)}
                                </TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell sx={{ whiteSpace: "nowrap" }}>
                                    Địa chỉ giao hàng
                                </TableCell>
                                <TableCell>
                                    {
                                        orderDetail.shippingAddress
                                            .specificAddress
                                    }
                                    , {orderDetail.shippingAddress.ward},{" "}
                                    {orderDetail.shippingAddress.district},{" "}
                                    {orderDetail.shippingAddress.city}
                                </TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell sx={{ whiteSpace: "nowrap" }}>
                                    Tiền sách
                                </TableCell>
                                <TableCell>
                                    {currencyFormat(orderDetail.subtotal)}
                                </TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell sx={{ whiteSpace: "nowrap" }}>
                                    Phí giao hàng
                                </TableCell>
                                <TableCell>
                                    {currencyFormat(orderDetail.deliveryFee)}
                                </TableCell>
                            </TableRow>
                            <TableRow>
                                <TableCell sx={{ whiteSpace: "nowrap" }}>
                                    Payment Intent ID
                                </TableCell>
                                <TableCell>
                                    {orderDetail.paymentIntentId}
                                </TableCell>
                            </TableRow>
                            {role === "Admin" && orderDetail.user && (
                                <>
                                    <TableRow>
                                        <TableCell
                                            sx={{ whiteSpace: "nowrap" }}
                                        >
                                            Người đặt hàng
                                        </TableCell>
                                        <TableCell>
                                            {orderDetail.user.fullName}
                                        </TableCell>
                                    </TableRow>
                                    <TableRow>
                                        <TableCell
                                            sx={{ whiteSpace: "nowrap" }}
                                        >
                                            Email
                                        </TableCell>
                                        <TableCell>
                                            {orderDetail.user.email}
                                        </TableCell>
                                    </TableRow>
                                    <TableRow>
                                        <TableCell
                                            sx={{ whiteSpace: "nowrap" }}
                                        >
                                            Số điện thoại
                                        </TableCell>
                                        <TableCell>
                                            {orderDetail.user.phoneNumber}
                                        </TableCell>
                                    </TableRow>
                                </>
                            )}
                        </TableBody>
                    </Table>
                </TableContainer>
            </Grid>

            <Grid item xs={12}>
                <Typography variant="h5" color="primary.main" mb={2}>
                    Danh sách đã mua
                </Typography>
                <TableContainer>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Sách</TableCell>
                                <TableCell align="right">Giá</TableCell>
                                <TableCell align="right">Giảm giá</TableCell>
                                <TableCell align="center">Số lượng</TableCell>
                                <TableCell align="right">Thành tiền</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {orderDetail.items.map((item) => (
                                <TableRow
                                    key={item.book.id}
                                    sx={{
                                        "&:last-child td, &:last-child th": {
                                            border: 0,
                                        },
                                    }}
                                >
                                    <TableCell component="th" scope="row">
                                        <Box display="flex" alignItems="center">
                                            <img
                                                src={
                                                    item.book.imageUrl ||
                                                    "/images/default-book.jpg"
                                                }
                                                alt={item.book.name}
                                                style={{
                                                    height: 50,
                                                    marginRight: 20,
                                                }}
                                            />
                                            <Button
                                                component={Link}
                                                to={`/book/${item.book.id}`}
                                                size="small"
                                            >
                                                {item.book.name}
                                            </Button>
                                        </Box>
                                    </TableCell>
                                    <TableCell align="right">
                                        {currencyFormat(item.book.price)}
                                    </TableCell>
                                    <TableCell align="right">
                                        {`${item.book.discount}%`}
                                    </TableCell>
                                    <TableCell align="right">
                                        {item.quantity}
                                    </TableCell>
                                    <TableCell align="right">
                                        {currencyFormat(
                                            item.book.price *
                                                (1 - item.book.discount / 100) *
                                                item.quantity
                                        )}
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            </Grid>

            <Grid item xs={12}>
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
            </Grid>
        </Grid>
    );
}
