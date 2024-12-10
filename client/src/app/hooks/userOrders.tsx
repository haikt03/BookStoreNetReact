import { useEffect } from "react";
import {
    getOrderFilterAsync,
    getOrdersAsync,
    orderSelectors,
} from "../../features/order/orderSlice";
import { useAppDispatch, useAppSelector } from "../store/configureStore";

export default function useOrders() {
    const orders = useAppSelector(orderSelectors.selectAll);
    const { ordersLoaded, filter, filterLoaded, metaData } = useAppSelector(
        (state) => state.order
    );
    const dispatch = useAppDispatch();

    useEffect(() => {
        if (!ordersLoaded) dispatch(getOrdersAsync());
    }, [ordersLoaded, dispatch]);

    useEffect(() => {
        if (!filterLoaded) dispatch(getOrderFilterAsync());
    }, [dispatch, filterLoaded]);

    return {
        orders,
        ordersLoaded,
        filter,
        filterLoaded,
        metaData,
    };
}
