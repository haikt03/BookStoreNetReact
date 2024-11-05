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
                .ForMember(dest => dest.PublicId, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
            CreateMap<UpdateAppUserDto, AppUser>()
                .ForMember(dest => dest.PublicId, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AppUser, AppUserDto>();
            CreateMap<AppUser, AppUserWithTokenDto>()
                .ForMember(dest => dest.AccessToken, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshToken, opt => opt.Ignore());
            CreateMap<AppUser, DetailAppUserDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            // Address
            CreateMap<UserAddress, UserAddressDto>();
            CreateMap<CreateUserAddressDto, UserAddress>();
            CreateMap<UpdateUserAddressDto, UserAddress>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Author
            CreateMap<CreateAuthorDto, Author>()
                .ForMember(dest => dest.PublicId, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
            CreateMap<UpdateAuthorDto, Author>()
                .ForMember(dest => dest.PublicId, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Author, AuthorDto>();
            CreateMap<Author, DetailAuthorDto>();

            // Book
            CreateMap<CreateBookDto, Book>()
                .ForMember(dest => dest.PublicId, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
            CreateMap<UpdateBookDto, Book>()
                .ForMember(dest => dest.PublicId, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
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
    }
}
