using EduControl.DataBase.ModelBd;

namespace EduControl.Repositories;

public interface IRepository<T>
{
    public Task<Book> Insert(T item);
    public Task<T?> Get(Guid id);
    public Task Remove(T item);
    public Task Find(Guid id);
}