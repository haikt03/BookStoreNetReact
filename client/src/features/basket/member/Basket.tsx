import { Button, Grid, Typography } from "@mui/material";
import {
    useAppDispatch,
    useAppSelector,
} from "../../../app/store/configureStore";
import { Link } from "react-router-dom";
import BasketTable from "./BasketTable";
import BasketSummary from "./BasketSummary";
import { useEffect } from "react";
import { getBasketAsync } from "../basketSlice";

export default function Basket() {
    const { basket } = useAppSelector((state) => state.basket);
    const dispatch = useAppDispatch();

    useEffect(() => {
        if (!basket) dispatch(getBasketAsync());
    }, []);

    if (!basket) return <Typography>Giỏ hàng đang trống</Typography>;

    return (
        <>
            <BasketTable items={basket.items} />
            {basket.items.length > 0 && (
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
            )}
        </>
    );
}
