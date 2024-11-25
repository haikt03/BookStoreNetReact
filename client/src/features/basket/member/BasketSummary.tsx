import {
    TableContainer,
    Paper,
    Table,
    TableBody,
    TableRow,
    TableCell,
} from "@mui/material";
import { currencyFormat } from "../../../app/utils/utils";
import { useAppSelector } from "../../../app/store/configureStore";

interface Props {
    subtotal?: number;
}

export default function BasketSummary({ subtotal }: Props) {
    const { basket } = useAppSelector((state) => state.basket);
    if (subtotal === undefined)
        subtotal =
            basket?.items.reduce(
                (sum, item) => sum + item.quantity * item.book.price,
                0
            ) ?? 0;
    const deliveryFee = subtotal > 200000 ? 0 : 30000;

    return (
        <>
            <TableContainer component={Paper} variant={"outlined"}>
                <Table>
                    <TableBody>
                        <TableRow>
                            <TableCell colSpan={2}>Tổng công</TableCell>
                            <TableCell align="right">
                                {currencyFormat(subtotal)}
                            </TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell colSpan={2}>Phí giao hàng*</TableCell>
                            <TableCell align="right">
                                {currencyFormat(deliveryFee)}
                            </TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell colSpan={2}>
                                Số tiền cần thanh toán
                            </TableCell>
                            <TableCell align="right">
                                {currencyFormat(subtotal + deliveryFee)}
                            </TableCell>
                        </TableRow>
                        <TableRow>
                            <TableCell>
                                <span style={{ fontStyle: "italic" }}>
                                    {`*Đơn hàng trên ${currencyFormat(
                                        200000
                                    )} sẽ được miễn phí giao hàng`}
                                </span>
                            </TableCell>
                        </TableRow>
                    </TableBody>
                </Table>
            </TableContainer>
        </>
    );
}
