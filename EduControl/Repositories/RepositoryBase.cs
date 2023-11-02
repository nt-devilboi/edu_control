using EduControl.DataBase;
using EduControl.DataBase.ModelBd;
using Microsoft.EntityFrameworkCore;
using Vostok.Logging.Abstractions;

namespace EduControl.Controllers.AppController.model;

public abstract class RepositoryBase<T> where T : class, IName, IEntity
{
    private readonly ControlTimeDb _db;
    private readonly ILog _log;
    protected RepositoryBase(ControlTimeDb db, ILog log)
    {
        _db = db;
        _log = log;
    }

    
    
    public async Task<T> Insert(T book)
    {
        var loadedEnitity = await _db.Set<T>().AddAsync(book);
        await _db.SaveChangesAsync();
        _log.Info($"{loadedEnitity.Entity} is {loadedEnitity.State}");
        return loadedEnitity.Entity;
    }
    
    public async Task<T?> Get(Guid id)
    {
        return await _db.Set<T>().FirstOrDefaultAsync(x => x.Id == id); //todo: ChangeToResult;
    }
    
    public async Task<Result<T?, GetError>> Get(string name)
    {
        var book = await _db.Set<T>().FirstOrDefaultAsync(x => x.Name == name);
        return book switch
        {
            null => new Result<T?, GetError>(GetError.NotFound, "Book not found"),
            _ => new Result<T?, GetError>(book)
        };
    }
    
    public async Task Remove(T item)
    {
        _db.Set<T>().Remove(item);
        await _db.SaveChangesAsync();
    }
}

