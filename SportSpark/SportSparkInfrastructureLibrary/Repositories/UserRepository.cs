using Microsoft.EntityFrameworkCore;
using SportSparkCoreLibrary.Entities;
using SportSparkCoreLibrary.Interfaces.Repositories;
using SportSparkInfrastructureLibrary.Database;
using SportSparkInfrastructureLibrary.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportSparkInfrastructureLibrary.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly SportSparkDBContext _context;

        public UserRepository(SportSparkDBContext context) : base(context)
        {
            _context = context;
        }

        public Task<User> GetByIdDetailedAsync(int id)
        {
            return _context.Users
                .Include(x => x.Friendships)
                .Include(x => x.Events)
                .Include(x => x.ProfileImage)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
