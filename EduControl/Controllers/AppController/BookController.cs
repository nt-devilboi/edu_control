using EduControl.Controllers.AppController.model;
using EduControl.Controllers.Model;
using EduControl.DataBase.ModelBd;
using EduControl.Repositories;
using Microsoft.AspNetCore.Mvc;
using Vostok.Logging.Abstractions;

namespace EduControl.Controllers.AppController;

[ApiController]
[Route("book")]
public class BookController
{
    private readonly ApiResult<Book> _bookNotFound = new("app:book-not-found", string.Empty, 403);
    
    private readonly ManageTime _manageTime;
    private readonly ILog _log;
    private readonly AccountScope _accountScope;
    public BookController(ManageTime manageTime, ILog log, AccountScope accountScope)
    {
        _manageTime = manageTime;
        _log = log;
        _accountScope = accountScope;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Post([FromBody] RequestNewBook requestNewBook)
    {
        var book = Book.From(requestNewBook, _accountScope.Account);
        var bookLoaded = await _manageTime.Book.Insert(book);
        
        _log.Info($"book Added With id {bookLoaded.Id} by user {_accountScope.Account.UserName}");
        return new StatusCodeResult(200);
    }

    [HttpPost("remove/{id:guid}")]
    public async Task<IActionResult> Remove(Guid id)
    {
        var book = await _manageTime.Book.Get(id);
        if (book == null)
        {
            _log.Info($"Book With id {id} not Found");
            return new StatusCodeResult(404);
        }

        await _manageTime.Book.Remove(book);
        
        return new StatusCodeResult(200);
    }

    [HttpPost("Get/{id:guid}")]
    public async Task<ApiResult<Book>> Get(string id)
    {
        var book = await _manageTime.Book.Get(new Guid(id));
        if (book == null)
        {
            _log.Info($"Book With guid {id} not found");
            return _bookNotFound;
        }

        return book;
    }
    
}