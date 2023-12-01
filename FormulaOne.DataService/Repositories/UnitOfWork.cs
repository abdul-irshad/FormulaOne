using FormulaOne.DataService.Data;
using FormulaOne.DataService.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace FormulaOne.DataService.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;


    public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        var logger = loggerFactory.CreateLogger("Logs");

        Drivers = new DriverRepository(context, logger);
        Achievements = new AchievementRepository(context, logger);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IDriverRepository Drivers { get; }

    public IAchievementRepository Achievements { get; }

    public async Task<bool> CompleteAsync()
    {
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }
}