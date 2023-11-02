using EduControl.DataBase.ModelBd;

namespace EduControl.Repositories;

public class ManageTime
{
    public IRepository<Book> Book { get; set; }
    public IEventRepository Event { get; set; }
    public IRepository<Status> Status { get; set; }

    public ManageTime(IRepository<Book> book, IEventRepository @event, IRepository<Status> status)
    {
        Book = book;
        Event = @event;
        Status = status;
    }
}