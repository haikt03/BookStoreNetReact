import { yupResolver } from "@hookform/resolvers/yup";
import { Controller, FieldValues, useForm } from "react-hook-form";
import { orderValidationSchema } from "./orderValidation";
import useOrders from "../../app/hooks/userOrders";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { useEffect } from "react";
import { getOrderAsync, setOrder } from "./orderSlice";
import { OrderDetail } from "../../app/models/order";
import agent from "../../app/api/agent";
import LoadingComponent from "../../app/layout/LoadingComponent";
import {
    Box,
    Button,
    FormControl,
    Grid,
    InputLabel,
    MenuItem,
    Paper,
    Select,
    Typography,
} from "@mui/material";
import AppTextInput from "../../app/components/AppTextInput";
import { LoadingButton } from "@mui/lab";

interface Props {
    orderId?: number;
    cancelEdit: () => void;
}

export default function OrderForm({ orderId, cancelEdit }: Props) {
    const {
        control,
        reset,
        handleSubmit,
        watch,
        formState: { isDirty, isSubmitting },
    } = useForm({
        mode: "onTouched",
        resolver: yupResolver<any>(orderValidationSchema),
    });
    const { filter } = useOrders();
    const watchFile = watch("file", null);
    const dispatch = useAppDispatch();
    const { orderDetail, orderDetailLoaded } = useAppSelector(
        (state) => state.order
    );

    useEffect(() => {
        if (orderId) dispatch(getOrderAsync(orderId));
    }, []);

    useEffect(() => {
        if (orderDetail && !watchFile && !isDirty)
            reset({
                ...orderDetail,
            });
        return () => {
            if (watchFile) URL.revokeObjectURL(watchFile.preview);
        };
    }, [orderDetail, reset, watchFile, isDirty]);

    async function handleSubmitData(data: FieldValues) {
        let response: OrderDetail;
        if (orderDetail) {
            response = await agent.order.updateOrderStatus(orderDetail.id, {
                orderStatus: data.orderStatus,
                note: data.note,
            });
            dispatch(setOrder(response));
        }
        cancelEdit();
    }

    if (orderId && !orderDetailLoaded) {
        return <LoadingComponent />;
    }

    return (
        <Box component={Paper} sx={{ p: 4 }}>
            <Typography
                variant="h4"
                gutterBottom
                sx={{ mb: 4 }}
                color="primary.light"
            >
                Cập nhật trạng thái đơn hàng
            </Typography>
            <form onSubmit={handleSubmit(handleSubmitData)}>
                <Grid container spacing={3}>
                    <Grid item xs={12} sm={12}>
                        <FormControl fullWidth>
                            <InputLabel>Trạng thái đơn hàng</InputLabel>
                            <Controller
                                control={control}
                                name="orderStatus"
                                defaultValue=""
                                render={({ field }) => (
                                    <Select
                                        {...field}
                                        label="Trạng thái đơn hàng"
                                    >
                                        {filter.orderStatuses.map(
                                            (status, index) => (
                                                <MenuItem
                                                    key={index}
                                                    value={status}
                                                >
                                                    {status}
                                                </MenuItem>
                                            )
                                        )}
                                    </Select>
                                )}
                            />
                        </FormControl>
                    </Grid>
                    <Grid item xs={12}>
                        <AppTextInput
                            multiline={true}
                            rows={4}
                            control={control}
                            name="note"
                            label="Ghi chú"
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
                        Cập nhât
                    </LoadingButton>
                </Box>
            </form>
        </Box>
    );
}
