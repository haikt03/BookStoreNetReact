namespace BookStoreNetReact.Application.Helpers
{
    public static class ValidationErrorMessages
    {
        public const string Required = "Giá trị này không được để trống";
        public const string Invalid = "Giá trị này không hợp lệ";
        public const string Length = "Giá trị này không được ít hơn {MinLength} ký tự và vượt quá {MaxLength} ký tự";
        public const string Range = "Giá trị này phải nằm trong khoảng {From} và {To}";
        public const string Greater = "Giá trị này phải lớn hơn {ComparisonValue}";
        public const string Email = "Email không hợp lệ";
        public const string PhoneNumber = "Số điện thoại không hợp lệ";
        public const string Date = "Ngày không hợp lệ";
    }
}
