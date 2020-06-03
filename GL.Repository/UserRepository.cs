using GL.Data;
using GL.Entities;

namespace GL.Repository
{
    public class UserRepository: Repository<ForumContext, Users>, IUserRepository
    {
        public UserRepository(ForumContext repoContext) : base(repoContext)
        {
        }
    }
}
