using LaCazuelaChapinaAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace LaCazuelaChapinaAPI.Services
{
  public class DatabaseService
  {
    private readonly ApplicationDbContext _context;

    public DatabaseService(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<(bool Success, string Error)> CheckDatabaseConnectionAsync()
    {
      try
      {
        await _context.Database.ExecuteSqlRawAsync("SELECT 1");
        return (true, null);
      }
      catch (Exception ex)
      {
        return (false, ex.Message);
      }
    }
  }
}
