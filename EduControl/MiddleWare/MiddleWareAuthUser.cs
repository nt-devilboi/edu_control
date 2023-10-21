using EduControl.Controllers.Model;
using EduControl.DataBase.ModelBd;
using EduControl.Repositories;
using Vostok.Logging.Abstractions;

namespace EduControl.MiddleWare;

public class MiddleWareAuthUser
{
    private static readonly ApiResult<Account>
        AccountNotFound = new("auth:Account-not-found", "try authorization", 403);

    private readonly RequestDelegate _next;
    private readonly ILog _log;
    private readonly IAccountRepository _accounts;
    private readonly ITokenRepository _tokens;

    public MiddleWareAuthUser(RequestDelegate next, ILog log, IAccountRepository accounts, ITokenRepository tokens)
    {
        _next = next;
        _log = log;
        _accounts = accounts;
        _tokens = tokens;
    }

    public async Task InvokeAsync(HttpContext context, AccountScope accountScope)
    {
        _log.Info("GetUserByTokenMiddleWare");
        if (!context.Request.Cookies.TryGetValue("token", out var token) || string.IsNullOrEmpty(token))
        {
            await context.Response.WriteAsJsonAsync(AccountNotFound);
            return;
        }
        _log.Info($"token here: {token}");
        var tokenResponse = await _tokens.Get(token);
        if (tokenResponse.HasError() || tokenResponse.Value == null)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsJsonAsync(tokenResponse);
            return;
        }
        
        var tokenData = tokenResponse.Value;
        
        var accountResponse = await _accounts.Get(tokenData);
        if (accountResponse.HasError() || accountResponse.Value == null)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsJsonAsync(accountResponse);
            return;
        }

        accountScope.Account = accountResponse.Value;
        await _next(context);
    }
}