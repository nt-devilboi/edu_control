using EduControl.DataBase.ModelBd;

namespace EduControl.Repositories;

public class StatusRepository : IRepository<Status>
{
    public Task<Book> Insert(Status item)
    {
        throw new NotImplementedException();
    }

    public Task<Status> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task Remove(Status item)
    {
        throw new NotImplementedException();
    }

    public Task Find(Guid id)
    {
        throw new NotImplementedException();
    }
}