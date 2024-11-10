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
import { Outlet } from "react-router-dom";

function App() {
    const [loading, setLoading] = useState(true);
    const [darkMode, setDarkMode] = useState(false);

    const initApp = useCallback(async () => {
        try {
        } catch (error) {
            console.error(error);
        }
    }, []);

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
