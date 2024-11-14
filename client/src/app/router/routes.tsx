import { createBrowserRouter, Navigate } from "react-router-dom";
import App from "../layout/App";
import ServerError from "../errors/ServerError";
import NotFound from "../errors/NotFound";
import Login from "../../features/account/Login";
import Register from "../../features/account/Register";
import Book from "../../features/book/Book";
import Author from "../../features/author/Author";
import RequireAuth from "./RequiredAuth";

export const router = createBrowserRouter(
    [
        {
            path: "/",
            element: <App />,
            children: [
                {
                    element: <RequireAuth />,
                    children: [],
                },
                {
                    element: <RequireAuth roles={["Admin"]} />,
                    children: [],
                },
                { path: "book", element: <Book /> },
                { path: "author", element: <Author /> },
                { path: "login", element: <Login /> },
                { path: "register", element: <Register /> },
                { path: "server-error", element: <ServerError /> },
                { path: "not-found", element: <NotFound /> },
                { path: "*", element: <Navigate replace to="not-found" /> },
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
