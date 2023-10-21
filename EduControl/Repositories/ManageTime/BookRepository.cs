using EduControl.DataBase;
using EduControl.DataBase.ModelBd;
using Microsoft.EntityFrameworkCore;
using Vostok.Logging.Abstractions;

namespace EduControl.Repositories;

public class BookRepository : IRepository<Book>
{
    private readonly ControlTimeDb _tableContext;
    private readonly ILog _log;
    
    public BookRepository(ControlTimeDb tableContext, ILog log)
    {
        _tableContext = tableContext;
        _log = log;
    }

    public async Task<Book> Insert(Book book)
    {
        var loadedBook = await _tableContext.Books.AddAsync(book);
        await _tableContext.SaveChangesAsync();
        _log.Info($"book {loadedBook.Entity} is {loadedBook.State}");
        return loadedBook.Entity;
    }

    public async Task<Book> Get(Guid id)
    {
        return await _tableContext.Books.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task Remove(Book item)
    {
        throw new NotImplementedException();
    }

    public async Task Find(Guid id)
    {
        
    }
}