import * as yup from "yup";

export const changePasswordValidationSchema = yup.object({
    currentPassword: yup
        .string()
        .label("Mật khẩu hiện tại")
        .required()
        .min(6)
        .max(50),
    newPassword: yup
        .string()
        .label("Mật khẩu mới")
        .required()
        .min(6)
        .max(50)
        .notOneOf(
            [yup.ref("currentPassword")],
            "Mật khẩu mới không được trùng với mật khẩu hiện tại."
        ),
});
