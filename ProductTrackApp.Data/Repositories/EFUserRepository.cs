using Microsoft.EntityFrameworkCore;
using ProductTrackApp.Data.Contexts;
using ProductTrackApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackApp.Data.Repositories
{
    public class EFUserRepository : IUserRepository
    {
        private readonly ProductTrackAppDbContext _context;

        public EFUserRepository(ProductTrackAppDbContext context)
        {
            _context = context;
        }

        public async Task<IList<User>> GetAllUserAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }
    }
}
