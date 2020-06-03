
using GL.Data;
using GL.Entities;

namespace GL.Repository
{
    public class UserStatusRepository : Repository<ForumContext, UsersStatus>, IUserStatusRepository
    {
        public UserStatusRepository(ForumContext repoContext) : base(repoContext)
        {
        }
    }
}
