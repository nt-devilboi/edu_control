using EduControl.Controllers.Model;
using EduControl.Repositories;
using Microsoft.AspNetCore.Mvc;
using Vostok.Logging.Abstractions;

namespace EduControl.Controllers.AppController;

[ApiController]
[Route("event")]
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
    
    
}