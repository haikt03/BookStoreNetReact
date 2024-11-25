import { createAsyncThunk, createSlice, isAnyOf } from "@reduxjs/toolkit";
import { Account } from "../../app/models/account";
import agent from "../../app/api/agent";
import Cookies from "js-cookie";
import { State } from "../../app/store/configureStore";
import { FieldValues } from "react-hook-form";

interface AccountState {
    user: Account | null;
    accessToken: string | null;
    isAuthenticated: boolean;
    loginStatus: boolean;
    registerStatus: boolean;
    logoutStatus: State;
    getCurrentUserStatus: State;
}

const initialState: AccountState = {
    user: null,
    accessToken: null,
    isAuthenticated: false,
    loginStatus: false,
    registerStatus: false,
    logoutStatus: {
        loading: false,
        status: false,
        error: null,
    },
    getCurrentUserStatus: {
        loading: false,
        status: false,
        error: null,
    },
};

export const loginAsync = createAsyncThunk<string, FieldValues>(
    "account/login",
    async (data, thunkAPI) => {
        try {
            const accessToken = await agent.account.login(data);
            return accessToken;
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

export const refreshAsync = createAsyncThunk<string, FieldValues>(
    "account/refresh",
    async (data, thunkAPI) => {
        try {
            const accessToken = await agent.account.refresh(data);
            return accessToken;
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
);

export const getCurrentUserAsync = createAsyncThunk<Account>(
    "account/getCurrentUser",
    async (_, thunkAPI) => {
        try {
            const user = await agent.account.getCurrentUser();
            return user;
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
);

export const accountSlice = createSlice({
    name: "account",
    initialState,
    reducers: {
        resetLoginStatus: (state) => {
            state.loginStatus = false;
        },
        resetRegisterStatus: (state) => {
            state.registerStatus = false;
        },
        resetLogoutState: (state) => {
            state.logoutStatus = initialState.logoutStatus;
        },
        resetGetCurrentUserState: (state) => {
            state.getCurrentUserStatus = initialState.getCurrentUserStatus;
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(logoutAsync.pending, (state) => {
                state.logoutStatus.loading = true;
            })
            .addCase(getCurrentUserAsync.pending, (state) => {
                state.getCurrentUserStatus.loading = true;
            })
            .addCase(registerAsync.fulfilled, (state) => {
                state.registerStatus = true;
            })
            .addCase(logoutAsync.fulfilled, (state) => {
                Cookies.remove("refreshToken");
                state.user = null;
                state.accessToken = null;
                state.logoutStatus.loading = false;
                state.logoutStatus.status = true;
                state.isAuthenticated = false;
            })
            .addCase(getCurrentUserAsync.fulfilled, (state, action) => {
                state.user = { ...state.user, ...action.payload };
                state.getCurrentUserStatus.loading = false;
                state.getCurrentUserStatus.status = true;
            })
            .addCase(logoutAsync.rejected, (state, action) => {
                state.logoutStatus.loading = false;
                state.logoutStatus.error = action.payload as string;
            })
            .addCase(refreshAsync.rejected, (state) => {
                Cookies.remove("refreshToken");
                state.user = null;
                state.accessToken = null;
                state.isAuthenticated = false;
            })
            .addCase(getCurrentUserAsync.rejected, (state, action) => {
                state.getCurrentUserStatus.loading = false;
                state.getCurrentUserStatus.error = action.payload as string;
            })
            .addMatcher(
                isAnyOf(loginAsync.fulfilled, refreshAsync.fulfilled),
                (state, action) => {
                    const claims = JSON.parse(
                        atob(action.payload.split(".")[1])
                    );
                    const role =
                        claims[
                            "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                        ];
                    state.user = {
                        ...state.user,
                        accessToken: action.payload,
                        role: role,
                    } as Account;
                    state.accessToken = action.payload;
                    if (action.type === loginAsync.fulfilled.type) {
                        state.loginStatus = true;
                        state.isAuthenticated = true;
                    }
                }
            );
    },
});

export const {
    resetLoginStatus,
    resetRegisterStatus,
    resetLogoutState,
    resetGetCurrentUserState,
} = accountSlice.actions;
