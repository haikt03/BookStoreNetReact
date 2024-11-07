using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Domain.Attributes
{
    public class DateOfBirthRangeAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateOnly dateOfBirth)
            {
                var currentDate = DateOnly.FromDateTime(DateTime.Now);
                var minDate = new DateOnly(1900, 1, 1);
                return dateOfBirth >= minDate && dateOfBirth <= currentDate;
            }
            return false;
        }
    }
}
