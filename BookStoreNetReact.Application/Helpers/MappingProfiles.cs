using AutoMapper;
using BookStoreNetReact.Application.Dtos.AppUser;
using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Dtos.Category;
using BookStoreNetReact.Application.Dtos.UserAddress;
using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // AppUser
            CreateMap<RegisterDto, AppUser>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => ParseDate(src.DateOfBirth)));
            CreateMap<UpdateAppUserDto, AppUser>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => ParseDate(src.DateOfBirth)))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AppUser, AppUserDto>();
            CreateMap<AppUser, DetailAppUserDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            // Address
            CreateMap<UserAddress, UserAddressDto>();
            CreateMap<UpdateUserAddressDto, UserAddress>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Author
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Author, AuthorDto>();
            CreateMap<Author, DetailAuthorDto>();

            // Book
            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Book, BookDto>();
            CreateMap<Book, DetailBookDto>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            // Category
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, DetailCategoryDto>()
                .ForMember(dest => dest.PCategory, opt => opt.MapFrom(src => src.PCategory));
        }

        private static DateOnly? ParseDate(string? dateOfBirth)
        {
            return DateOnly.TryParse(dateOfBirth, out var parsedDate) ? parsedDate : null;
        }
    }
}
