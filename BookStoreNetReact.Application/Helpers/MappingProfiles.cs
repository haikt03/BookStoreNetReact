using AutoMapper;
using BookStoreNetReact.Application.Dtos.AppUser;
using BookStoreNetReact.Application.Dtos.Author;
using BookStoreNetReact.Application.Dtos.Basket;
using BookStoreNetReact.Application.Dtos.Book;
using BookStoreNetReact.Application.Dtos.UserAddress;
using BookStoreNetReact.Application.Dtos.Order;
using BookStoreNetReact.Domain.Entities;
using BookStoreNetReact.Domain.Entities.OrderAggregate;

namespace BookStoreNetReact.Application.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // AppUser
            CreateMap<RegisterDto, AppUser>();
            CreateMap<UpdateAppUserDto, AppUser>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AppUser, AppUserDto>();
            CreateMap<AppUser, AppUserDetailDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            // Address
            CreateMap<UserAddress, UserAddressDto>();
            CreateMap<CreateUserAddresssDto, UserAddress>();
            CreateMap<UpdateUserAddressDto, UserAddress>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Author
            CreateMap<CreateAuthorDto, Author>();
            CreateMap<UpdateAuthorDto, Author>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Author, AuthorDto>();
            CreateMap<Author, AuthorDetailDto>();
            CreateMap<Author, AuthorForUpsertBookDto>();

            // Basket
            CreateMap<Basket, BasketDto>();
            CreateMap<BasketItem, BasketItemDto>();

            // Order
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus.ToString()))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus.ToString()));
            CreateMap<Order, OrderDetailDto>()
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus.ToString()))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus.ToString()));
            CreateMap<OrderItem, OrderItemDto>();

            // Book
            CreateMap<CreateBookDto, Book>();
            CreateMap<UpdateBookDto, Book>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Book, BookDto>();
            CreateMap<Book, BookDetailDto>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author));
        }
    }
}
