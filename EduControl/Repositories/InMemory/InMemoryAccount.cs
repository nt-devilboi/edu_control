using EduControl.Controllers.Model;
using EduControl.DataBase.ModelBd;

namespace EduControl.Repositories.InMemory;

public class InMemoryAccount : IAccountRepository
{
    private Dictionary<Guid, Account> _accounts = new();

    public async Task<Result<Account, GetError>> Get(Token token)
    {
        var account = _accounts.FirstOrDefault(x => x.Value.Id == token.UsedId).Value;
        return new Result<Account, GetError>(account);
    }

    public Task<Result<Account, GetError>> Get(string userName)
    {
        throw new NotImplementedException();
    }

    public async Task Add(Account account)
    {
        _accounts.Add(account.Id, account);
    }
}