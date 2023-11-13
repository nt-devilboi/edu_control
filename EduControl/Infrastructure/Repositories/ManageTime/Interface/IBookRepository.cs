using EduControl.DataBase.ModelBd;

namespace EduControl.Repositories;

public interface IRepository<T>
{
    public Task<T> Insert(T item);
    public Task<T?> Get(Guid id);

    public Task<List<T>> Get(Account account);
    public Task<Result<T?, GetError>> Get(string name);
    public Task<Result<T, GetError>> Update(T request); 
    public Task Remove(T item);
}