﻿namespace BookStoreNetReact.Application.Dtos.Pagination
{
    public class PaginationDto
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
        public int PageIndex { get; set; } = 1;
    }
}
