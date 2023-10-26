using EduControl.Controllers.AppController.model;
using EduControl.DataBase;
using EduControl.DataBase.ModelBd;
using Microsoft.EntityFrameworkCore;
using Vostok.Logging.Abstractions;

namespace EduControl.Repositories;

public class BookRepository : IRepository<Book>
{
    private static readonly Result<Book,GetError> BookNotFound = new Result<Book, GetError>(GetError.NotFound, "Book not found");
    private readonly ControlTimeDb _bd;
    private readonly ILog _log;
    
    public BookRepository(ControlTimeDb bd, ILog log)
    {
        _bd = bd;
        _log = log;
    }

    public async Task<Book> Insert(Book book)
    {
        var loadedBook = await _bd.Books.AddAsync(book);
        await _bd.SaveChangesAsync();
        _log.Info($"book {loadedBook.Entity} is {loadedBook.State}");
        return loadedBook.Entity;
    }

    public async Task<Book> Get(Guid id)
    {
        return await _bd.Books.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<Result<Book?, GetError>> Get(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Book, GetError>> Update(Book requestBookChanged)
    {
        var entity = await  _bd.Books.FirstOrDefaultAsync(x => x.Id == requestBookChanged.Id);
        if (entity == null) return BookNotFound;

        entity.Desc = requestBookChanged.Desc;
        entity.Name = requestBookChanged.Name;
        await _bd.SaveChangesAsync();

        return new Result<Book, GetError>(entity);
    }

    public Task Remove(Book item)
    {
        throw new NotImplementedException();
    }

    public async Task Find(Guid id)
    {
    }
}