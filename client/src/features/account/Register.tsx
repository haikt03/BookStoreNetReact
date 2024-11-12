import {
    Container,
    Paper,
    Avatar,
    Typography,
    Box,
    TextField,
    Grid,
} from "@mui/material";
import { Controller, useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router-dom";
import { LockOutlined } from "@mui/icons-material";
import { RegisterRequest } from "../../app/models/user";
import { useAppDispatch } from "../../app/store/configureStore";
import { registerAsync } from "./accountSlice";
import { LoadingButton } from "@mui/lab";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import dayjs from "dayjs";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { toast } from "react-toastify";

export default function Register() {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const {
        register,
        handleSubmit,
        control,
        reset,
        formState: { isSubmitting, errors, isValid },
    } = useForm<RegisterRequest>({
        mode: "onTouched",
    });

    async function submitForm(data: RegisterRequest) {
        data.dateOfBirth = dayjs(data.dateOfBirth).format("YYYY-MM-DD");
        await dispatch(registerAsync(data));
        reset();
        toast.success("Đăng ký thành công");
        navigate("/login");
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
                <LockOutlined />
            </Avatar>
            <Typography component="h1" variant="h5">
                Đăng ký
            </Typography>
            <Box
                component="form"
                onSubmit={handleSubmit(submitForm)}
                noValidate
                sx={{ mt: 1 }}
            >
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    label="Họ và tên"
                    {...register("fullName", {
                        required: "Họ và tên không được để trống",
                        minLength: {
                            value: 6,
                            message: "Họ và tên phải có ít nhất 6 ký tự",
                        },
                        maxLength: {
                            value: 100,
                            message: "Họ và tên không được quá 50 ký tự",
                        },
                    })}
                    error={!!errors.fullName}
                    helperText={errors?.fullName?.message as string}
                />
                <LocalizationProvider dateAdapter={AdapterDayjs}>
                    <Controller
                        name="dateOfBirth"
                        control={control}
                        rules={{
                            required: "Ngày sinh không được để trống",
                            validate: (value) => {
                                const selectedDate = dayjs(value);
                                const today = dayjs();
                                const isValidDate =
                                    value &&
                                    selectedDate.isBefore(today, "day");
                                const isOlderThan18 = selectedDate.isBefore(
                                    today.subtract(18, "years"),
                                    "day"
                                );

                                if (!isValidDate) {
                                    return "Ngày sinh không hợp lệ";
                                } else if (!isOlderThan18) {
                                    return "Tuổi phải lớn hơn 18";
                                }
                                return true;
                            },
                        }}
                        render={({ field }) => (
                            <DatePicker
                                {...field}
                                label="Ngày sinh"
                                format="DD/MM/YYYY"
                                value={field.value ? dayjs(field.value) : null}
                                onChange={(date) => {
                                    field.onChange(date);
                                    field.onBlur();
                                }}
                                slotProps={{
                                    textField: {
                                        fullWidth: true,
                                        error: !!errors.dateOfBirth,
                                        helperText: errors?.dateOfBirth
                                            ?.message as string,
                                    },
                                }}
                                sx={{ width: "100%" }}
                            />
                        )}
                    />
                </LocalizationProvider>
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    autoFocus
                    label="Tên người dùng"
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
                    required
                    fullWidth
                    label="Email"
                    autoComplete="email"
                    {...register("email", {
                        required: "Email không được để trống",
                        pattern: {
                            value: /^\w+[\w-.]*@\w+((-\w+)|(\w*))\.[a-z]{2,3}$/,
                            message: "Email không hợp lệ",
                        },
                    })}
                    error={!!errors.email}
                    helperText={errors?.email?.message as string}
                />
                <TextField
                    margin="normal"
                    required
                    fullWidth
                    autoFocus
                    label="Số điện thoại"
                    {...register("phoneNumber", {
                        required: "Số điện thoại không được để trống",
                        pattern: {
                            value: /^0\d{9}$|^\+84\d{10}$/,
                            message: "Số điện thoại không hợp lệ",
                        },
                    })}
                />
                <TextField
                    margin="normal"
                    required
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
                    helperText={errors?.password?.message as string}
                />
                <LoadingButton
                    loading={isSubmitting}
                    disabled={!isValid}
                    type="submit"
                    fullWidth
                    variant="contained"
                    sx={{ mt: 3, mb: 2 }}
                >
                    Đăng ký
                </LoadingButton>
                <Grid container>
                    <Grid item>
                        <Link to="/login" style={{ textDecoration: "none" }}>
                            {"Bạn đã có tài khoản? Hãy đăng nhập"}
                        </Link>
                    </Grid>
                </Grid>
            </Box>
        </Container>
    );
}
