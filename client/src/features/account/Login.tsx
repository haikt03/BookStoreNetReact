import { LoginOutlined } from "@mui/icons-material";
import {
    Container,
    Paper,
    Avatar,
    Typography,
    Box,
    TextField,
    Grid,
} from "@mui/material";
import { Link, useNavigate } from "react-router-dom";
import { FieldValues, useForm } from "react-hook-form";
import { LoadingButton } from "@mui/lab";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { loginAsync, resetLoginStatus } from "./accountSlice";
import { useEffect } from "react";

export default function Login() {
    const dispatch = useAppDispatch();
    const { loginStatus } = useAppSelector((state) => state.account);
    const navigate = useNavigate();
    const {
        register,
        handleSubmit,
        formState: { isSubmitting, errors, isValid },
    } = useForm({
        mode: "onTouched",
    });

    useEffect(() => {
        if (loginStatus) {
            navigate("/book");
            dispatch(resetLoginStatus());
        }
    }, [loginStatus, navigate, dispatch]);

    async function submitForm(data: FieldValues) {
        await dispatch(loginAsync(data));
    }

    return (
        <Container
            component={Paper}
            maxWidth="sm"
            sx={{
                p: 4,
                display: "flex",
                flexDirection: "column",
                alignItems: "center",
            }}
        >
            <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
                <LoginOutlined />
            </Avatar>
            <Typography component="h1" variant="h5">
                Đăng nhập
            </Typography>
            <Box
                component="form"
                onSubmit={handleSubmit(submitForm)}
                noValidate
                sx={{ mt: 1 }}
            >
                <TextField
                    margin="normal"
                    fullWidth
                    label="Tên người dùng"
                    autoComplete="userName"
                    autoFocus
                    {...register("userName", {
                        required: "Tên người dùng không được để trống",
                        minLength: {
                            value: 6,
                            message: "Tên người dùng phải có ít nhất 6 ký tự",
                        },
                        maxLength: {
                            value: 50,
                            message: "Tên người dùng không được quá 50 ký tự",
                        },
                    })}
                    error={!!errors.userName}
                    helperText={errors?.userName?.message as string}
                />
                <TextField
                    margin="normal"
                    fullWidth
                    label="Mật khẩu"
                    type="password"
                    autoComplete="current-password"
                    {...register("password", {
                        required: "Mật khẩu không được để trống",
                        minLength: {
                            value: 6,
                            message: "Mật khẩu phải có ít nhất 6 ký tự",
                        },
                        maxLength: {
                            value: 50,
                            message: "Mật khẩu không được quá 50 ký tự",
                        },
                    })}
                    error={!!errors.password}
                    helperText={errors.password?.message as string}
                />
                <LoadingButton
                    loading={isSubmitting}
                    disabled={!isValid}
                    type="submit"
                    fullWidth
                    variant="contained"
                    sx={{ mt: 3, mb: 2 }}
                >
                    Đăng nhập
                </LoadingButton>
                <Grid container>
                    <Grid item>
                        <Link to="/register" style={{ textDecoration: "none" }}>
                            {"Bạn chưa có tài khoản? Hãy đăng ký"}
                        </Link>
                    </Grid>
                </Grid>
            </Box>
        </Container>
    );
}
