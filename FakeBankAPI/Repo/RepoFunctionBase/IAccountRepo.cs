using FakeBankAPI.Models;

namespace FakeBankAPI.Repo.RepoFunctionBase
{
    public interface IAccountRepo : IRepo<Account>
    {
        Task<Account> UpdateAsync(Account entity);
    }
}
