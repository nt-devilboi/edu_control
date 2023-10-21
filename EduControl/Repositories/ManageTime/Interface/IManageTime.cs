using EduControl.DataBase.ModelBd;

namespace EduControl.Repositories;

public interface IManageTime 
{
    public IRepository<Book> Book { get; set; }
    public  IRepository<Event> Event { get; set; }
    public IRepository<Status> Status { get; set; }
}