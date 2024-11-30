import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { router } from "../router/routes";
import { PaginatedResponse } from "../models/pagination";
import { store } from "../store/configureStore";
import { refreshAsync } from "../../features/account/accountSlice";
import Cookies from "js-cookie";

// const sleep = () => new Promise((resolve) => setTimeout(resolve, 500));

axios.defaults.baseURL = import.meta.env.VITE_API_URL as string;
axios.defaults.withCredentials = true;

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.request.use((config) => {
    const accessToken = Cookies.get("accessToken");
    if (accessToken) config.headers.Authorization = `Bearer ${accessToken}`;
    return config;
});

axios.interceptors.response.use(
    async (response) => {
        // if (import.meta.env.DEV) await sleep();
        const pagination = response.headers["pagination"];
        if (pagination) {
            response.data = new PaginatedResponse(
                response.data,
                JSON.parse(pagination)
            );
            return response;
        }
        return response;
    },
    async (error: AxiosError) => {
        const { data, status } = error.response as AxiosResponse;
        switch (status) {
            case 400:
                if (data.errors) {
                    for (const key in data.errors) {
                        toast.error(data.errors[key][0]);
                    }
                }
                toast.error(data.title);
                break;
            case 401: {
                const refreshToken = Cookies.get("refreshToken");
                if (refreshToken) {
                    await store.dispatch(refreshAsync({ refreshToken }));
                    const accessToken = Cookies.get("accessToken");
                    const originalRequest = error.config;
                    if (originalRequest && accessToken) {
                        originalRequest.headers[
                            "Authorization"
                        ] = `Bearer ${accessToken}`;
                        return axios(originalRequest);
                    }
                }
                router.navigate("/login");
                break;
            }
            case 403:
                toast.error("Bạn không được phép truy cập");
                break;
            case 500:
                router.navigate("/server-error", { state: { error: data } });
                break;
            default:
                break;
        }
        return Promise.reject(error.response);
    }
);

const requests = {
    get: (url: string, params?: URLSearchParams) =>
        axios.get(url, { params }).then(responseBody),
    post: (url: string, body: object) =>
        axios.post(url, body).then(responseBody),
    put: (url: string, body: object) => axios.put(url, body).then(responseBody),
    del: (url: string) => axios.delete(url).then(responseBody),
    postForm: (url: string, data: FormData) =>
        axios
            .post(url, data, {
                headers: { "Content-type": "multipart/form-data" },
            })
            .then(responseBody),
    putForm: (url: string, data: FormData) =>
        axios
            .put(url, data, {
                headers: { "Content-type": "multipart/form-data" },
            })
            .then(responseBody),
};

function createFormData(item: any) {
    const formData = new FormData();
    for (const key in item) {
        formData.append(key, item[key]);
    }
    return formData;
}

const account = {
    login: (body: any) => requests.post("account/login", body),
    register: (body: any) => requests.post("account/register", body),
    logout: (body: any) => requests.post("account/me/logout", body),
    refresh: (body: any) => requests.post("account/refresh", body),
    getCurrentUser: () => requests.get("account/me"),
    updateMe: (body: any) =>
        requests.putForm("account/me", createFormData(body)),
    updateUserAddress: (body: any) => requests.put("account/me/address", body),
    changePassword: (body: any) => requests.put("account/me/password", body),
    sendConfirmationEmail: () =>
        requests.post("account/me/send-confirmation-email", {}),
    confirmEmail: (params: URLSearchParams) =>
        requests.get("account/confirm-email", params),
};

const book = {
    getBooks: (params: URLSearchParams) => requests.get("books", params),
    getBook: (id: number) => requests.get(`books/${id}`),
    createBook: (body: any) => requests.postForm("books", createFormData(body)),
    updateBook: (id: number, body: any) =>
        requests.putForm(`books/${id}`, createFormData(body)),
    deleteBook: (id: number) => requests.del(`books/${id}`),
    getBookFilter: () => requests.get("books/filter"),
};

const basket = {
    getBasket: () => requests.get("basket"),
    addBasketItem: (bookId: number, quantity = 1) =>
        requests.post(`basket/add?bookId=${bookId}&quantity=${quantity}`, {}),
    removeBasketItem: (bookId: number, quantity = 1) =>
        requests.post(
            `basket/remove?bookId=${bookId}&quantity=${quantity}`,
            {}
        ),
};

const author = {
    getAuthors: (params: URLSearchParams) => requests.get("authors", params),
    getAuthor: (id: number) => requests.get(`authors/${id}`),
    createAuthor: (body: any) =>
        requests.postForm("authors", createFormData(body)),
    updateAuthor: (id: number, body: any) =>
        requests.putForm(`authors/${id}`, createFormData(body)),
    deleteAuthor: (id: number) => requests.del(`authors/${id}`),
    getAuthorFilter: () => requests.get("authors/filter"),
    getBooksByAuthor: (id: number, params: URLSearchParams) =>
        requests.get(`authors/${id}/books`, params),
    getAuthorsForUpsertBook: () => requests.get("authors/upsert-book"),
};

const agent = {
    account,
    book,
    basket,
    author,
};

export default agent;
