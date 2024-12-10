using BookStoreNetReact.Application.Dtos.Pagination;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class FilterAppUserDto : FilterDto
    {
        public string FullNameSearch { get; set; } = "";
        public string EmailSearch { get; set; } = "";
        public string PhoneNumberSearch { get; set; } = "";
    }
}
