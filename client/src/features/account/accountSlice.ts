import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import agent from "../../app/api/agent";
import Cookies from "js-cookie";
import { State, store } from "../../app/store/configureStore";
import { FieldValues } from "react-hook-form";
import { UserDetail } from "../../app/models/user";
import { getBasketAsync } from "../basket/basketSlice";

interface AccountState {
    user: UserDetail | null;
    role: string | null;
    loginStatus: boolean;
    registerStatus: boolean;
    logoutState: State;
}

const initialState: AccountState = {
    user: null,
    role: null,
    loginStatus: false,
    registerStatus: false,
    logoutState: {
        loading: false,
        status: false,
    },
};

export const loginAsync = createAsyncThunk<UserDetail, FieldValues>(
    "account/login",
    async (data, thunkAPI) => {
        try {
            const user = await agent.account.login(data);
            localStorage.setItem("user", JSON.stringify(user));
            thunkAPI.dispatch(setRole());
            if (store.getState().account.role === "Member") {
                thunkAPI.dispatch(getBasketAsync());
            }
            return user;
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
);

export const registerAsync = createAsyncThunk<void, FieldValues>(
    "account/register",
    async (data, thunkAPI) => {
        try {
            await agent.account.register(data);
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
);

export const logoutAsync = createAsyncThunk<void>(
    "account/logout",
    async (_, thunkAPI) => {
        try {
            const refreshToken = Cookies.get("refreshToken");
            if (refreshToken) {
                await agent.account.logout({ refreshToken });
            }
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
);

export const refreshAsync = createAsyncThunk<void, FieldValues>(
    "account/refresh",
    async (data, thunkAPI) => {
        try {
            await agent.account.refresh(data);
            thunkAPI.dispatch(setRole());
            if (store.getState().account.role === "Member") {
                thunkAPI.dispatch(getBasketAsync());
            }
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
);

export const getCurrentUserAsync = createAsyncThunk<UserDetail>(
    "account/getCurrentUser",
    async (_, thunkAPI) => {
        const storedUser = localStorage.getItem("user");
        if (storedUser) {
            thunkAPI.dispatch(setUser(JSON.parse(storedUser)));
        }
        try {
            const user = await agent.account.getCurrentUser();
            localStorage.setItem("user", JSON.stringify(user));
            thunkAPI.dispatch(setRole());
            if (store.getState().account.role === "Member") {
                thunkAPI.dispatch(getBasketAsync());
            }
            return user;
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    },
    {
        condition: () => {
            if (!localStorage.getItem("user")) return false;
        },
    }
);

export const accountSlice = createSlice({
    name: "account",
    initialState,
    reducers: {
        setUser: (state, action) => {
            state.user = {
                ...state.user,
                ...action.payload,
            };
        },
        setRole: (state) => {
            const accessToken = Cookies.get("accessToken");
            if (accessToken) {
                const claims = JSON.parse(atob(accessToken.split(".")[1]));
                const role =
                    claims[
                        "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                    ];
                localStorage.setItem("role", role);
                state.role = role;
            } else {
                state.role = null;
            }
        },
        resetLoginStatus: (state) => {
            state.loginStatus = false;
        },
        resetRegisterStatus: (state) => {
            state.registerStatus = false;
        },
        resetLogoutState: (state) => {
            state.logoutState = initialState.logoutState;
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(logoutAsync.pending, (state) => {
                state.logoutState.loading = true;
            })
            .addCase(loginAsync.fulfilled, (state, action) => {
                state.user = { ...state.user, ...action.payload };
                state.loginStatus = true;
            })
            .addCase(registerAsync.fulfilled, (state) => {
                state.registerStatus = true;
            })
            .addCase(logoutAsync.fulfilled, (state) => {
                state.user = null;
                state.role = null;
                state.logoutState.loading = false;
                state.logoutState.status = true;
                localStorage.removeItem("user");
                localStorage.removeItem("role");
            })
            .addCase(getCurrentUserAsync.fulfilled, (state, action) => {
                state.user = { ...state.user, ...action.payload };
            })
            .addCase(logoutAsync.rejected, (state) => {
                state.logoutState.loading = false;
            })
            .addCase(refreshAsync.rejected, (state) => {
                state.user = null;
            });
    },
});

export const {
    setUser,
    setRole,
    resetLoginStatus,
    resetRegisterStatus,
    resetLogoutState,
} = accountSlice.actions;
