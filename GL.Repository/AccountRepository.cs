using GL.Data;
using GL.Entities;

namespace GL.Repository
{
    public class AccountRepository : Repository<MasterContext, Account>, IAccountRepository
    {
        public AccountRepository(MasterContext repoContext) : base(repoContext)
        {
        }
    }
}
