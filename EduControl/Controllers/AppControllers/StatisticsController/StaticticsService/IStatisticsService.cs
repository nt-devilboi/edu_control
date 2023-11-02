using EduControl.Controllers.AppController.Model;
using EduControl.DataBase.ModelBd;

namespace EduControl.Controllers.AppController;

public interface IStatisticsService
{
    public EventStatistics WithData(List<Event> events);
}

