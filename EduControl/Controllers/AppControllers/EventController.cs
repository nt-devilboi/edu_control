using EduControl.Controllers.AppController.model;
using EduControl.Controllers.Model;
using EduControl.DataBase.ModelBd;
using EduControl.Repositories;
using Microsoft.AspNetCore.Mvc;
using Vostok.Logging.Abstractions;

namespace EduControl.Controllers.AppController;

[ApiController]
[Route("api/event")]
public class EventController
{
    private readonly ManageTime _manageTime;
    private readonly ILog _log;
    private readonly AccountScope _accountScope;

    public EventController(ManageTime manageTime, ILog log, AccountScope accountScope)
    {
        _manageTime = manageTime;
        _log = log;
        _accountScope = accountScope;
    }

    [HttpPost("start")]
    public async Task<ApiResult<Event>> StartEvent([FromBody] RequestEvent newEvent)
    {
        var book = await _manageTime.Book.Get(newEvent.BookId);
        if (book == null)
        {
            return ExceptionEntity.NotFound<Book,Event>();
        }

        if (book.UserId != _accountScope.Account.Id)
        {
            return ExceptionEntity.BelongsToOtherUser<Book, Event>();
        }

        var status = await _manageTime.Status.Get(newEvent.StatusId);
        if (status == null)
        {
            return ExceptionEntity.NotFound<Status, Event>();
        }

        if (status.UserId != _accountScope.Account.Id)
        {
            return ExceptionEntity.BelongsToOtherUser<Status, Event>();
        }

        var @event = await _manageTime.Event.Insert(newEvent); //todo maybe don't
        return @event;
    }

    [HttpPatch("end")]
    public async Task<ApiResult<Event>> EventEnd([FromBody] RequestEndEvent requestEndEvent)
    {
        var @event = await _manageTime.Event.Get(requestEndEvent.EventId);
        if (@event == null)
        {
            return ExceptionEntity.NotFound<Event>();
        }
        @event.End = DateTime.UtcNow;
        var endEventResponse = await _manageTime.Event.Update(@event);
        if (endEventResponse.HasError() || endEventResponse.Value == null)
        {
            return new ApiResult<Event>(endEventResponse.Error.ToString(), endEventResponse.ErrorExplain,403);
        }

        return endEventResponse.Value;
    }
}