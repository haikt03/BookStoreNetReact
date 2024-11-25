import {
    AppBar,
    Badge,
    Box,
    IconButton,
    List,
    ListItem,
    Switch,
    Toolbar,
    Typography,
} from "@mui/material";
import { Link, NavLink } from "react-router-dom";
import {
    ShoppingCart,
    MenuBook,
    People,
    PersonAdd,
    LoginOutlined,
    Category,
} from "@mui/icons-material";
import { useAppSelector } from "../store/configureStore";
import SignedInMenu from "./SignedInMenu";

const midLinksMember = [
    { title: "Sách", path: "/book", icon: <MenuBook /> },
    { title: "Tác giả", path: "/author", icon: <People /> },
];

const midLinksAdmin = [
    { title: "Sách", path: "/book", icon: <MenuBook /> },
    { title: "Tác giả", path: "/author", icon: <People /> },
    { title: "Thể loại", path: "/category", icon: <Category /> },
];

const rightLinks = [
    { title: "Đăng nhập", path: "/login", icon: <LoginOutlined /> },
    { title: "Đăng ký", path: "/register", icon: <PersonAdd /> },
];

const navLinkStyles = {
    color: "inherit",
    textDecoration: "none",
    typography: "h6",
    display: "flex",
    alignItems: "center",
    gap: 1,
    "&:hover": {
        color: "grey.500",
    },
    "&.active": {
        color: "text.secondary",
    },
    whiteSpace: "nowrap",
};

interface Props {
    darkMode: boolean;
    handleThemeChange: () => void;
}

export default function Header({ handleThemeChange, darkMode }: Props) {
    const { isAuthenticated, user } = useAppSelector((state) => state.account);
    const { basket } = useAppSelector((state) => state.basket);
    const itemCount = basket?.items.reduce(
        (sum, item) => sum + item.quantity,
        0
    );

    return (
        <AppBar position="sticky">
            <Toolbar
                sx={{
                    display: "flex",
                    justifyContent: "space-between",
                    alignItems: "center",
                }}
            >
                <Box display="flex" alignItems="center" gap={2}>
                    <Typography
                        variant="h6"
                        key="home"
                        component={NavLink}
                        to="/"
                        sx={navLinkStyles}
                    >
                        BOOK CONNER
                    </Typography>
                    <Switch checked={darkMode} onChange={handleThemeChange} />
                </Box>

                <List sx={{ display: "flex", gap: 2 }}>
                    {user && user.role === "Admin"
                        ? midLinksAdmin.map(({ title, path, icon }) => (
                              <ListItem
                                  component={NavLink}
                                  to={path}
                                  key={path}
                                  sx={navLinkStyles}
                              >
                                  {icon}
                                  {title.toUpperCase()}
                              </ListItem>
                          ))
                        : midLinksMember.map(({ title, path, icon }) => (
                              <ListItem
                                  component={NavLink}
                                  to={path}
                                  key={path}
                                  sx={navLinkStyles}
                              >
                                  {icon}
                                  {title.toUpperCase()}
                              </ListItem>
                          ))}
                </List>

                <Box display="flex" alignItems="center" gap={2}>
                    {user && user.role === "Member" && (
                        <IconButton
                            component={Link}
                            to="/basket"
                            size="large"
                            edge="start"
                            color="inherit"
                            sx={{ mr: 2 }}
                        >
                            <Badge badgeContent={itemCount} color="secondary">
                                <ShoppingCart />
                            </Badge>
                        </IconButton>
                    )}

                    {isAuthenticated ? (
                        <SignedInMenu />
                    ) : (
                        <List sx={{ display: "flex", gap: 2 }}>
                            {rightLinks.map(({ title, path, icon }) => (
                                <ListItem
                                    component={NavLink}
                                    to={path}
                                    key={path}
                                    sx={navLinkStyles}
                                >
                                    {icon}
                                    {title.toUpperCase()}
                                </ListItem>
                            ))}
                        </List>
                    )}
                </Box>
            </Toolbar>
        </AppBar>
    );
}
