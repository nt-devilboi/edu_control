using EduControl.Controllers.AppController.model;
using EduControl.Controllers.Model;
using EduControl.DataBase.ModelBd;
using EduControl.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Vostok.Logging.Abstractions;

namespace EduControl.Controllers.AppController;

[ApiController]
[Route("api/book")]
public class BookController : TimeControllerBase
{
    private readonly ManageTime _manageTime;
    private readonly ILog _log;
    private readonly AccountScope _accountScope;

    public BookController(ManageTime manageTime, ILog log, AccountScope accountScope) : base(log, accountScope)
    {
        _manageTime = manageTime;
        _log = log;
        _accountScope = accountScope;
    }

    [HttpGet]
    public async Task<ApiResult<List<Book>>> GetAll()
    {
        return await Get(_manageTime.Book);
    }
    
    
    [HttpPost("create")]
    public async Task<ApiResult<Book>> Post([FromBody] RequestNewBook requestNewBook)
    {
        var statusResponse = await _manageTime.Book.Get(requestNewBook.Name);
        if (statusResponse.Value != null)
        {
            _log.Info("status with this name exsisted");
            return ExceptionEntity.BelongsToOtherUser<Book>();
        }

        var book = Book.From(requestNewBook, _accountScope.Account);
        var bookLoaded = await _manageTime.Book.Insert(book);

        _log.Info($"book Added With id {bookLoaded.Id} by user {_accountScope.Account.UserName}");
        return book;
    }

    [HttpDelete("remove/{id:guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        return await Remove(_manageTime.Book, id);
    }

    [HttpGet("{id:guid}")]
    public async Task<ApiResult<Book>> Get(Guid id)
    {
        return await Get(_manageTime.Book, id);
    }

    [HttpPatch("update")]
    public async Task<ApiResult<Book>> Patch([FromBody] Book bookChanged)
    {
        return await Patch(_manageTime.Book, bookChanged);
    }
}