import { useAppSelector } from "../../app/store/configureStore";
import { Typography, Grid } from "@mui/material";
import BasketTable from "../basket/member/BasketTable";
import BasketSummary from "../basket/member/BasketSummary";

export default function Review() {
    const { basket } = useAppSelector((state) => state.basket);
    return (
        <>
            <Typography variant="h6" gutterBottom color="primary.light">
                Thông tin đơn hàng
            </Typography>
            {basket && <BasketTable items={basket.items} isBasket={false} />}
            <Grid container>
                <Grid item xs={6} />
                <Grid item xs={6}>
                    <BasketSummary />
                </Grid>
            </Grid>
        </>
    );
}
