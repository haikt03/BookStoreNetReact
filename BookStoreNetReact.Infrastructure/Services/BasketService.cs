﻿using AutoMapper;
using BookStoreNetReact.Application.Dtos.Basket;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Application.Interfaces.Services;
using BookStoreNetReact.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BookStoreNetReact.Infrastructure.Services
{
    public class BasketService : GenericService<BasketService>, IBasketService
    {
        public BasketService(IMapper mapper, ICloudUploadService cloudUploadService, IUnitOfWork unitOfWork, ILogger<BasketService> logger) : base(mapper, cloudUploadService, unitOfWork, logger)
        {
        }

        public async Task<BasketDto?> GetByUserIdAsync(int userId)
        {
            try
            {
                var basket = await _unitOfWork.BasketRepository.GetByUserIdAsync(userId);
                if (basket == null)
                    throw new NullReferenceException("Basket not found");
                var basketDto = _mapper.Map<BasketDto>(basket);
                return basketDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while adding basket item");
                return null;
            }
        }

        public async Task<BasketDto?> UpdateQuantityAsync(UpdateQuantityDto updateDto, int userId, string type)
        {
            try
            {
                var basket = await _unitOfWork.BasketRepository.GetByUserIdAsync(userId);
                if (basket == null)
                    throw new NullReferenceException("Basket not found");

                var book = await _unitOfWork.BookRepository.GetByIdAsync(updateDto.BookId);
                if (book == null)
                    throw new NullReferenceException("Book not found");

                var basketItem = basket.Items.FirstOrDefault(i => i.BookId == book.Id);
                switch(type)
                {
                    case "plus":
                        if (basketItem == null)
                            basket.Items.Add(new BasketItem { BasketId = basket.Id, BookId = book.Id, Quantity = updateDto.Quantity });
                        else
                        {
                            basketItem.Quantity += updateDto.Quantity;
                        }
                        break;
                    case "minus":
                        if (basketItem != null)
                        {
                            if (basketItem.Quantity <= updateDto.Quantity)
                                basket.Items.Remove(basketItem);
                            else
                                basketItem.Quantity -= updateDto.Quantity;
                        }
                        break;
                    default:
                        throw new ArgumentException("Invalid type");
                }

                await _unitOfWork.CompleteAsync();
                var basketDto = _mapper.Map<BasketDto>(basket);
                return basketDto;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "An error occurred while adding basket item");
                return null;
            }
        }
    }
}