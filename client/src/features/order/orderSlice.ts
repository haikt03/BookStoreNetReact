import {
    createAsyncThunk,
    createEntityAdapter,
    createSlice,
} from "@reduxjs/toolkit";
import { Order, OrderDetail, OrderParams } from "../../app/models/order";
import { MetaData } from "../../app/models/pagination";
import { RootState, store } from "../../app/store/configureStore";
import agent from "../../app/api/agent";

interface OrderState {
    orderDetail: OrderDetail | null;
    orderDetailLoaded: boolean;
    ordersLoaded: boolean;
    filter: {
        paymentStatuses: string[];
        orderStatuses: string[];
        minAmount: number;
        maxAmount: number;
        orderDateStart: string | null;
        orderDateEnd: string | null;
    };
    filterLoaded: boolean;
    orderParams: OrderParams;
    metaData: MetaData | null;
    status: string;
}

interface Filter {
    paymentStatuses: string[];
    orderStatuses: string[];
    minAmount: number;
    maxAmount: number;
}

const ordersAdapter = createEntityAdapter<Order>();

const getAxiosParams = (orderParams: OrderParams) => {
    const params = new URLSearchParams();
    params.append("pageIndex", orderParams.pageIndex.toString());
    params.append("pageSize", orderParams.pageSize.toString());
    params.append("sort", orderParams.sort);
    if (orderParams.codeSearch) {
        params.append("codeSearch", orderParams.codeSearch);
    }
    if (orderParams.userSearch) {
        params.append("userSearch", orderParams.userSearch);
    }
    if (orderParams.minAmount) {
        params.append("minAmount", orderParams.minAmount.toString());
    }
    if (orderParams.maxAmount) {
        params.append("maxAmount", orderParams.maxAmount.toString());
    }
    if (orderParams.orderDateStart) {
        params.append("orderDateStart", orderParams.orderDateStart.toString());
    }
    if (orderParams.orderDateEnd) {
        params.append("orderDateEnd", orderParams.orderDateEnd.toString());
    }
    if (orderParams.paymentStatuses.length > 0)
        params.append(
            "paymentStatuses",
            orderParams.paymentStatuses.toString()
        );
    if (orderParams.orderStatuses.length > 0)
        params.append("orderStatuses", orderParams.orderStatuses.toString());
    return params;
};

export const getOrdersAsync = createAsyncThunk<
    Order[],
    void,
    { state: RootState }
>("order/getOrdersAsync", async (_, thunkAPI) => {
    try {
        const params = getAxiosParams(thunkAPI.getState().order.orderParams);
        let response;
        const role = store.getState().account.role;
        if (role === "Admin") {
            response = await agent.order.getOrders(params);
        } else if (role === "Member") {
            response = await agent.order.getOrdersByMe(params);
        }
        thunkAPI.dispatch(setOrderMetaData(response.metaData));
        return response.items;
    } catch (error: any) {
        return thunkAPI.rejectWithValue({ error: error.data });
    }
});

export const getOrderAsync = createAsyncThunk<OrderDetail, number>(
    "order/getOrderAsync",
    async (orderId, thunkAPI) => {
        try {
            const order = await agent.order.getOrder(orderId);
            return order;
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
);

export const getOrderFilterAsync = createAsyncThunk<Filter, void>(
    "order/getOrderFilterAsync",
    async (_, thunkAPI) => {
        try {
            const filter = await agent.order.getOrderFilter();
            return filter;
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
);

function initParams(): OrderParams {
    return {
        pageIndex: 1,
        pageSize: 12,
        sort: "orderDateDesc",
        paymentStatuses: [],
        orderStatuses: [],
    };
}

export const orderSlice = createSlice({
    name: "order",
    initialState: ordersAdapter.getInitialState<OrderState>({
        orderDetail: null,
        orderDetailLoaded: false,
        ordersLoaded: false,
        filter: {
            paymentStatuses: [],
            orderStatuses: [],
            minAmount: 0,
            maxAmount: 0,
            orderDateStart: null,
            orderDateEnd: null,
        },
        filterLoaded: false,
        orderParams: initParams(),
        metaData: null,
        status: "idle",
    }),
    reducers: {
        setOrderParams: (state, action) => {
            state.ordersLoaded = false;
            state.orderParams = {
                ...state.orderParams,
                ...action.payload,
                pageIndex: 1,
            };
        },
        setOrderPageIndex: (state, action) => {
            state.ordersLoaded = false;
            state.orderParams = { ...state.orderParams, ...action.payload };
        },
        setOrderMetaData: (state, action) => {
            state.metaData = action.payload;
        },
        resetOrderParams: (state) => {
            state.orderParams = initParams();
        },
        setOrder: (state, action) => {
            ordersAdapter.upsertOne(state, action.payload);
            state.ordersLoaded = false;
        },
    },
    extraReducers: (builder) => {
        builder.addCase(getOrdersAsync.pending, (state) => {
            state.status = "pendingGetOrders";
        });
        builder.addCase(getOrdersAsync.fulfilled, (state, action) => {
            ordersAdapter.setAll(state, action.payload);
            state.status = "idle";
            state.ordersLoaded = true;
        });
        builder.addCase(getOrdersAsync.rejected, (state) => {
            state.status = "idle";
        });
        builder.addCase(getOrderAsync.pending, (state) => {
            state.status = "pendingGetOrder";
        });
        builder.addCase(getOrderAsync.fulfilled, (state, action) => {
            ordersAdapter.upsertOne(state, action.payload);
            state.orderDetail = action.payload;
            state.orderDetailLoaded = true;
            state.status = "idle";
        });
        builder.addCase(getOrderAsync.rejected, (state) => {
            state.status = "idle";
        });
        builder.addCase(getOrderFilterAsync.pending, (state) => {
            state.status = "pendingGetOrderFilter";
        });
        builder.addCase(getOrderFilterAsync.fulfilled, (state, action) => {
            state.filter.paymentStatuses = action.payload.paymentStatuses;
            state.filter.orderStatuses = action.payload.orderStatuses;
            state.filter.minAmount = action.payload.minAmount;
            state.filter.maxAmount = action.payload.maxAmount;
            state.status = "idle";
            state.filterLoaded = true;
        });
        builder.addCase(getOrderFilterAsync.rejected, (state) => {
            state.status = "idle";
        });
    },
});

export const {
    setOrderMetaData,
    setOrderParams,
    setOrderPageIndex,
    resetOrderParams,
    setOrder,
} = orderSlice.actions;

export const orderSelectors = ordersAdapter.getSelectors(
    (state: RootState) => state.order
);
