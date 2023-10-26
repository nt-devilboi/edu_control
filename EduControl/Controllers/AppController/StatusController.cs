using EduControl.Controllers.AppController.model;
using EduControl.Controllers.Model;
using EduControl.DataBase.ModelBd;
using EduControl.Repositories;
using Microsoft.AspNetCore.Mvc;
using Vostok.Logging.Abstractions;

namespace EduControl.Controllers.AppController;

[ApiController]
[Route("status")]
public class StatusController: TimeControllerBase
{
    private static ApiResult<Status> StatusExisted = new ApiResult<Status>("Status with this name exsisted", "change name", 403);
    private readonly ManageTime _manageTime;
    private readonly ILog _log;
    private readonly AccountScope _accountScope;

    public StatusController(ManageTime manageTime, ILog log, AccountScope accountScope) : base(log, accountScope)
    {
        _manageTime = manageTime;
        _log = log;
        _accountScope = accountScope;
    }

    [HttpPost("create")]
    public async Task<ApiResult<Status>> Post([FromBody] RequestStatus requestStatus)
    {
        var statusResponse = await _manageTime.Status.Get(requestStatus.Name);
        if (statusResponse.Value != null)
        {
            _log.Info("status with this name exsisted");
            return StatusExisted;
        }
        
        var newStatus = Status.From(requestStatus, _accountScope.Account);
        var statusLoaded = await _manageTime.Status.Insert(newStatus);
        
        _log.Info($"status Added With id {statusLoaded.Id} by user {_accountScope.Account.UserName}");
        return statusLoaded;
    }

    [HttpGet("{id:guid}")]
    public async Task<ApiResult<Status>> Get(Guid id)
    {
        return await Get(_manageTime.Status, id);
    }
    
    [HttpPost("remove/{id:guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        return await Remove(_manageTime.Status, id);
    }
    
    [HttpPatch("Update")]
    public async Task<ApiResult<Status>> Patch([FromBody] Status statusChanged)
    {
        return await Patch(_manageTime.Status, statusChanged);
    }
}