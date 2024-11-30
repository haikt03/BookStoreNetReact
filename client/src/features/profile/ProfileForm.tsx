import { yupResolver } from "@hookform/resolvers/yup";
import { Controller, FieldValues, useForm } from "react-hook-form";
import { profileValidationSchema } from "./profileValidation";
import { useAppDispatch } from "../../app/store/configureStore";
import { UserDetail } from "../../app/models/user";
import { useEffect } from "react";
import agent from "../../app/api/agent";
import { getCurrentUserAsync } from "../account/accountSlice";
import { Box, Button, Container, Grid, Paper, Typography } from "@mui/material";
import AppTextInput from "../../app/components/AppTextInput";
import AppDropzone from "../../app/components/AppDropzone";
import { LoadingButton } from "@mui/lab";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import dayjs from "dayjs";

interface Props {
    user: UserDetail;
    cancelEdit: () => void;
}

export default function ProfileForm({ user, cancelEdit }: Props) {
    const {
        control,
        reset,
        handleSubmit,
        watch,
        formState: { isDirty, isSubmitting },
    } = useForm({
        mode: "onTouched",
        resolver: yupResolver<any>(profileValidationSchema),
    });
    const watchFile = watch("file", null);
    const dispatch = useAppDispatch();

    useEffect(() => {
        if (user && !watchFile && !isDirty) reset(user);
        return () => {
            if (watchFile) URL.revokeObjectURL(watchFile.preview);
        };
    }, [user, reset, watchFile, isDirty]);

    async function handleSubmitData(data: FieldValues) {
        data.dateOfBirth = dayjs(data.dateOfBirth).format("YYYY-MM-DD");
        await agent.account.updateMe(data);
        await dispatch(getCurrentUserAsync());
        cancelEdit();
    }

    return (
        <Container component={Paper} maxWidth="md" sx={{ p: 4 }}>
            <Typography
                variant="h4"
                gutterBottom
                sx={{ mb: 4 }}
                color="primary"
            >
                Cập nhật thông tin cá nhân
            </Typography>
            <form onSubmit={handleSubmit(handleSubmitData)}>
                <Grid container spacing={3}>
                    <Grid item xs={12} sm={12}>
                        <AppTextInput
                            control={control}
                            name="fullName"
                            label="Họ và tên"
                        />
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <AppTextInput
                            control={control}
                            name="email"
                            label="Email"
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <AppTextInput
                            control={control}
                            name="phoneNumber"
                            label="Số điện thoại"
                        />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                        <Box
                            sx={{
                                display: "flex",
                                flexDirection: "column",
                                mt: 2,
                            }}
                        >
                            <LocalizationProvider dateAdapter={AdapterDayjs}>
                                <Controller
                                    name="dateOfBirth"
                                    control={control}
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
                                        />
                                    )}
                                />
                            </LocalizationProvider>
                        </Box>
                    </Grid>
                    <Grid item xs={12}>
                        <Box
                            display="flex"
                            justifyContent="space-between"
                            alignItems="center"
                        >
                            <AppDropzone control={control} name="file" />
                            {watchFile ? (
                                <img
                                    src={watchFile.preview}
                                    alt="preview"
                                    style={{ maxHeight: 200 }}
                                />
                            ) : (
                                <img
                                    src={user?.imageUrl}
                                    alt={user?.fullName}
                                    style={{ maxHeight: 200 }}
                                />
                            )}
                        </Box>
                    </Grid>
                </Grid>
                <Box
                    display="flex"
                    justifyContent="space-between"
                    sx={{ mt: 3 }}
                >
                    <Button
                        onClick={cancelEdit}
                        variant="contained"
                        color="inherit"
                    >
                        Huỷ
                    </Button>
                    <LoadingButton
                        loading={isSubmitting}
                        type="submit"
                        variant="contained"
                        color="success"
                    >
                        Cập nhật
                    </LoadingButton>
                </Box>
            </form>
        </Container>
    );
}
