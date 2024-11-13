import { createAsyncThunk, createSlice, isAnyOf } from "@reduxjs/toolkit";
import {
    LoginRequest,
    RefreshRequest,
    RegisterRequest,
    User,
} from "../../app/models/user";
import agent from "../../app/api/agent";
import Cookies from "js-cookie";

type LoadingErrorState = {
    loading: boolean;
    status: boolean;
    error: string | null;
};

interface AccountState {
    user: User | null;
    loginStatus: LoadingErrorState;
    registerStatus: LoadingErrorState;
    logoutStatus: LoadingErrorState;
    getCurrentUserStatus: LoadingErrorState;
    refreshStatus: LoadingErrorState;
}

const initialState: AccountState = {
    user: null,
    loginStatus: {
        loading: false,
        status: false,
        error: null,
    },
    registerStatus: {
        loading: false,
        status: false,
        error: null,
    },
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
    refreshStatus: {
        loading: false,
        status: false,
        error: null,
    },
};

export const loginAsync = createAsyncThunk<string, LoginRequest>(
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

export const registerAsync = createAsyncThunk<void, RegisterRequest>(
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

export const getCurrentUserAsync = createAsyncThunk<User>(
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

export const refreshAsync = createAsyncThunk<string, RefreshRequest>(
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

export const accountSlice = createSlice({
    name: "account",
    initialState,
    reducers: {
        resetLoginStatus: (state) => {
            state.loginStatus = initialState.loginStatus;
        },
        resetRegisterStatus: (state) => {
            state.registerStatus = initialState.registerStatus;
        },
        resetLogoutStatus: (state) => {
            state.logoutStatus = initialState.logoutStatus;
        },
        resetGetCurrentUserStatus: (state) => {
            state.getCurrentUserStatus = initialState.getCurrentUserStatus;
        },
        resetRefreshStatus: (state) => {
            state.refreshStatus = initialState.refreshStatus;
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(loginAsync.pending, (state) => {
                state.loginStatus.loading = true;
                state.loginStatus.status = false;
                state.loginStatus.error = null;
            })
            .addCase(registerAsync.pending, (state) => {
                state.registerStatus.loading = true;
                state.registerStatus.status = false;
                state.registerStatus.error = null;
            })
            .addCase(logoutAsync.pending, (state) => {
                state.logoutStatus.loading = true;
                state.logoutStatus.status = false;
                state.logoutStatus.error = null;
            })
            .addCase(getCurrentUserAsync.pending, (state) => {
                state.getCurrentUserStatus.loading = true;
                state.getCurrentUserStatus.status = false;
                state.getCurrentUserStatus.error = null;
            })
            .addCase(refreshAsync.pending, (state) => {
                state.refreshStatus.loading = true;
                state.refreshStatus.status = false;
                state.refreshStatus.error = null;
            })
            .addCase(registerAsync.fulfilled, (state) => {
                state.registerStatus.loading = false;
                state.registerStatus.status = true;
                state.registerStatus.error = null;
            })
            .addCase(logoutAsync.fulfilled, (state) => {
                Cookies.remove("refreshToken");
                state.user = null;
                state.logoutStatus.loading = false;
                state.logoutStatus.status = true;
                state.logoutStatus.error = null;
            })
            .addCase(getCurrentUserAsync.fulfilled, (state, action) => {
                state.user = action.payload;
                state.getCurrentUserStatus.loading = false;
                state.getCurrentUserStatus.status = true;
                state.getCurrentUserStatus.error = null;
            })
            .addCase(loginAsync.rejected, (state, action) => {
                state.loginStatus.loading = false;
                state.loginStatus.status = false;
                state.loginStatus.error = action.payload as string;
            })
            .addCase(registerAsync.rejected, (state, action) => {
                state.registerStatus.loading = false;
                state.registerStatus.status = false;
                state.registerStatus.error = action.payload as string;
            })
            .addCase(logoutAsync.rejected, (state, action) => {
                state.logoutStatus.loading = false;
                state.logoutStatus.status = false;
                state.logoutStatus.error = action.payload as string;
            })
            .addCase(getCurrentUserAsync.rejected, (state, action) => {
                state.getCurrentUserStatus.loading = false;
                state.getCurrentUserStatus.status = false;
                state.getCurrentUserStatus.error = action.payload as string;
            })
            .addCase(refreshAsync.rejected, (state, action) => {
                state.refreshStatus.loading = false;
                state.refreshStatus.status = false;
                state.refreshStatus.error = action.payload as string;
            })
            .addMatcher(
                isAnyOf(loginAsync.fulfilled, refreshAsync.fulfilled),
                (state, action) => {
                    const claims = JSON.parse(
                        atob(action.payload.split(".")[1])
                    );
                    const roles =
                        claims[
                            "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                        ];
                    state.user = {
                        ...state.user,
                        accessToken: action.payload,
                        roles: typeof roles === "string" ? [roles] : roles,
                    } as User;
                    if (action.type === loginAsync.fulfilled.type) {
                        state.loginStatus.loading = false;
                        state.loginStatus.status = true;
                        state.loginStatus.error = null;
                    }
                    if (action.type === refreshAsync.fulfilled.type) {
                        state.refreshStatus.loading = false;
                        state.refreshStatus.status = true;
                        state.refreshStatus.error = null;
                    }
                }
            );
    },
});

export const {
    resetLoginStatus,
    resetRegisterStatus,
    resetLogoutStatus,
    resetGetCurrentUserStatus,
    resetRefreshStatus,
} = accountSlice.actions;
