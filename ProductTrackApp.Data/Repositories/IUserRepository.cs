using ProductTrackApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackApp.Data.Repositories
{
    public interface IUserRepository
    {
        Task<IList<User>> GetAllUserAsync();
        Task<IList<User>> GetUsersByManagerIdAsync(int managerId);
    }
}
