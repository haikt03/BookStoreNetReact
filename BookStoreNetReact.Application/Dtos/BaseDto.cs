namespace BookStoreNetReact.Application.Dtos
{
    public class BaseDto
    {
        protected const string RequiredErrorMessage = "Giá trị này không được để trống";
        protected const string InvalidErrorMessage = "Giá trị này không hợp lệ";
        protected const string MaxLengthErrorMessage = "Giá trị này không được vượt quá {1} ký tự";
        protected const string MinLengthErrorMessage = "Giá trị này không được ít hơn {1} ký tự";
        protected const string RangeErrorMessage = "Giá trị này phải nằm trong khoảng {1} và {2}";
        protected const string EmailErrorMessage = "Email không hợp lệ";
        protected const string PhoneErrorMessage = "Số điện thoại không hợp lệ";
        protected const string DateErrorMessage = "Ngày không hợp lệ";
    }
}
