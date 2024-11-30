import * as Yup from "yup";

Yup.setLocale({
    mixed: {
        required: "${label} không được để trống",
        notType: "${label} không hợp lệ",
    },
    string: {
        email: "Email không hợp lệ",
        min: "${label} phải có ít nhất ${min} ký tự",
        max: "${label} không được vượt quá ${max} ký tự",
        length: "${label} phải đúng ${length} ký tự",
    },
    number: {
        min: "${label} phải lớn hơn hoặc bằng ${min}",
        max: "${label} phải nhỏ hơn hoặc bằng ${max}",
        lessThan: "${label} phải nhỏ hơn ${less}",
        moreThan: "${label} phải lớn hơn ${more}",
        positive: "${label} phải là số dương",
        negative: "${label} phải là số âm",
        integer: "${label} phải là số nguyên",
    },
});

export function currencyFormat(amount: number) {
    return amount.toLocaleString("vi", {
        style: "currency",
        currency: "VND",
    });
}

export const bookSortOptions = [
    { value: "nameAsc", label: "Tên sách (↑)" },
    { value: "nameDesc", label: "Tên sách (↓)" },
    { value: "publishedYearAsc", label: "Năm xuất bản (↑)" },
    { value: "publishedYearDesc", label: "Năm xuất bản (↓)" },
    { value: "priceAsc", label: "Giá (↑)" },
    { value: "priceDesc", label: "Giá (↓)" },
    { value: "priceAfterDiscountAsc", label: "Giá sau khi giảm (↑)" },
    { value: "priceAfterDiscountDesc", label: "Giá sau khi giảm (↓)" },
    { value: "discountAsc", label: "Giảm giá (↑)" },
    { value: "discountDesc", label: "Giảm giá(↓)" },
    { value: "quantityInStockAsc", label: "Số lượng trong kho (↑)" },
    { value: "quantityInStockDesc", label: "Số lượng trong kho(↓)" },
];

export const authorSortOptions = [
    { value: "fullNameAsc", label: "Tên tác giả (↑)" },
    { value: "fullNameDesc", label: "Tên tác giả (↓)" },
    { value: "countryAsc", label: "Quốc tịch (↑)" },
    { value: "countryDesc", label: "Quốc tịch (↓)" },
];
