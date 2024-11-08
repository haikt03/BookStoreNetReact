namespace BookStoreNetReact.Application.Helpers
{
    public static class ValidationErrorMessages
    {
        public const string EmailAddress = "Email không hợp lệ";
        public const string PhoneNumber = "Số điện thoại không hợp lệ";
        public const string DateOfBirth = "Ngày sinh không hợp lệ";

        public static string NotEmpty(string? name = "Giá trị này")
        {
            return $"{name} không được để trống";
        }

        public static string Length(string? name = "Giá trị này")
        {
            return $"{name} phải có độ dài từ {{MinLength}} đến {{MaxLength}} ký tự";
        }

        public static string InclusiveBetween(string? name = "Giá trị này")
        {
            return $"{name} phải nằm trong khoảng {{From}} và {{To}}";
        }

        public static string GreaterThanOrEqualTo(string? name = "Giá trị này")
        {
            return $"{name} phải lớn hơn hoặc bằng {{ComparisonValue}}";
        }
    }
}
