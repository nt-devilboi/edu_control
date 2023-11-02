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
    public async Task<ApiResult<string>> GetUser(RequestGetToken requestUser)
    {
        var response = await _accounts.Get(requestUser.UserName);
        if (response.HasError() || response.Value == null)
        {
            return new ApiResult<string>(response.Error.ToString(), response.ErrorExplain, 403);
        }
        
        var account = response.Value;
        var token = Token.From(GenerateCode.GenerateToken(), account);
        
        await _tokens.Add(token);
        HttpContext.Response.Cookies.Append("token", token.Value);
        return "token recive";
    } 
}