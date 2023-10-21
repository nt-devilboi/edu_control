using System.Collections.Concurrent;
using EduControl.DataBase.ModelBd;
using Vostok.Logging.Abstractions;

namespace EduControl.Repositories.InMemory;

public class InMemoryBook : IRepository<Book>
{
    private readonly ConcurrentDictionary<Guid, Book> _books = new ();
    private readonly ILog _log;
    public InMemoryBook(ILog log)
    {
        _log = log;
    }

    public async Task<Book> Insert(Book item)
    {
        if (!_books.TryAdd(item.Id, item))
        {
            _log.Error("Не добавилось: такая уже есть");
        }

        return item;
    }

    public async Task<Book> Get(Guid id)
    {
        if (!_books.ContainsKey(id))
        {
            _log.Info("С таким нету");
        }
        
        return _books[id];
    }

    public Task Remove(Book item)
    {
        throw new NotImplementedException();
    }

    public Task Find(Guid id)
    {
        throw new NotImplementedException();
    }
}