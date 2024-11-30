import * as yup from "yup";

export const authorValidationSchema = yup.object({
    fullName: yup.string().label("Tên tác giả").required().min(1).max(100),
    country: yup.string().label("Quốc tịch").required().min(1).max(50),
    biography: yup.string().label("Tiểu sử").required().min(50).max(500),
    file: yup.mixed().when("imageUrl", {
        is: (value: string) => !value,
        then: (schema) => schema.label("Hình ảnh").required(),
        otherwise: (schema) => schema.notRequired(),
    }),
});
