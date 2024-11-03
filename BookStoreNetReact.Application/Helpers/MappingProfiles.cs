using AutoMapper;
using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Dtos.Category;
using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Author
            CreateMap<CreateAuthorDto, Author>()
                .ForMember(dest => dest.PublicId, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
            CreateMap<UpdateAuthorDto, Author>()
                .ForMember(dest => dest.PublicId, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Author, AuthorDto>();

            // Book
            CreateMap<CreateBookDto, Book>()
                .ForMember(dest => dest.PublicId, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());
            CreateMap<UpdateBookDto, Book>()
                .ForMember(dest => dest.PublicId, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

            // Category
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Category, CategoryDto>();
        }
    }
}
