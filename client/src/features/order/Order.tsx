import useOrders from "../../app/hooks/userOrders";
import type { Order } from "../../app/models/order";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import {
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
import OrderFilter from "./OrderFilter";
import AppPagination from "../../app/components/AppPagination";
import { setOrderPageIndex } from "./orderSlice";
import { currencyFormat } from "../../app/utils/utils";
import { Visibility } from "@mui/icons-material";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { Link } from "react-router-dom";

export default function Order() {
    const { orders, filter, filterLoaded, metaData } = useOrders();
    const dispatch = useAppDispatch();
    const { role } = useAppSelector((state) => state.account);

    if (!filterLoaded) return <LoadingComponent />;

    return (
        <Grid container columnSpacing={4}>
            <Grid item xs={3}>
                <OrderFilter filter={filter} />
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
                            dispatch(setOrderPageIndex({ pageIndex: page }))
                        }
                    />
                )}
                <TableContainer component={Paper}>
                    <Table sx={{ minWidth: 650 }} aria-label="simple table">
                        <TableHead>
                            <TableRow>
                                <TableCell>#</TableCell>
                                <TableCell align="left">Mã đơn hàng</TableCell>
                                <TableCell align="center">Tổng tiền</TableCell>
                                <TableCell align="center">
                                    TT thanh toán
                                </TableCell>
                                <TableCell align="center">
                                    TT đơn hàng
                                </TableCell>
                                <TableCell align="center">
                                    Ngày đặt hàng
                                </TableCell>
                                <TableCell align="right"></TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {orders.map((order) => (
                                <TableRow
                                    key={order.id}
                                    sx={{
                                        "&:last-child td, &:last-child th": {
                                            border: 0,
                                        },
                                    }}
                                >
                                    <TableCell component="th" scope="row">
                                        {order.id}
                                    </TableCell>
                                    <TableCell align="left">
                                        <span>{order.code}</span>
                                    </TableCell>
                                    <TableCell align="center">
                                        {currencyFormat(order.amount)}
                                    </TableCell>
                                    <TableCell align="center">
                                        {order.paymentStatus}
                                    </TableCell>
                                    <TableCell align="center">
                                        {order.orderStatus}
                                    </TableCell>
                                    <TableCell align="center">
                                        {new Date(
                                            order.orderDate
                                        ).toLocaleString("vi-VN", {
                                            hour: "2-digit",
                                            minute: "2-digit",
                                            second: "2-digit",
                                            day: "2-digit",
                                            month: "2-digit",
                                            year: "numeric",
                                        })}
                                    </TableCell>
                                    <TableCell align="right">
                                        <Button
                                            startIcon={<Visibility />}
                                            component={Link}
                                            to={
                                                role === "Admin"
                                                    ? `/manage/order/${order?.id}`
                                                    : `/order/${order?.id}`
                                            }
                                            size="small"
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
                            dispatch(setOrderPageIndex({ pageIndex: page }))
                        }
                    />
                )}
            </Grid>
        </Grid>
    );
}
