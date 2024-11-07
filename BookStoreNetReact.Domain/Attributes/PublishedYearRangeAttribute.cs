using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Domain.Attributes
{
    public class PublishedYearRangeAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is int year)
            {
                var currentYear = DateTime.Now.Year;
                return year >= 1800 && year <= currentYear;
            }
            return false;
        }
    }
}
