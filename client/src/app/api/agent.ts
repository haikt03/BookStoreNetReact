import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { router } from "../router/routes";
import { PaginatedResponse } from "../models/pagination";
import {
    LoginRequest,
    LogoutRequest,
    RefreshRequest,
    RegisterRequest,
} from "../models/user";
import { store } from "../store/configureStore";
import { refreshAsync } from "../../features/account/accountSlice";

axios.defaults.baseURL = import.meta.env.VITE_API_URL as string;
axios.defaults.withCredentials = true;

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.request.use((config) => {
    const token = store.getState().account.user?.accessToken;
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
});

axios.interceptors.response.use(
    async (response) => {
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
                    const modelStateErrors: string[] = [];
                    for (const key in data.errors) {
                        if (data.errors[key]) {
                            modelStateErrors.push(data.errors[key]);
                        }
                    }
                    throw modelStateErrors.flat();
                }
                toast.error(data.title);
                break;
            case 401: {
                const refreshToken = document.cookie
                    .split("; ")
                    .find((row) => row.startsWith("refreshToken"))
                    ?.split("=")[1];
                if (refreshToken) {
                    const newAccessToken = await store.dispatch(
                        refreshAsync({ refreshToken })
                    );

                    const originalRequest = error.config;
                    if (originalRequest) {
                        originalRequest.headers[
                            "Authorization"
                        ] = `Bearer ${newAccessToken}`;
                        return axios(originalRequest);
                    }
                }
                toast.error(data.title);
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

const account = {
    login: (values: LoginRequest) => requests.post("account/login", values),
    register: (values: RegisterRequest) =>
        requests.post("account/register", values),
    logout: (values: LogoutRequest) =>
        requests.post("account/me/logout", values),
    getCurrentUser: () => requests.get("account/me"),
    refresh: (values: RefreshRequest) =>
        requests.post("account/refresh", values),
};

const agent = {
    account,
};

export default agent;
