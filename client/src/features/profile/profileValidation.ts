import dayjs from "dayjs";
import * as yup from "yup";

export const profileValidationSchema = yup.object({
    email: yup.string().email().label("Email").required(),
    phoneNumber: yup.string().label("Số điện thoại").required().min(10).max(12),
    fullName: yup.string().label("Họ và tên").required().min(6).max(100),
    dateOfBirth: yup
        .string()
        .label("Ngày sinh")
        .required()
        .test("is-valid-date", "Ngày sinh không hợp lệ", (value) => {
            const selectedDate = dayjs(value);
            const today = dayjs();
            const isValidDate = value && selectedDate.isBefore(today, "day");
            if (!isValidDate) {
                return new yup.ValidationError(
                    "Ngày sinh không hợp lệ",
                    value,
                    "dateOfBirth"
                );
            }
            return true;
        })
        .test("is-older-than-18", "Tuổi phải lớn hơn 18", (value) => {
            const selectedDate = dayjs(value);
            const today = dayjs();
            const isOlderThan18 = selectedDate.isBefore(
                today.subtract(18, "years"),
                "day"
            );
            if (!isOlderThan18) {
                return new yup.ValidationError(
                    "Tuổi phải lớn hơn 18",
                    value,
                    "dateOfBirth"
                );
            }
            return true;
        }),
    file: yup.mixed().when("imageUrl", {
        is: (value: string) => !value,
        then: (schema) => schema.label("Hình ảnh").required(),
        otherwise: (schema) => schema.notRequired(),
    }),
});
