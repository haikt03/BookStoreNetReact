import { FieldValues, useForm } from "react-hook-form";
import { useAppDispatch } from "../../app/store/configureStore";
import agent from "../../app/api/agent";
import { getCurrentUserAsync } from "../account/accountSlice";
import { Box, Button, Container, Grid, Paper, Typography } from "@mui/material";
import AppTextInput from "../../app/components/AppTextInput";
import { LoadingButton } from "@mui/lab";
import { useState } from "react";
import AppAddress from "../../app/components/AppAddress";

interface Props {
    specificAddress: string;
    cancelEdit: () => void;
}

export default function AddressForm({ specificAddress, cancelEdit }: Props) {
    const {
        control,
        handleSubmit,
        formState: { isSubmitting },
        setValue,
    } = useForm({
        mode: "onTouched",
    });
    const dispatch = useAppDispatch();

    const [address, setAddress] = useState({
        city: "",
        district: "",
        ward: "",
        specificAddress: specificAddress || "",
    });

    const handleAddressChange = (newAddress: any) => {
        setAddress(newAddress);
        setValue("city", newAddress.city);
        setValue("district", newAddress.district);
        setValue("ward", newAddress.ward);
        setValue("specificAddress", newAddress.specificAddress);
    };

    async function handleSubmitData(data: FieldValues) {
        await agent.account.updateUserAddress(data);
        await dispatch(getCurrentUserAsync());
        cancelEdit();
    }

    return (
        <Container component={Paper} maxWidth="sm" sx={{ p: 4 }}>
            <Typography
                variant="h4"
                gutterBottom
                sx={{ mb: 4 }}
                color="primary.light"
            >
                Cập nhật thông tin địa chỉ
            </Typography>
            <form onSubmit={handleSubmit(handleSubmitData)}>
                <Grid container spacing={3}>
                    <Grid item xs={12} sm={12}>
                        <AppAddress
                            address={address}
                            onChange={handleAddressChange}
                        />
                    </Grid>

                    <Grid item xs={12} sm={12}>
                        <AppTextInput
                            control={control}
                            name="specificAddress"
                            label="Địa chỉ cụ thể"
                            value={address.specificAddress}
                            onChange={(e) =>
                                setAddress({
                                    ...address,
                                    specificAddress: e.target.value,
                                })
                            }
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
