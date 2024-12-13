import {
    createAsyncThunk,
    createEntityAdapter,
    createSlice,
} from "@reduxjs/toolkit";
import { Author, AuthorParams, AuthorDetail } from "../../app/models/author";
import { MetaData } from "../../app/models/pagination";
import { RootState } from "../../app/store/configureStore";
import agent from "../../app/api/agent";

interface AuthorState {
    authorDetail: AuthorDetail | null;
    authorDetailLoaded: boolean;
    authorsLoaded: boolean;
    filter: {
        countries: string[];
    };
    filterLoaded: boolean;
    authorParams: AuthorParams;
    metaData: MetaData | null;
    status: string;
}

interface Filter {
    countries: string[];
}

const authorsAdapter = createEntityAdapter<Author>();

const getAxiosParams = (authorParams: AuthorParams) => {
    const params = new URLSearchParams();
    params.append("pageIndex", authorParams.pageIndex.toString());
    params.append("pageSize", authorParams.pageSize.toString());
    params.append("sort", authorParams.sort);
    if (authorParams.fullNameSearch) {
        params.append("fullNameSearch", authorParams.fullNameSearch);
    }
    if (authorParams.countries.length > 0)
        params.append("countries", authorParams.countries.toString());
    return params;
};

export const getAuthorsAsync = createAsyncThunk<
    Author[],
    void,
    { state: RootState }
>("author/getAuthorsAsync", async (_, thunkAPI) => {
    try {
        const params = getAxiosParams(thunkAPI.getState().author.authorParams);
        const response = await agent.author.getAuthors(params);
        thunkAPI.dispatch(setAuthorMetaData(response.metaData));
        return response.items;
    } catch (error: any) {
        return thunkAPI.rejectWithValue({ error: error.data });
    }
});

export const getAuthorAsync = createAsyncThunk<AuthorDetail, number>(
    "author/getAuthorAsync",
    async (authorId, thunkAPI) => {
        try {
            const author = await agent.author.getAuthor(authorId);
            return author;
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
);

export const getAuthorFilterAsync = createAsyncThunk<Filter, void>(
    "author/getAuthorFilterAsync",
    async (_, thunkAPI) => {
        try {
            const filter = await agent.author.getAuthorFilter();
            return filter;
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
);

function initParams(): AuthorParams {
    return {
        pageIndex: 1,
        pageSize: 6,
        sort: "fullNameAsc",
        countries: [],
    };
}

export const authorSlice = createSlice({
    name: "author",
    initialState: authorsAdapter.getInitialState<AuthorState>({
        authorDetail: null,
        authorDetailLoaded: false,
        authorsLoaded: false,
        filterLoaded: false,
        filter: {
            countries: [],
        },
        authorParams: initParams(),
        metaData: null,
        status: "idle",
    }),
    reducers: {
        setAuthorParams: (state, action) => {
            state.authorsLoaded = false;
            state.authorParams = {
                ...state.authorParams,
                ...action.payload,
                pageIndex: 1,
            };
        },
        setAuthorPageIndex: (state, action) => {
            state.authorsLoaded = false;
            state.authorParams = { ...state.authorParams, ...action.payload };
        },
        setAuthorMetaData: (state, action) => {
            state.metaData = action.payload;
        },
        resetAuthorParams: (state) => {
            state.authorParams = initParams();
        },
        setAuthor: (state, action) => {
            authorsAdapter.upsertOne(state, action.payload);
            state.authorsLoaded = false;
        },
        removeAuthor: (state, action) => {
            authorsAdapter.removeOne(state, action.payload);
            state.authorsLoaded = false;
        },
    },
    extraReducers: (builder) => {
        builder.addCase(getAuthorsAsync.pending, (state) => {
            state.status = "pendingGetAuthors";
        });
        builder.addCase(getAuthorsAsync.fulfilled, (state, action) => {
            authorsAdapter.setAll(state, action.payload);
            state.status = "idle";
            state.authorsLoaded = true;
        });
        builder.addCase(getAuthorsAsync.rejected, (state) => {
            state.status = "idle";
        });
        builder.addCase(getAuthorAsync.pending, (state) => {
            state.status = "pendingGetAuthor";
        });
        builder.addCase(getAuthorAsync.fulfilled, (state, action) => {
            authorsAdapter.upsertOne(state, action.payload);
            state.authorDetail = action.payload;
            state.authorDetailLoaded = true;
            state.status = "idle";
        });
        builder.addCase(getAuthorAsync.rejected, (state) => {
            state.status = "idle";
        });
        builder.addCase(getAuthorFilterAsync.pending, (state) => {
            state.status = "pendingGetAuthorFilter";
        });
        builder.addCase(getAuthorFilterAsync.fulfilled, (state, action) => {
            state.filter.countries = action.payload.countries;
            state.status = "idle";
            state.filterLoaded = true;
        });
        builder.addCase(getAuthorFilterAsync.rejected, (state) => {
            state.status = "idle";
        });
    },
});

export const {
    setAuthorMetaData,
    setAuthorParams,
    setAuthorPageIndex,
    resetAuthorParams,
    setAuthor,
    removeAuthor,
} = authorSlice.actions;

export const authorSelectors = authorsAdapter.getSelectors(
    (state: RootState) => state.author
);
