using Microsoft.EntityFrameworkCore;
using Program.Data;
using Program.Models;
using Program.Repositories.Abstract;

namespace Program.Repositories.Concrete;

public class UserRepository : IUserRepository
{



    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        
        await _context.SaveChangesAsync();
    
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}
