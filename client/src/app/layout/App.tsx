import {
    Box,
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
import { useAppDispatch } from "../store/configureStore";
import { getCurrentUserAsync } from "../../features/account/accountSlice";
import Footer from "./Footer";

function App() {
    const location = useLocation();
    const dispatch = useAppDispatch();
    const [loading, setLoading] = useState(true);
    const [darkMode, setDarkMode] = useState(false);

    const initApp = useCallback(async () => {
        await dispatch(getCurrentUserAsync());
    }, [dispatch]);

    useEffect(() => {
        initApp().then(() => setLoading(false));
    }, [initApp]);

    const paletteType = darkMode ? "dark" : "light";
    const theme = createTheme({
        palette: {
            mode: paletteType,
            background: {
                default: paletteType === "light" ? "#eaeaea" : "#121212",
                paper: paletteType === "light" ? "#fff" : "#333",
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
            <Box
                sx={{
                    display: "flex",
                    flexDirection: "column",
                    minHeight: "100vh",
                }}
            >
                <Header
                    darkMode={darkMode}
                    handleThemeChange={handleThemeChange}
                />
                <Box sx={{ flex: 1 }}>
                    {loading ? (
                        <LoadingComponent />
                    ) : location.pathname === "/" ? (
                        <Home />
                    ) : (
                        <Container sx={{ mt: 4 }}>
                            <Outlet />
                        </Container>
                    )}
                </Box>
                <Footer />
            </Box>
        </ThemeProvider>
    );
}

export default App;
