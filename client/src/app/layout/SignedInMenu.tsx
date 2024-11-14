import { Button, Menu, Fade, MenuItem } from "@mui/material";
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
    const { user, logoutStatus } = useAppSelector((state) => state.account);
    const [anchorEl, setAnchorEl] = useState(null);
    const open = Boolean(anchorEl);

    const handleClick = (event: any) => {
        setAnchorEl(event.currentTarget);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    useEffect(() => {
        if (logoutStatus) {
            navigate("/");
            dispatch(resetLogoutState());
        }
    }, [logoutStatus, navigate, dispatch]);

    const handleLogout = async () => {
        await dispatch(logoutAsync());
    };

    return (
        <>
            <Button
                color="inherit"
                onClick={handleClick}
                sx={{ typography: "h6" }}
            >
                {user?.userName}
            </Button>
            <Menu
                anchorEl={anchorEl}
                open={open}
                onClose={handleClose}
                TransitionComponent={Fade}
            >
                <MenuItem onClick={handleClose}>Thông tin cá nhân</MenuItem>
                <MenuItem component={Link} to="/orders">
                    Đơn hàng
                </MenuItem>
                <MenuItem onClick={handleLogout}>Đăng xuất</MenuItem>
            </Menu>
        </>
    );
}
