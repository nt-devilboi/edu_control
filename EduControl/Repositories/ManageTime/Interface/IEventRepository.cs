using EduControl.DataBase.ModelBd;

namespace EduControl.Repositories;

public interface IEventRepository
{
    public Task<Event?> Insert(Event item);
    public Task<List<Event>> GetFrom(Book book);
    public Task<Event?> Get(Guid id);
    public Task<Result<Event, GetError>> Update(Event request);

    public Task Remove(Event item);
}