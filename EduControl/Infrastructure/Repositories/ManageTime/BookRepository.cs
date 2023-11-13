using EduControl.Controllers.AppController.model;
using EduControl.DataBase;
using EduControl.DataBase.ModelBd;
using Microsoft.EntityFrameworkCore;
using Vostok.Logging.Abstractions;

namespace EduControl.Repositories;

public class BookRepository : RepositoryBase<Book>, IRepository<Book>
{
    private static readonly Result<Book, GetError> BookNotFound =
        new Result<Book, GetError>(GetError.NotFound, "Book not found");

    private readonly ControlTimeDb _db;
    private readonly ILog _log;

    public BookRepository(ControlTimeDb db, ILog log) : base(db, log)
    {
        _db = db;
        _log = log;
    }

    public async Task<List<Book>> Get(Account account)
    {
        return await _db.Books.Where(x => x.UserId == account.Id).ToListAsync();
    }

    public async Task<Result<Book, GetError>> Update(Book requestBookChanged)
    {
        var entity = await _db.Books.FirstOrDefaultAsync(x => x.Id == requestBookChanged.Id);
        if (entity == null) return BookNotFound;
        entity.Desc = requestBookChanged.Desc;
        entity.Name = requestBookChanged.Name;
        await _db.SaveChangesAsync();

        return new Result<Book, GetError>(entity);
    }
}