using EduControl.Controllers.Model;
using EduControl.DataBase;
using EduControl.DataBase.ModelBd;
using Microsoft.EntityFrameworkCore;

namespace EduControl.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly ControlTimeDb _db;

    public AccountRepository(ControlTimeDb db)
    {
        _db = db;
    }

    public async Task<Result<Account, GetError>> Get(Token token)
    {
        try
        {
            var account = await _db.Accounts.FirstOrDefaultAsync(x => x.Id == token.UsedId);
            return account switch
            {
                null => new Result<Account, GetError>(GetError.NotFound, "book not found"),
                _ => new Result<Account, GetError>(account)
            };
        }
        catch (Exception e)
        {
            return new Result<Account, GetError>(GetError.Error, e.Message);
        }
    }

    public async Task<Result<Account, GetError>> Get(string userName)
    {
        var account = await _db.Accounts.FirstOrDefaultAsync(x => x.UserName == userName);
        return account switch
        {
            null => new Result<Account, GetError>(GetError.Error, "Not have account"),
            _ => new Result<Account, GetError>(account)
        };
    }

    public async Task Add(Account account)
    {
        await _db.Accounts.AddAsync(account);
        await _db.SaveChangesAsync();
    }
}