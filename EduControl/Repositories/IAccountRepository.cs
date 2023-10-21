using EduControl.Controllers.Model;
using EduControl.DataBase.ModelBd;

namespace EduControl.Repositories;

public interface IAccountRepository
{
    public Task<Result<Account, GetError>> Get(Token token);

    public Task<Result<Account, GetError>> Get(string userName);

    public Task Add(Account account);
}