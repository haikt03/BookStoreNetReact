import { Typography, Grid } from "@mui/material";
import { useFormContext } from "react-hook-form";
import AppTextInput from "../../app/components/AppTextInput";
import { Address } from "../../app/models/address";
import AppAddress from "../../app/components/AppAddress";

export default function ShippingAddressForm() {
    const { control, setValue, watch } = useFormContext();
    const address: Address = watch("address") || {
        city: "",
        district: "",
        ward: "",
        specificAddress: "",
    };

    const handleAddressChange = (updatedAddress: Address) => {
        setValue("address", updatedAddress);
    };

    const handleSpecificAddressChange = (value: string) => {
        setValue("address", {
            ...address,
            specificAddress: value,
        });
    };

    return (
        <>
            <Typography variant="h6" gutterBottom color="primary.light">
                Địa chỉ giao hàng
            </Typography>
            <Grid container spacing={3}>
                <Grid item xs={12}>
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
                            handleSpecificAddressChange(e.target.value)
                        }
                    />
                </Grid>
            </Grid>
        </>
    );
}
