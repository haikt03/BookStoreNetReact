﻿using BookStoreNetReact.Application.Dtos.UserAddress;
using System.ComponentModel.DataAnnotations;

namespace BookStoreNetReact.Application.Dtos.AppUser
{
    public class RegisterDto
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
        public required string FullName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public required CreateUserAddressDto Address { get; set; }
    }
}
