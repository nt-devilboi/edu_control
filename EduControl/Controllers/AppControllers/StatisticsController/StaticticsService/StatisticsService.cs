using EduControl.Controllers.AppControllers.StatisticsController.StaticticsService;
using EduControl.DataBase.ModelBd;
using EduControl.Repositories;

namespace EduControl.Controllers.AppController.Model;

public class StatisticsService : IStatisticsService
{
    private readonly ManageTime _manageTime;

    public StatisticsService(ManageTime manageTime)
    {
        _manageTime = manageTime;
    }

    public EventStatistics WithData(List<Event> events)
    {
        return new EventStatistics(events, _manageTime);
    }
}



public class EventStatistics
{
    private List<Event> Events { get; }
    private readonly ManageTime _manage;
    public EventStatistics(List<Event> events, ManageTime manage)
    {
        Events = events;
        _manage = manage;
    }

    public EventStatistics From(DateTime dateTime)
    {
        var result = new List<Event>();
        foreach (var @event in Events)
        {
            if (@event.Start >= dateTime)
                
        }
    }

    public EventStatistics To(DateTime dateTime)
    {
        throw new NotImplementedException();
    }

    public List<StatisticsStatusData> DivideByStatus()
    {
        //будет hashset, который будет проверять этот статус уже взят из бд или нет.
    }
    // потом уже Linq 
    public StatisticsStatusData TotalTime()
    {
        // вернуть с status "allStatuses"
    }
}