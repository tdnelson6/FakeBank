using FakeBankAPI.BaseData;
using FakeBankAPI.Models;
using FakeBankAPI.Repo.RepoFunctionBase;

namespace FakeBankAPI.Repo
{
    public class AccountRepo : Repo<Account>, IAccountRepo //handles specific account functionality (update)
    {
        private readonly DBContext _data;
        public AccountRepo(DBContext data) : base(data)
        {
            _data = data; //initializes the database context
        }

        public async Task<Account> UpdateAsync(Account entity)
        {
            entity.UpdatedAt = DateTime.Now; //updates the updated at field
            _data.Accounts.Update(entity); //updates the entity
            await _data.SaveChangesAsync();//saves the changes
            return entity; //returns the updated entity
        }
    }
}
