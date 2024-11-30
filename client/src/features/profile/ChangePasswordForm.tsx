import { FieldValues, useForm } from "react-hook-form";
import { changePasswordValidationSchema } from "./changePasswordValidation";
import { yupResolver } from "@hookform/resolvers/yup";
import agent from "../../app/api/agent";
import { Box, Button, Container, Grid, Paper, Typography } from "@mui/material";
import AppTextInput from "../../app/components/AppTextInput";
import { LoadingButton } from "@mui/lab";

interface Props {
    cancelEdit: () => void;
}

export default function ChangePasswordForm({ cancelEdit }: Props) {
    const {
        control,
        handleSubmit,
        formState: { isSubmitting },
    } = useForm({
        mode: "onTouched",
        resolver: yupResolver<any>(changePasswordValidationSchema),
    });

    async function handleSubmitData(data: FieldValues) {
        await agent.account.changePassword(data);
        cancelEdit();
    }

    return (
        <Container maxWidth="sm" component={Paper} sx={{ p: 4 }}>
            <Typography
                variant="h4"
                gutterBottom
                sx={{ mb: 4 }}
                color="primary.light"
            >
                Thay đổi mật khẩu
            </Typography>
            <form onSubmit={handleSubmit(handleSubmitData)}>
                <Grid container spacing={3}>
                    <Grid item xs={12} sm={12}>
                        <AppTextInput
                            type="password"
                            autoComplete="current-password"
                            control={control}
                            name="currentPassword"
                            label="Mật khẩu hiện tại"
                        />
                    </Grid>
                    <Grid item xs={12} sm={12}>
                        <AppTextInput
                            type="password"
                            autoComplete="new-password"
                            control={control}
                            name="newPassword"
                            label="Mật khẩu mới"
                        />
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
