import * as yup from "yup";

const OrderStatusEnum = {
    PendingConfirmation: "PendingConfirmation",
    PendingPickup: "PendingPickup",
    PendingDelivery: "PendingDelivery",
    Delivered: "Delivered",
    PendingReturn: "PendingReturn",
    Returned: "Returned",
    Cancelled: "Cancelled",
};

export const orderValidationSchema = yup.object({
    orderStatus: yup
        .string()
        .oneOf(
            Object.values(OrderStatusEnum),
            "Trạng thái đơn hàng không hợp lệ"
        )
        .required(),
    note: yup.string().label("Ghi chú"),
});
