import {
    createAsyncThunk,
    createEntityAdapter,
    createSlice,
} from "@reduxjs/toolkit";
import { Book, BookParams, BookDetail } from "../../app/models/book";
import { MetaData } from "../../app/models/pagination";
import { RootState } from "../../app/store/configureStore";
import agent from "../../app/api/agent";
import { AuthorForUpsertBook } from "../../app/models/author";

interface BookState {
    bookDetail: BookDetail | null;
    bookDetailLoaded: boolean;
    booksLoaded: boolean;
    filterLoaded: boolean;
    filter: {
        publishers: string[];
        languages: string[];
        categories: string[];
        minPrice: number;
        maxPrice: number;
    };
    bookParams: BookParams;
    metaData: MetaData | null;
    status: string;
    authorsForUpsert: AuthorForUpsertBook[];
    authorsForUpsertLoaded: boolean;
}

interface Filter {
    publishers: string[];
    languages: string[];
    categories: string[];
    minPrice: number;
    maxPrice: number;
}

const booksAdapter = createEntityAdapter<Book>();

const getAxiosParams = (bookParams: BookParams) => {
    const params = new URLSearchParams();
    params.append("pageIndex", bookParams.pageIndex.toString());
    params.append("pageSize", bookParams.pageSize.toString());
    params.append("sort", bookParams.sort);
    if (bookParams.nameSearch) {
        params.append("nameSearch", bookParams.nameSearch);
    }
    if (bookParams.authorSearch) {
        params.append("authorSearch", bookParams.authorSearch);
    }
    if (bookParams.minPrice) {
        params.append("minPrice", bookParams.minPrice.toString());
    }
    if (bookParams.maxPrice) {
        params.append("maxPrice", bookParams.maxPrice.toString());
    }
    if (bookParams.languages.length > 0)
        params.append("languages", bookParams.languages.toString());
    if (bookParams.publishers.length > 0)
        params.append("publishers", bookParams.publishers.toString());
    if (bookParams.categories.length > 0)
        params.append("categories", bookParams.categories.toString());
    return params;
};

export const getBooksAsync = createAsyncThunk<
    Book[],
    void,
    { state: RootState }
>("book/getBooksAsync", async (_, thunkAPI) => {
    try {
        const params = getAxiosParams(thunkAPI.getState().book.bookParams);
        const response = await agent.book.getBooks(params);
        thunkAPI.dispatch(setBookMetaData(response.metaData));
        return response.items;
    } catch (error: any) {
        return thunkAPI.rejectWithValue({ error: error.data });
    }
});

export const getBookAsync = createAsyncThunk<BookDetail, number>(
    "book/getBookAsync",
    async (bookId, thunkAPI) => {
        try {
            const book = await agent.book.getBook(bookId);
            return book;
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
);

export const getBookFilterAsync = createAsyncThunk<Filter, void>(
    "book/getBookFilterAsync",
    async (_, thunkAPI) => {
        try {
            const filter = await agent.book.getBookFilter();
            return filter;
        } catch (error: any) {
            return thunkAPI.rejectWithValue({ error: error.data });
        }
    }
);

export const getAuthorsForUpsertBook = createAsyncThunk<
    AuthorForUpsertBook[],
    void
>("book/getAuthorsForUpsertBook", async (_, thunkAPI) => {
    try {
        const authors = await agent.author.getAuthorsForUpsertBook();
        return authors;
    } catch (error: any) {
        return thunkAPI.rejectWithValue({ error: error.data });
    }
});

function initParams(): BookParams {
    return {
        pageIndex: 1,
        pageSize: 12,
        sort: "nameAsc",
        publishers: [],
        languages: [],
        categories: [],
    };
}

export const bookSlice = createSlice({
    name: "book",
    initialState: booksAdapter.getInitialState<BookState>({
        bookDetail: null,
        bookDetailLoaded: false,
        booksLoaded: false,
        filter: {
            publishers: [],
            languages: [],
            categories: [],
            minPrice: 0,
            maxPrice: 0,
        },
        filterLoaded: false,
        bookParams: initParams(),
        metaData: null,
        status: "idle",
        authorsForUpsert: [],
        authorsForUpsertLoaded: false,
    }),
    reducers: {
        setBookParams: (state, action) => {
            state.booksLoaded = false;
            state.bookParams = {
                ...state.bookParams,
                ...action.payload,
                pageIndex: 1,
            };
        },
        setBookPageIndex: (state, action) => {
            state.booksLoaded = false;
            state.bookParams = { ...state.bookParams, ...action.payload };
        },
        setBookMetaData: (state, action) => {
            state.metaData = action.payload;
        },
        resetBookParams: (state) => {
            state.bookParams = initParams();
        },
        setBook: (state, action) => {
            booksAdapter.upsertOne(state, action.payload);
            state.booksLoaded = false;
        },
        removeBook: (state, action) => {
            booksAdapter.removeOne(state, action.payload);
            state.booksLoaded = false;
        },
    },
    extraReducers: (builder) => {
        builder.addCase(getBooksAsync.pending, (state) => {
            state.status = "pendingGetBooks";
        });
        builder.addCase(getBooksAsync.fulfilled, (state, action) => {
            booksAdapter.setAll(state, action.payload);
            state.status = "idle";
            state.booksLoaded = true;
        });
        builder.addCase(getBooksAsync.rejected, (state) => {
            state.status = "idle";
        });
        builder.addCase(getBookAsync.pending, (state) => {
            state.status = "pendingGetBook";
        });
        builder.addCase(getBookAsync.fulfilled, (state, action) => {
            booksAdapter.upsertOne(state, action.payload);
            state.bookDetail = action.payload;
            state.bookDetailLoaded = true;
            state.status = "idle";
        });
        builder.addCase(getBookAsync.rejected, (state) => {
            state.status = "idle";
        });
        builder.addCase(getBookFilterAsync.pending, (state) => {
            state.status = "pendingGetBookFilter";
        });
        builder.addCase(getBookFilterAsync.fulfilled, (state, action) => {
            state.filter.publishers = action.payload.publishers;
            state.filter.languages = action.payload.languages;
            state.filter.categories = action.payload.categories;
            state.filter.minPrice = action.payload.minPrice;
            state.filter.maxPrice = action.payload.maxPrice;
            state.status = "idle";
            state.filterLoaded = true;
        });
        builder.addCase(getBookFilterAsync.rejected, (state) => {
            state.status = "idle";
        });
        builder.addCase(getAuthorsForUpsertBook.pending, (state) => {
            state.status = "pendingAuthorsForUpsertBook";
        });
        builder.addCase(getAuthorsForUpsertBook.fulfilled, (state, action) => {
            state.authorsForUpsert = action.payload;
            state.status = "idle";
            state.filterLoaded = true;
        });
        builder.addCase(getAuthorsForUpsertBook.rejected, (state) => {
            state.status = "idle";
        });
    },
});

export const {
    setBookMetaData,
    setBookParams,
    setBookPageIndex,
    resetBookParams,
    setBook,
    removeBook,
} = bookSlice.actions;

export const bookSelectors = booksAdapter.getSelectors(
    (state: RootState) => state.book
);
