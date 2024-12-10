import { createBrowserRouter, Navigate } from "react-router-dom";
import App from "../layout/App";
import ServerError from "../errors/ServerError";
import NotFound from "../errors/NotFound";
import Login from "../../features/account/Login";
import Register from "../../features/account/Register";
import Book from "../../features/book/member/Book";
import Author from "../../features/author/member/Author";
import RequireAuth from "./RequiredAuth";
import BookDetail from "../../features/book/member/BookDetail";
import AuthorDetail from "../../features/author/member/AuthorDetail";
import Basket from "../../features/basket/member/Basket";
import ManageBook from "../../features/book/admin/ManageBook";
import ManageAuthor from "../../features/author/admin/ManageAuthor";
import Profile from "../../features/profile/Profile";
import CheckoutWrapper from "../../features/checkout/CheckoutWrapper";
import Order from "../../features/order/Order";
import OrderDetail from "../../features/order/OrderDetail";

export const router = createBrowserRouter(
    [
        {
            path: "/",
            element: <App />,
            children: [
                {
                    element: <RequireAuth />,
                    children: [{ path: "/profile", element: <Profile /> }],
                },
                {
                    element: <RequireAuth requiredRole="Member" />,
                    children: [
                        { path: "/order", element: <Order /> },
                        { path: "/basket", element: <Basket /> },
                        { path: "/checkout", element: <CheckoutWrapper /> },
                        { path: "/order/:id", element: <OrderDetail /> },
                    ],
                },
                {
                    element: <RequireAuth requiredRole="Admin" />,
                    children: [
                        { path: "manage/order", element: <Order /> },
                        { path: "/manage/book", element: <ManageBook /> },
                        { path: "/manage/author", element: <ManageAuthor /> },
                        { path: "/manage/book/:id", element: <BookDetail /> },
                        {
                            path: "/manage/author/:id",
                            element: <AuthorDetail />,
                        },
                        { path: "/manage/order/:id", element: <OrderDetail /> },
                    ],
                },
                { path: "/book", element: <Book /> },
                { path: "/book/:id", element: <BookDetail /> },
                { path: "/author", element: <Author /> },
                { path: "/author/:id", element: <AuthorDetail /> },
                { path: "/login", element: <Login /> },
                { path: "/register", element: <Register /> },
                { path: "/server-error", element: <ServerError /> },
                { path: "/not-found", element: <NotFound /> },
                { path: "*", element: <Navigate replace to="/not-found" /> },
            ],
        },
    ],
    {
        future: {
            v7_fetcherPersist: true,
            v7_normalizeFormMethod: true,
            v7_partialHydration: true,
            v7_relativeSplatPath: true,
            v7_skipActionErrorRevalidation: true,
        },
    }
);
