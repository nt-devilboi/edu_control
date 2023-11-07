using EduControl.Controllers.AppControllers.StatisticsController.StaticticsService;
using EduControl.DataBase.ModelBd;

namespace EduControl.Controllers.AppController.Model;

public class StatisticsService : IStatisticsService
{
    public EventStatistics WithData(List<Event> events)
    {
        return new EventStatistics(events);
    }
}

public class EventStatistics
{
    private List<Event> Events { get; }

    public EventStatistics(List<Event> events)
    {
        Events = events;
    }

    public EventStatistics From(DateTime dateTime)
    {
        var result = new List<Event>();
        foreach (var @event in Events)
        {
            if (@event.Start > dateTime) continue;

            @event.Start = dateTime;
            result.Add(@event);
        }

        
        return new EventStatistics(result);
    }

    public EventStatistics To(DateTime dateTime)
    {
        var result =  new List<Event>();
        foreach (var @event in Events)
        {
            if (@event.End < dateTime) continue;

            @event.End = dateTime;
            result.Add(@event);
        }

        return new EventStatistics(result);
    }

    public List<StatisticsStatusData> DivideByStatus(List<Status> statuses)
    {
        throw new NotImplementedException();
        //будет hashset, который будет проверять этот статус уже взят из бд или нет.
    }

    // потом уже Linq 
    public List<StatisticsStatusData> TotalTime()
    {
        var result = new StatisticsStatusData
        {
            Status = new Status
            {
                Name = "total time"
            }
        };

        foreach (var @event in Events)
        {
            result.TotalTime += @event.GetTime();
        }
        
        return new List<StatisticsStatusData> { result };
    }
}