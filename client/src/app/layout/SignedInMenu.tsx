import {
    Button,
    Menu,
    Fade,
    MenuItem,
    ListItemIcon,
    Typography,
    Avatar,
    Box,
} from "@mui/material";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import ExitToAppIcon from "@mui/icons-material/ExitToApp";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";
import { useEffect, useState } from "react";
import { useAppDispatch, useAppSelector } from "../store/configureStore";
import { Link, useNavigate } from "react-router-dom";
import {
    logoutAsync,
    resetLogoutState,
} from "../../features/account/accountSlice";

export default function SignedInMenu() {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const { user, role, logoutState } = useAppSelector(
        (state) => state.account
    );
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);

    const handleClick = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    useEffect(() => {
        if (logoutState) {
            navigate("/");
            dispatch(resetLogoutState());
        }
    }, [logoutState, navigate, dispatch]);

    const handleLogout = async () => {
        await dispatch(logoutAsync());
        handleClose();
    };

    return (
        <>
            <Button
                color="inherit"
                onClick={handleClick}
                sx={{
                    typography: "h6",
                    textTransform: "none",
                    display: "flex",
                    alignItems: "center",
                    gap: 1,
                }}
            >
                {user?.imageUrl ? (
                    <Avatar
                        src={user.imageUrl}
                        alt={user.userName}
                        sx={{ width: 32, height: 32 }}
                    />
                ) : (
                    <AccountCircleIcon fontSize="large" />
                )}
                <Box>{user?.userName}</Box>
            </Button>
            <Menu
                anchorEl={anchorEl}
                open={open}
                onClose={handleClose}
                TransitionComponent={Fade}
                PaperProps={{
                    elevation: 3,
                    sx: { borderRadius: 2, minWidth: 200 },
                }}
            >
                <MenuItem component={Link} to="/profile" onClick={handleClose}>
                    <ListItemIcon>
                        <AccountCircleIcon fontSize="small" />
                    </ListItemIcon>
                    <Typography variant="body1">Thông tin cá nhân</Typography>
                </MenuItem>
                {role === "Member" && (
                    <MenuItem
                        component={Link}
                        to="/order"
                        onClick={handleClose}
                        sx={{
                            "&:hover": {
                                backgroundColor: "rgba(0, 150, 136, 0.1)",
                            },
                        }}
                    >
                        <ListItemIcon>
                            <ShoppingCartIcon fontSize="small" />
                        </ListItemIcon>
                        <Typography variant="body1">Đơn hàng</Typography>
                    </MenuItem>
                )}
                <MenuItem onClick={handleLogout}>
                    <ListItemIcon>
                        <ExitToAppIcon fontSize="small" />
                    </ListItemIcon>
                    <Typography variant="body1" color="error">
                        Đăng xuất
                    </Typography>
                </MenuItem>
            </Menu>
        </>
    );
}
