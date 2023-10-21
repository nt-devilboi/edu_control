using EduControl.Controllers.Model;
using EduControl.Repositories;
using Microsoft.AspNetCore.Mvc;
using Vostok.Logging.Abstractions;

namespace EduControl.Controllers;

[ApiController]
[Route("/Auth")]
public class AuthController: ControllerBase
{
    private readonly ILog _log;
    private readonly IAccountRepository _accounts;
    private readonly ITokenRepository _tokens;

    public AuthController(ILog log, IAccountRepository accounts, ITokenRepository tokens)
    {
        _log = log;
        _accounts = accounts;
        _tokens = tokens;
    }

    [HttpPost]
    public async Task<IActionResult> GetUser(RequestGetToken requestUser)
    {
        var response = await _accounts.Get(requestUser.UserName);
        if (response.HasError())
        {
            return new StatusCodeResult(403);
        }
        
        var account = response.Value;
        if (account == null)
        {
            _log.Info($"user: {requestUser.UserName} not found");
            return new StatusCodeResult(403);
        }

        var token = Token.From(GenerateCode.GenerateToken(), account);
        
        await _tokens.Add(token);
        HttpContext.Response.Cookies.Append("token", token.Value);
        return new StatusCodeResult(200);
    } 
}