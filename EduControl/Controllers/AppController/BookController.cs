using EduControl.Controllers.AppController.model;
using EduControl.Controllers.Model;
using EduControl.DataBase.ModelBd;
using EduControl.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vostok.Logging.Abstractions;

namespace EduControl.Controllers.AppController;

[ApiController]
[Route("book")]
public class BookController : TimeControllerBase
{
    private static readonly ApiResult<Book> BookNotFound = new("app:book-not-found", string.Empty, 403);
    private static readonly ApiResult<Book> BookExisted = new("app:book-is-existed", string.Empty, 403);
    private readonly ManageTime _manageTime;
    private readonly ILog _log;
    private readonly AccountScope _accountScope;

    public BookController(ManageTime manageTime, ILog log, AccountScope accountScope) : base(log, accountScope)
    {
        _manageTime = manageTime;
        _log = log;
        _accountScope = accountScope;
    }

    [HttpPost("create")]
    public async Task<ApiResult<Book>> Post([FromBody] RequestNewBook requestNewBook)
    {
        var statusResponse = await _manageTime.Status.Get(requestNewBook.Name);
        if (statusResponse.Value != null)
        {
            _log.Info("status with this name exsisted");
            return BookExisted;
        }

        var book = Book.From(requestNewBook, _accountScope.Account);
        var bookLoaded = await _manageTime.Book.Insert(book);

        _log.Info($"book Added With id {bookLoaded.Id} by user {_accountScope.Account.UserName}");
        return book;
    }

    [HttpPost("remove/{id:guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        return await Remove(_manageTime.Book, id);
    }

    [HttpPost("Get/{id:guid}")]
    public async Task<ApiResult<Book>> Get(Guid id)
    {
        return await Get(_manageTime.Book, id);
    }

    [HttpPatch("Update")]
    public async Task<ApiResult<Book>> Patch([FromBody] Book bookChanged)
    {
        return await Patch(_manageTime.Book, bookChanged);
    }
}