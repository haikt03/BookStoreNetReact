import { Button, Grid, Typography } from "@mui/material";
import { useAppSelector } from "../../../app/store/configureStore";
import { Link } from "react-router-dom";
import BasketTable from "./BasketTable";
import BasketSummary from "./BasketSummary";

export default function Basket() {
    const { basket } = useAppSelector((state) => state.basket);

    if (!basket)
        return <Typography variant="h3">Giỏ hàng đang trống</Typography>;

    return (
        <>
            <BasketTable items={basket.items} />
            <Grid container>
                <Grid item xs={6}></Grid>
                <Grid item xs={6}>
                    <BasketSummary />
                    <Button
                        component={Link}
                        to={"/checkout"}
                        variant="contained"
                        size="large"
                        fullWidth
                    >
                        Thanh toán
                    </Button>
                </Grid>
            </Grid>
        </>
    );
}
