import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { router } from "../router/routes";
import { PaginatedResponse } from "../models/pagination";
import { store } from "../store/configureStore";
import { refreshAsync } from "../../features/account/accountSlice";
import {
    ChangePasswordRequest,
    ConfirmEmailQuery,
    LoginRequest,
    LogoutRequest,
    RefreshRequest,
    RegisterRequest,
    UpdateMeRequest,
    UpdateUserAddressRequest,
} from "../models/account";

axios.defaults.baseURL = import.meta.env.VITE_API_URL as string;
axios.defaults.withCredentials = true;

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.request.use((config) => {
    const token = store.getState().account.accessToken;
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
                    for (const key in data.errors) {
                        toast.error(data.errors[key][0]);
                    }
                }
                toast.error(data.title);
                break;
            case 401: {
                const refreshToken = document.cookie
                    .split("; ")
                    .find((row) => row.startsWith("refreshToken"))
                    ?.split("=")[1];
                if (refreshToken) {
                    await store.dispatch(refreshAsync({ refreshToken }));
                    const newAccessToken = store.getState().account.accessToken;

                    const originalRequest = error.config;
                    if (originalRequest) {
                        originalRequest.headers[
                            "Authorization"
                        ] = `Bearer ${newAccessToken}`;
                        return axios(originalRequest);
                    }
                }
                toast.error(data);
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
    login: (body: LoginRequest) => requests.post("account/login", body),
    register: (body: RegisterRequest) =>
        requests.post("account/register", body),
    logout: (body: LogoutRequest) => requests.post("account/me/logout", body),
    refresh: (body: RefreshRequest) => requests.post("account/refresh", body),
    getCurrentUser: () => requests.get("account/me"),
    updateMe: (body: UpdateMeRequest) =>
        requests.putForm("account/me", createFormData(body)),
    updateUserAddress: (body: UpdateUserAddressRequest) =>
        requests.put("account/me/address", body),
    changePassword: (body: ChangePasswordRequest) =>
        requests.put("account/me/password", body),
    sendConfirmationEmail: () =>
        requests.post("account/me/send-confirmation-email", {}),
    confirmEmail: (params: URLSearchParams) =>
        requests.get(
            'account/confirm-email'
        ),
};

const user = {
    getAllUsers: (params: URLSearchParams) => requests.get("users", params),
};

const agent = {
    account,
    user,
};

export default agent;
