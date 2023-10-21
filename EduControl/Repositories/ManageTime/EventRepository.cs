using EduControl.DataBase.ModelBd;

namespace EduControl.Repositories;

public class EventRepository : IRepository<Event>
{
    public Task<Book> Insert(Event item)
    {
        throw new NotImplementedException();
    }

    public Task<Event> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Remove(Event item)
    {
        throw new NotImplementedException();
    }

    public Task Find(Guid id)
    {
        throw new NotImplementedException();
    }
}