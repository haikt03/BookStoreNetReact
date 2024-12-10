import { useAppSelector } from "../../app/store/configureStore";
import {
    Box,
    Card,
    CardContent,
    Typography,
    Avatar,
    Grid,
    Button,
    Divider,
    Paper,
} from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import LockIcon from "@mui/icons-material/Lock";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { useState } from "react";
import ProfileForm from "./ProfileForm";
import AddressForm from "./AddressForm";
import ChangePasswordForm from "./ChangePasswordForm";

export default function Profile() {
    const { user } = useAppSelector((state) => state.account);
    const [editProfileMode, setEditProfileMode] = useState(false);
    const [editAddressMode, setEditAddressMode] = useState(false);
    const [changePasswordMode, setChangePasswordMode] = useState(false);

    function cancelEditProfile() {
        setEditProfileMode(false);
    }

    function cancelEditAddress() {
        setEditAddressMode(false);
    }

    function cancelChangePassword() {
        setChangePasswordMode(false);
    }

    if (!user) return <LoadingComponent />;

    if (editProfileMode)
        return <ProfileForm cancelEdit={cancelEditProfile} user={user} />;

    if (editAddressMode)
        return (
            <AddressForm
                cancelEdit={cancelEditAddress}
                specificAddress={user.address.specificAddress}
            />
        );

    if (changePasswordMode)
        return <ChangePasswordForm cancelEdit={cancelChangePassword} />;

    return (
        <Box display="flex" justifyContent="center" alignItems="center" mt={4}>
            <Card sx={{ width: "100%", p: 4 }}>
                <CardContent>
                    <Box display="flex" justifyContent="center" mb={3}>
                        <Avatar
                            alt={user.fullName}
                            src={user.imageUrl || "/default-avatar.jpg"}
                            sx={{ width: 120, height: 120 }}
                        />
                    </Box>
                    <Typography
                        variant="h5"
                        align="center"
                        gutterBottom
                        fontWeight="bold"
                        color="primary"
                    >
                        {user.fullName}
                    </Typography>

                    <Grid container spacing={3}>
                        <Grid item xs={12} md={6}>
                            <Paper sx={{ p: 3, boxShadow: 3 }}>
                                <Typography variant="h6" fontWeight="bold">
                                    Thông tin cá nhân
                                </Typography>
                                <Divider sx={{ my: 1 }} />
                                <Box mb={2}>
                                    <Typography
                                        variant="body2"
                                        color="text.secondary"
                                    >
                                        <strong>Email:</strong>{" "}
                                        <span style={{ float: "right" }}>
                                            {user.email}
                                        </span>
                                    </Typography>
                                </Box>
                                <Box mb={2}>
                                    <Typography
                                        variant="body2"
                                        color="text.secondary"
                                    >
                                        <strong>Số điện thoại:</strong>{" "}
                                        <span style={{ float: "right" }}>
                                            {user.phoneNumber ||
                                                "Chưa cập nhật"}
                                        </span>
                                    </Typography>
                                </Box>
                                <Box mb={2}>
                                    <Typography
                                        variant="body2"
                                        color="text.secondary"
                                    >
                                        <strong>Ngày sinh:</strong>{" "}
                                        <span style={{ float: "right" }}>
                                            {user.dateOfBirth ||
                                                "Chưa cập nhật"}
                                        </span>
                                    </Typography>
                                </Box>
                                <Box mb={2}>
                                    <Typography
                                        variant="body2"
                                        color="text.secondary"
                                    >
                                        <strong>Email xác thực:</strong>{" "}
                                        <span style={{ float: "right" }}>
                                            {user.emailConfirmed
                                                ? "Đã xác thực"
                                                : "Chưa xác thực"}
                                        </span>
                                    </Typography>
                                </Box>
                                <Box mb={2}>
                                    <Typography
                                        variant="body2"
                                        color="text.secondary"
                                    >
                                        <strong>Số điện thoại xác thực:</strong>{" "}
                                        <span style={{ float: "right" }}>
                                            {user.phoneNumberConfirmed
                                                ? "Đã xác thực"
                                                : "Chưa xác thực"}
                                        </span>
                                    </Typography>
                                </Box>
                            </Paper>
                        </Grid>

                        <Grid item xs={12} md={6}>
                            <Paper sx={{ p: 3, boxShadow: 3 }}>
                                <Typography variant="h6" fontWeight="bold">
                                    Thông tin địa chỉ
                                </Typography>
                                <Divider sx={{ my: 1 }} />
                                {user.address ? (
                                    <>
                                        <Box mb={2}>
                                            <Typography
                                                variant="body2"
                                                color="text.secondary"
                                            >
                                                <strong>Tỉnh/Thành phố:</strong>{" "}
                                                <span
                                                    style={{ float: "right" }}
                                                >
                                                    {user.address.city}
                                                </span>
                                            </Typography>
                                        </Box>
                                        <Box mb={2}>
                                            <Typography
                                                variant="body2"
                                                color="text.secondary"
                                            >
                                                <strong>Quận/Huyện:</strong>{" "}
                                                <span
                                                    style={{ float: "right" }}
                                                >
                                                    {user.address.district}
                                                </span>
                                            </Typography>
                                        </Box>
                                        <Box mb={2}>
                                            <Typography
                                                variant="body2"
                                                color="text.secondary"
                                            >
                                                <strong>Phường/Xã:</strong>{" "}
                                                <span
                                                    style={{ float: "right" }}
                                                >
                                                    {user.address.ward}
                                                </span>
                                            </Typography>
                                        </Box>
                                        <Box mb={2}>
                                            <Typography
                                                variant="body2"
                                                color="text.secondary"
                                            >
                                                <strong>Địa chỉ cụ thể:</strong>{" "}
                                                <span
                                                    style={{ float: "right" }}
                                                >
                                                    {
                                                        user.address
                                                            .specificAddress
                                                    }
                                                </span>
                                            </Typography>
                                        </Box>
                                    </>
                                ) : (
                                    <Typography
                                        variant="body2"
                                        color="text.secondary"
                                    >
                                        Chưa cập nhật địa chỉ
                                    </Typography>
                                )}
                            </Paper>
                        </Grid>
                    </Grid>

                    <Box mt={3}>
                        <Grid container spacing={3}>
                            <Grid item xs={12} md={6}>
                                <Button
                                    variant="contained"
                                    color="primary"
                                    fullWidth
                                    startIcon={<EditIcon />}
                                    sx={{
                                        "&:hover": {
                                            backgroundColor: "#1976d2",
                                        },
                                    }}
                                    onClick={() => setEditProfileMode(true)}
                                >
                                    Cập nhật thông tin cá nhân
                                </Button>
                            </Grid>

                            <Grid item xs={12} md={6}>
                                <Button
                                    variant="contained"
                                    color="success"
                                    fullWidth
                                    startIcon={<EditIcon />}
                                    sx={{
                                        "&:hover": {
                                            backgroundColor: "#4caf50",
                                        },
                                    }}
                                    onClick={() => setEditAddressMode(true)}
                                >
                                    Cập nhật thông tin địa chỉ
                                </Button>
                            </Grid>

                            <Grid item xs={12} md={6}>
                                <Button
                                    variant="contained"
                                    color="secondary"
                                    fullWidth
                                    startIcon={<LockIcon />}
                                    sx={{
                                        "&:hover": {
                                            backgroundColor: "#9c27b0",
                                        },
                                    }}
                                    onClick={() => setChangePasswordMode(true)}
                                >
                                    Thay đổi mật khẩu
                                </Button>
                            </Grid>
                        </Grid>
                    </Box>
                </CardContent>
            </Card>
        </Box>
    );
}
