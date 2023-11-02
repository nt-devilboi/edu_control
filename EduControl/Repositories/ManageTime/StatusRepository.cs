using EduControl.Controllers.AppController.model;
using EduControl.DataBase;
using EduControl.DataBase.ModelBd;
using Microsoft.EntityFrameworkCore;
using Vostok.Logging.Abstractions;

namespace EduControl.Repositories;

public class StatusRepository : RepositoryBase<Status>, IRepository<Status>
{
    private static readonly Result<Status, GetError> StatusNotFound =
        new(GetError.NotFound, "Status not found");
    private readonly ControlTimeDb _db;
    private readonly ILog _log;
    public StatusRepository(ControlTimeDb db, ILog log) : base(db, log)
    {
        _db = db;
        _log = log;
    }
    
    
    public async Task<List<Status>> Get(Account account)
    {
        return await _db.Statuses.Where(x => x.UserId == account.Id).ToListAsync();
    }

    public async Task<Result<Status, GetError>> Update(Status request)
    {
        var entity = await _db.Set<Status>().FirstOrDefaultAsync(x => x.Id == request.Id);
        if (entity == null) return StatusNotFound;

        entity.Desc = request.Desc;
        entity.Name = request.Name;
        await _db.SaveChangesAsync();

        return new Result<Status, GetError>(entity);
    }
}