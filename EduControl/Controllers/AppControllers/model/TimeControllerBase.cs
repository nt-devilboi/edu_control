using EduControl.Controllers.Model;
using EduControl.DataBase.ModelBd;
using EduControl.Repositories;
using Microsoft.AspNetCore.Mvc;
using Vostok.Logging.Abstractions;

namespace EduControl.Controllers.AppController.model;

public abstract class TimeControllerBase 
{
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
            return ExceptionEntity.NotFound<T>();
        }

        if (!_accountScope.Account.IsMy(entity))
        {
            _log.Info($"{nameof(entity)} belongs to other account");
            return ExceptionEntity.BelongsToOtherUser<T>();
        }


        return entity;
    }

    protected async Task<ApiResult<List<T>>> Get<T>(IRepository<T> repository) where T: IUserLink
    {
        var entities = await repository.Get(_accountScope.Account);
        if (entities.Count == 0)
        {
            _log.Info($"user: {_accountScope.Account.UserName} don't have {entities.GetType().Name}");
            return ExceptionEntity.NotFound<List<T>>();
        }

        return new ApiResult<List<T>>(entities);
    }
    
    public async Task<IActionResult> Remove<T>(IRepository<T> repository, Guid id) where T : IUserLink
    {
        var entity = await repository.Get(id); //todo maybe make manangeRepo.set<T>(); 
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
            _log.Info($"{entity.GetType().Name} belongs to other account");
            return ExceptionEntity.BelongsToOtherUser<T>();
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