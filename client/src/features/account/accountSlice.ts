import { createAsyncThunk, createSlice, isAnyOf } from "@reduxjs/toolkit";
import {
    LoginRequest,
    LogoutRequest,
    RefreshRequest,
    RegisterRequest,
    User,
} from "../../app/models/user";
import agent from "../../app/api/agent";
import { router } from "../../app/router/routes";

interface AccountState {
    user: User | null;
    loading: boolean;
}

const initialState: AccountState = {
    user: null,
    loading: false,
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

export const logoutAsync = createAsyncThunk<void, LogoutRequest>(
    "account/logout",
    async (data, thunkAPI) => {
        try {
            await agent.account.logout(data);
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
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(loginAsync.fulfilled, () => {
                router.navigate("/books");
            })
            .addCase(registerAsync.fulfilled, () => {
                router.navigate("/login");
            })
            .addCase(getCurrentUserAsync.fulfilled, (state, action) => {
                state.user = action.payload;
                state.loading = false;
            })
            .addCase(logoutAsync.fulfilled, (state) => {
                state.user = null;
                state.loading = false;
            })
            .addMatcher(
                isAnyOf(
                    loginAsync.pending,
                    registerAsync.pending,
                    getCurrentUserAsync.pending,
                    logoutAsync.pending,
                    refreshAsync.pending
                ),
                (state) => {
                    state.loading = true;
                }
            )
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
                    state.loading = false;
                }
            )
            .addMatcher(
                isAnyOf(
                    loginAsync.rejected,
                    registerAsync.rejected,
                    getCurrentUserAsync.rejected,
                    logoutAsync.rejected,
                    refreshAsync.rejected
                ),
                (state, action) => {
                    state.loading = false;
                    throw action.payload;
                }
            );
    },
});

// export const {} = accountSlice.actions;
