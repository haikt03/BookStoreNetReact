using BookStoreNetReact.Application.Interfaces.Repositories;
using BookStoreNetReact.Domain.Entities;

namespace BookStoreNetReact.Application.Interfaces
{
    public interface ICategoryRepository: IGenericRepository<Category>
    {
    }
}
