import { Container, Paper, Avatar, Typography, Box, Grid } from "@mui/material";
import { Controller, FieldValues, useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router-dom";
import { PersonAdd } from "@mui/icons-material";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { registerAsync, resetRegisterStatus } from "./accountSlice";
import { LoadingButton } from "@mui/lab";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import dayjs from "dayjs";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { toast } from "react-toastify";
import { useEffect, useState } from "react";
import AppTextInput from "../../app/components/AppTextInput";
import AppAddress from "../../app/components/AppAddress";

export default function Register() {
    const dispatch = useAppDispatch();
    const { registerStatus } = useAppSelector((state) => state.account);
    const navigate = useNavigate();
    const {
        handleSubmit,
        control,
        reset,
        setValue,
        formState: { isSubmitting, errors, isValid },
    } = useForm({
        mode: "onTouched",
    });

    const [address, setAddress] = useState({
        city: "",
        district: "",
        ward: "",
        specificAddress: "",
    });

    useEffect(() => {
        setValue("specificAddress", address.specificAddress);
    }, [address.specificAddress, setValue]);

    useEffect(() => {
        if (registerStatus) {
            reset();
            toast.success("Đăng ký thành công");
            navigate("/login");
            dispatch(resetRegisterStatus());
        }
    }, [registerStatus, reset, navigate, dispatch]);

    async function submitForm(data: FieldValues) {
        data.dateOfBirth = dayjs(data.dateOfBirth).format("YYYY-MM-DD");
        data.address = address;
        delete data.specificAddress;
        await dispatch(registerAsync(data));
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
                <PersonAdd />
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
                <AppTextInput
                    name="fullName"
                    required
                    control={control}
                    label="Họ và tên"
                    rules={{
                        required: "Họ và tên không được để trống",
                        minLength: {
                            value: 6,
                            message: "Họ và tên phải có ít nhất 6 ký tự",
                        },
                        maxLength: {
                            value: 100,
                            message: "Họ và tên không được quá 100 ký tự",
                        },
                    }}
                />
                <AppTextInput
                    name="userName"
                    required
                    control={control}
                    label="Tên người dùng"
                    rules={{
                        required: "Tên người dùng không được để trống",
                        minLength: {
                            value: 6,
                            message: "Tên người dùng phải có ít nhất 6 ký tự",
                        },
                        maxLength: {
                            value: 50,
                            message: "Tên người dùng không được quá 50 ký tự",
                        },
                    }}
                />
                <AppTextInput
                    name="email"
                    required
                    control={control}
                    label="Email"
                    type="email"
                    autoComplete="username"
                    rules={{
                        required: "Email không được để trống",
                        pattern: {
                            value: /^\w+[\w-.]*@\w+((-\w+)|(\w*))\.[a-z]{2,3}$/,
                            message: "Email không hợp lệ",
                        },
                    }}
                />
                <Grid container spacing={2}>
                    <Grid item xs={6}>
                        <AppTextInput
                            name="phoneNumber"
                            required
                            control={control}
                            label="Số điện thoại"
                            rules={{
                                required: "Số điện thoại không được để trống",
                                pattern: {
                                    value: /^0\d{9}$|^\+84\d{10}$/,
                                    message: "Số điện thoại không hợp lệ",
                                },
                            }}
                        />
                    </Grid>

                    <Grid item xs={6}>
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
                                        const isOlderThan18 =
                                            selectedDate.isBefore(
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
                                        value={
                                            field.value
                                                ? dayjs(field.value)
                                                : null
                                        }
                                        onChange={(date) => {
                                            field.onChange(date);
                                            field.onBlur();
                                        }}
                                        slotProps={{
                                            textField: {
                                                margin: "normal",
                                                fullWidth: true,
                                                error: !!errors.dateOfBirth,
                                                helperText: errors?.dateOfBirth
                                                    ?.message as string,
                                            },
                                        }}
                                    />
                                )}
                            />
                        </LocalizationProvider>
                    </Grid>
                </Grid>
                <AppAddress
                    address={address}
                    onChange={(newAddress) => setAddress(newAddress)}
                />
                <AppTextInput
                    name="specificAddress"
                    value={address.specificAddress}
                    required
                    control={control}
                    label="Địa chỉ cụ thể"
                    rules={{
                        required: "Địa chỉ cụ thể không được để trống",
                        maxLength: {
                            value: 100,
                            message: "Địa chỉ cụ thể không được quá 100 ký tự",
                        },
                    }}
                    onChange={(e) => {
                        const newAddress = e.target.value;
                        setAddress({ ...address, specificAddress: newAddress });
                        setValue("specificAddress", newAddress);
                    }}
                />
                <AppTextInput
                    name="password"
                    required
                    control={control}
                    label="Mật khẩu"
                    type="password"
                    autoComplete="current-password"
                    rules={{
                        required: "Mật khẩu không được để trống",
                        minLength: {
                            value: 6,
                            message: "Mật khẩu phải có ít nhất 6 ký tự",
                        },
                        maxLength: {
                            value: 50,
                            message: "Mật khẩu không được quá 50 ký tự",
                        },
                    }}
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
