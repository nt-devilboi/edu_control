using EduControl.Controllers.Model;
using EduControl.DataBase.ModelBd;
using EduControl.Repositories;
using Microsoft.AspNetCore.Mvc;
using Vostok.Logging.Abstractions;

namespace EduControl.Controllers;

[ApiController]
[Route("register")]
public class RegisterController : ControllerBase
{
    private static readonly ApiResult<Account> UserNameIsBusy = new("register:user-name-is-busy", string.Empty, 403);
    private readonly ILog _log;
    private readonly IAccountRepository _accounts;

    public RegisterController(ILog log, IAccountRepository accounts)
    {
        _log = log;
        _accounts = accounts;
    }

    [HttpPost]
    public async Task<ApiResult<string>> Post([FromBody] RequestNewUser newUser)
    {
        var user = await _accounts.Get(newUser.UserName);
        if (user.Value != null) return "this username is Busy";

        var account = Account.From(newUser);
        await _accounts.Add(account);
        
        return "Аккаунт создан"; //todo: а что выглядит неплохо, аха)(()
    }
}