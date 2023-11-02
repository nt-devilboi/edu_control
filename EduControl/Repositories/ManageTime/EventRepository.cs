using EduControl.DataBase;
using EduControl.DataBase.ModelBd;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Vostok.Logging.Abstractions;

namespace EduControl.Repositories;

public class EventRepository : IEventRepository
{
    private readonly ControlTimeDb _db;
    private readonly ILog _log;

    public EventRepository(ControlTimeDb db, ILog log)
    {
        _db = db;
        _log = log;
    }

    public async Task<Event?> Insert(Event? item)
    {
        var @event = await _db.Events.AddAsync(item);
        await _db.SaveChangesAsync();

        return @event.Entity;
    }

    public Task<List<Event>> GetFrom(Book book)
    {
        throw new NotImplementedException();
    }

    public async Task<Event?> Get(Guid id)
    {
        return await _db.Events.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Result<Event, GetError>> Update(Event request)
    {
        var entity = await _db.Events.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (entity == null) return new Result<Event, GetError>(GetError.NotFound, String.Empty);

        entity.End = request.End;
        entity.Start = request.Start;
        await _db.SaveChangesAsync();
        
        return new Result<Event, GetError>(entity);
    }

    public Task Remove(Event item)
    {
        throw new NotImplementedException();
    }
}