using EduControl.Controllers.AppController.Model;
using EduControl.Repositories;
using Microsoft.AspNetCore.Mvc;
using Vostok.Logging.Abstractions;

namespace EduControl.Controllers.AppController;

[ApiController]
[Route("api/statistics")]
public class StatisticsController
{
    private readonly ManageTime _manageTime;
    private readonly ILog _log;
    private readonly IStatisticsService _statistics;
    public StatisticsController(ManageTime manageTime, ILog log, IStatisticsService statistics)
    {
        _manageTime = manageTime;
        _log = log;
        _statistics = statistics;
    }

    [HttpGet]
    public async Task<ApiResult<ResponseStatics>> Get([FromQuery] string bookName)
    {
        var bookResponse = await _manageTime.Book.Get(bookName);
        if (bookResponse.HasError() || bookResponse.Value == null)
        {
            return new ApiResult<ResponseStatics>(bookResponse.Error.ToString(), bookResponse.ErrorExplain, 403);
        }

        var book = bookResponse.Value;

        var events = await _manageTime.Event.GetFrom(book);

        _statistics.WithData(events);
    }
}