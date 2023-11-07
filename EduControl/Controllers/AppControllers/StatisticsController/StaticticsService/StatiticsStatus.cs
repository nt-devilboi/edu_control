using System.ComponentModel.DataAnnotations;
using EduControl.DataBase.ModelBd;

namespace EduControl.Controllers.AppControllers.StatisticsController.StaticticsService;

public class StatisticsStatusData
{
    public Status Status { get; set; }
    [DisplayFormat(DataFormatString = "{0:HH}")] public TimeSpan  TotalTime { get; set; }
}