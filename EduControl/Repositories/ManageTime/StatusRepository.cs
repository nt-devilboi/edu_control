using EduControl.DataBase.ModelBd;

namespace EduControl.Repositories;

public class StatusRepository : IRepository<Status>
{
    public Task<Status> Insert(Status item)
    {
        throw new NotImplementedException();
    }

    public Task<Status> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Status?, GetError>> Get(string name)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Status, GetError>> Update(Status request)
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