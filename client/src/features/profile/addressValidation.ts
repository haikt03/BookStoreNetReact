import * as yup from "yup";

export const addressValidationSchema = yup.object({
    city: yup.string().label("Tỉnh/Thành phố").required().min(1).max(50),
    district: yup.string().label("Quận/Huyện").required().min(1).max(50),
    ward: yup.string().label("Phường/Xã").required().min(1).max(50),
    specificAddress: yup
        .string()
        .label("Địa chỉ cụ thể")
        .required()
        .min(1)
        .max(100),
});
