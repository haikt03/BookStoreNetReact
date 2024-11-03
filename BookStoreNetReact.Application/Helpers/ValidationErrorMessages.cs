namespace BookStoreNetReact.Application.Helpers
{
    public static class ValidationErrorMessages
    {
        public const string Required = "Giá trị này không được để trống";
        public const string Invalid = "Giá trị này không hợp lệ";
        public const string ValidLength = "Giá trị này không được ít hơn {MinLength} ký tự và vượt quá {MaxLength} ký tự";
        public const string ValidRange = "Giá trị này phải nằm trong khoảng {From} và {To}";
        public const string GreaterThan = "Giá trị này phải lớn hơn {ComparisonValue}";
        public const string ValidEmail = "Email không hợp lệ";
        public const string ValidPhoneNumber = "Số điện thoại không hợp lệ";
        public const string ValidDate = "Ngày không hợp lệ";

        public static string BeNullOrValidLength(int minLength, int maxLength)
        {
            return $"Giá trị này không được ít hơn {minLength} ký tự và vượt quá {maxLength} ký tự";
        }

        public static string BeNullOrValidRange(int min, int max)
        {
            return $"Giá trị này phải nằm trong khoảng {min} và {max}";
        }

        public static string BeNullOrGreaterThan(int value)
        {
            return $"Giá trị này phải lớn hơn {value}";
        }
    }
}
