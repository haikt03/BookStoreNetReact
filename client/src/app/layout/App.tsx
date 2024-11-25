import {
    Container,
    createTheme,
    CssBaseline,
    ThemeProvider,
} from "@mui/material";
import { useCallback, useEffect, useState } from "react";
import { ToastContainer } from "react-toastify";
import Header from "./Header";
import LoadingComponent from "./LoadingComponent";
import Home from "../../features/home/Home";
import { Outlet, useLocation } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../store/configureStore";
import { getCurrentUserAsync } from "../../features/account/accountSlice";
import { getBasketAsync } from "../../features/basket/basketSlice";

function App() {
    const location = useLocation();
    const dispatch = useAppDispatch();
    const [loading, setLoading] = useState(true);
    const [darkMode, setDarkMode] = useState(false);
    const { isAuthenticated } = useAppSelector((state) => state.account);

    const initApp = useCallback(async () => {
        if (isAuthenticated) {
            await dispatch(getCurrentUserAsync());
            await dispatch(getBasketAsync());
        }
    }, [dispatch, isAuthenticated]);

    useEffect(() => {
        initApp().then(() => setLoading(false));
    }, [initApp]);

    const paletteType = darkMode ? "dark" : "light";
    const theme = createTheme({
        palette: {
            mode: paletteType,
            background: {
                default: paletteType === "light" ? "#eaeaea" : "#121212",
            },
        },
    });

    const handleThemeChange = () => {
        setDarkMode(!darkMode);
    };

    return (
        <ThemeProvider theme={theme}>
            <ToastContainer
                position="bottom-right"
                hideProgressBar
                theme="colored"
            />
            <CssBaseline />
            <Header darkMode={darkMode} handleThemeChange={handleThemeChange} />
            {loading ? (
                <LoadingComponent />
            ) : location.pathname === "/" ? (
                <Home />
            ) : (
                <Container sx={{ mt: 4 }}>
                    <Outlet />
                </Container>
            )}
        </ThemeProvider>
    );
}

export default App;
