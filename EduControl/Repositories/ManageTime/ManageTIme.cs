using EduControl.DataBase.ModelBd;

namespace EduControl.Repositories;

public class ManageTime : IManageTime
{
    public IRepository<Book> Book { get; set; }
    public IRepository<Event> Event { get; set; }
    public IRepository<Status> Status { get; set; }

    public ManageTime(IRepository<Book> book, IRepository<Event> @event, IRepository<Status> status)
    {
        Book = book;
        Event = @event;
        Status = status;
    }
}