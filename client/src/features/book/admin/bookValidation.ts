import * as yup from "yup";

export const bookValidationSchema = yup.object({
    name: yup.string().label("Tên sách").required().min(6).max(250),
    category: yup.string().label("Thể loại").required().min(6).max(50),
    translator: yup.string().label("Người dịch").min(1).max(100),
    publisher: yup.string().label("Nhà xuất bản").required().min(1).max(100),
    publishedYear: yup
        .number()
        .label("Năm xuất bản")
        .required()
        .min(1800)
        .max(new Date().getFullYear()),
    language: yup.string().label("Ngôn ngữ").required().min(6).max(50),
    weight: yup.number().label("Trọng lượng").required().min(1),
    numberOfPages: yup.number().label("Số trang").required().min(1),
    form: yup.string().label("Hình thức").required().min(6).max(50),
    description: yup.string().label("Mô tả").required().min(50).max(500),
    price: yup.number().label("Giá").required().min(1),
    discount: yup.number().label("Giảm giá").required().min(0),
    quantityInStock: yup.number().label("Số lượng trong kho").required().min(0),
    authorId: yup.number().label("Id tác giả").required().min(1),
    file: yup.mixed().when("imageUrl", {
        is: (value: string) => !value,
        then: (schema) => schema.label("Hình ảnh").required(),
        otherwise: (schema) => schema.notRequired(),
    }),
});
