import { Remove, Add, Delete } from "@mui/icons-material";
import { LoadingButton } from "@mui/lab";
import {
    TableContainer,
    Paper,
    Table,
    TableHead,
    TableRow,
    TableCell,
    TableBody,
    Box,
    Button,
} from "@mui/material";
import { BasketItem } from "../../../app/models/basket";
import {
    useAppSelector,
    useAppDispatch,
} from "../../../app/store/configureStore";
import { removeBasketItemAsync, addBasketItemAsync } from "../basketSlice";
import { currencyFormat } from "../../../app/utils/utils";
import { Link } from "react-router-dom";

interface Props {
    items: BasketItem[];
    isBasket?: boolean;
}

export default function BasketTable({ items, isBasket = true }: Props) {
    const { status } = useAppSelector((state) => state.basket);
    const dispatch = useAppDispatch();

    return (
        <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }}>
                <TableHead>
                    <TableRow>
                        <TableCell>Sách</TableCell>
                        <TableCell align="right">Giá</TableCell>
                        <TableCell align="right">Giảm giá</TableCell>
                        <TableCell align="center">Số lượng</TableCell>
                        <TableCell align="right">Thành tiền</TableCell>
                        {isBasket && <TableCell align="right"></TableCell>}
                    </TableRow>
                </TableHead>
                <TableBody>
                    {items.map((item) => (
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
                                        style={{ height: 50, marginRight: 20 }}
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
                            <TableCell align="center">
                                {isBasket && (
                                    <Box
                                        display="flex"
                                        justifyContent="center"
                                        alignItems="center"
                                    >
                                        <LoadingButton
                                            loading={
                                                status ===
                                                "pendingRemoveItem" +
                                                    item.book.id +
                                                    "rem"
                                            }
                                            onClick={() =>
                                                dispatch(
                                                    removeBasketItemAsync({
                                                        bookId: item.book.id,
                                                        quantity: 1,
                                                        name: "rem",
                                                    })
                                                )
                                            }
                                            color="error"
                                        >
                                            <Remove />
                                        </LoadingButton>

                                        {item.quantity}

                                        <LoadingButton
                                            loading={
                                                status ===
                                                "pendingAddItem" + item.book.id
                                            }
                                            onClick={() =>
                                                dispatch(
                                                    addBasketItemAsync({
                                                        bookId: item.book.id,
                                                    })
                                                )
                                            }
                                            color="secondary"
                                        >
                                            <Add />
                                        </LoadingButton>
                                    </Box>
                                )}
                            </TableCell>
                            <TableCell align="right">
                                {currencyFormat(
                                    item.book.price *
                                        (1 - item.book.discount / 100) *
                                        item.quantity
                                )}
                            </TableCell>
                            {isBasket && (
                                <TableCell align="right">
                                    <LoadingButton
                                        loading={
                                            status ===
                                            "pendingRemoveItem" +
                                                item.book.id +
                                                "del"
                                        }
                                        onClick={() =>
                                            dispatch(
                                                removeBasketItemAsync({
                                                    bookId: item.book.id,
                                                    quantity: item.quantity,
                                                    name: "del",
                                                })
                                            )
                                        }
                                        color="error"
                                    >
                                        <Delete />
                                    </LoadingButton>
                                </TableCell>
                            )}
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}
