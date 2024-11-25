import { createAsyncThunk, createSlice, isAnyOf } from "@reduxjs/toolkit";
import { Basket } from "../../app/models/basket";
import agent from "../../app/api/agent";

interface BasketState {
    basket: Basket | null;
    status: string;
}

const initialState: BasketState = {
    basket: null,
    status: "idle",
};

export const getBasketAsync = createAsyncThunk<Basket>(
    "basket/getBasketAsync",
    async (_, thunkAPI) => {
        try {
            var basket = await agent.basket.getBasket();
            return basket;
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
);

export const addBasketItemAsync = createAsyncThunk<
    Basket,
    { bookId: number; quantity?: number }
>("basket/addBasketItemAsync", async ({ bookId, quantity = 1 }, thunkAPI) => {
    try {
        var basket = await agent.basket.addBasketItem(bookId, quantity);
        return basket;
    } catch (error: any) {
        return thunkAPI.rejectWithValue({ error: error.data });
    }
});

export const removeBasketItemAsync = createAsyncThunk<
    Basket,
    { bookId: number; quantity: number; name?: string }
>("basket/removeBasketItemASync", async ({ bookId, quantity }, thunkAPI) => {
    try {
        var basket = await agent.basket.removeBasketItem(bookId, quantity);
        return basket;
    } catch (error: any) {
        return thunkAPI.rejectWithValue({ error: error.data });
    }
});

export const basketSlice = createSlice({
    name: "basket",
    initialState,
    reducers: {
        setBasket: (state, action) => {
            state.basket = action.payload;
        },
        clearBasket: (state) => {
            state.basket = null;
        },
    },
    extraReducers: (builder) => {
        builder.addCase(addBasketItemAsync.pending, (state, action) => {
            state.status = "pendingAddItem" + action.meta.arg.bookId;
        });
        builder.addCase(removeBasketItemAsync.pending, (state, action) => {
            state.status =
                "pendingRemoveItem" +
                action.meta.arg.bookId +
                action.meta.arg.name;
        });
        builder.addCase(removeBasketItemAsync.fulfilled, (state, action) => {
            const { bookId, quantity } = action.meta.arg;
            const itemIndex = state.basket?.items.findIndex(
                (i) => i.book.id === bookId
            );
            if (itemIndex === -1 || itemIndex === undefined) return;
            state.basket!.items[itemIndex].quantity -= quantity;
            if (state.basket?.items[itemIndex].quantity === 0)
                state.basket.items.splice(itemIndex, 1);
            state.status = "idle";
        });
        builder.addCase(removeBasketItemAsync.rejected, (state) => {
            state.status = "idle";
        });
        builder.addMatcher(
            isAnyOf(addBasketItemAsync.fulfilled, getBasketAsync.fulfilled),
            (state, action) => {
                state.basket = action.payload;
                state.status = "idle";
            }
        );
        builder.addMatcher(
            isAnyOf(addBasketItemAsync.rejected, getBasketAsync.rejected),
            (state) => {
                state.status = "idle";
            }
        );
    },
});

export const { setBasket, clearBasket } = basketSlice.actions;
