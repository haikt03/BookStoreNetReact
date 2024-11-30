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
    People,
    PersonAdd,
    LoginOutlined,
    MenuBook,
} from "@mui/icons-material";
import { useAppSelector } from "../store/configureStore";
import SignedInMenu from "./SignedInMenu";

const midLinksMember = [
    { title: "Sách", path: "/book", icon: <MenuBook /> },
    { title: "Tác giả", path: "/author", icon: <People /> },
];

const midLinksAdmin = [
    { title: "Quản lý sách", path: "manage/book", icon: <MenuBook /> },
    { title: "Quản lý tác giả", path: "manage/author", icon: <People /> },
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
    const { user, role } = useAppSelector((state) => state.account);
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
                        BOOK CORNER
                    </Typography>
                    <Switch checked={darkMode} onChange={handleThemeChange} />
                </Box>

                <List sx={{ display: "flex", gap: 2 }}>
                    {role === "Admin"
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
                    {role === "Member" && (
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

                    {user ? (
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
