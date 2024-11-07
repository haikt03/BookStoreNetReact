using AutoMapper;
using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace BookStoreNetReact.Infrastructure.Services
{
    public class GenericService<T>
    {
        protected readonly IMapper _mapper;
        protected readonly ICloudUploadService _cloudUploadService;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ILogger<T> _logger;
        public GenericService(IMapper mapper, ICloudUploadService cloudUploadService, IUnitOfWork unitOfWork, ILogger<T> logger)
        {
            _mapper = mapper;
            _cloudUploadService = cloudUploadService;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
    }
}
