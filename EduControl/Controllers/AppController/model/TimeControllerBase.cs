using EduControl.Controllers.Model;
using EduControl.DataBase.ModelBd;
using EduControl.Repositories;
using Microsoft.AspNetCore.Mvc;
using Vostok.Logging.Abstractions;

namespace EduControl.Controllers.AppController.model;

public abstract class TimeControllerBase
{
    private static ApiResult<T> EntityNotFound<T>(string e) =>
        new($"app:{e}-not-found", string.Empty, 403);

    private static ApiResult<T> EntityNotYour<T>(string e) =>
        new($"app:{e}-not-your", string.Empty, 403);

    private static ApiResult<T> EntityExisted<T>(string e) => new($"app:{e}-name-is-busy", String.Empty, 403);
    private readonly ILog _log;
    private readonly AccountScope _accountScope;

    protected TimeControllerBase(ILog log, AccountScope accountScope)
    {
        _log = log;
        _accountScope = accountScope;
    }
    
    protected async Task<ApiResult<T>> Get<T>(IRepository<T> repository, Guid id) where T : IUserLink
    {
        var entity = await repository.Get(id);
        if (entity == null)
        {
            _log.Info($"{nameof(entity)} With guid {id} not found");
            return EntityNotFound<T>(nameof(entity));
        }

        if (!_accountScope.Account.IsMy(entity))
        {
            _log.Info($"{nameof(entity)} belongs to other account");
            return EntityNotYour<T>(nameof(entity));
        }


        return entity;
    }

    [HttpPost("remove/{id:guid}")]
    public async Task<IActionResult> Remove<T>(IRepository<T> repository, Guid id) where T : IUserLink
    {
        var entity = await repository.Get(id);
        if (entity == null)
        {
            _log.Info($"Book With id {id} not Found");
            return new StatusCodeResult(404);
        }

        if (!_accountScope.Account.IsMy(entity))
        {
            _log.Warn($"user {_accountScope.Account.UserName} try remove not own book");
            return new StatusCodeResult(403);
        }

        await repository.Remove(entity);
        return new StatusCodeResult(200);
    }

    public async Task<ApiResult<T>> Patch<T>(IRepository<T> repository, T entity) where T : IUserLink
    {
        if (!_accountScope.Account.IsMy(entity))
        {
            _log.Info($"{nameof(entity)} belongs to other account");
            return EntityNotYour<T>(nameof(entity));
        }

        var bookResponse = await repository.Update(entity);
        if (bookResponse.HasError() || bookResponse.Value == null)
        {
            _log.Warn(
                $"Patch not completed with {nameof(bookResponse.Value)} {bookResponse.Value?.ToString() ?? bookResponse.Error.ToString()}");
            return new ApiResult<T>(bookResponse.Error.ToString(), bookResponse.ErrorExplain!, 403); // HERE  "!"
        }

        return bookResponse.Value;
    }
}