using FormulaOne.DataService.Data;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormulaOne.DataService.Repositories;

public class DriverRepository : GenericRepository<Driver>, IDriverRepository
{
    public DriverRepository(AppDbContext context, ILogger logger) : base(context, logger)
    {
    }

    public override async Task<IEnumerable<Driver>> All()
    {
        try
        {
            return await _dbSet.Where(x => x.Status == 1)
                .AsNoTracking()
                .AsSingleQuery()
                .OrderBy(x => x.AddedDate)
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} All Function Error", typeof(DriverRepository));
            throw;
        }
    }

    public override async Task<bool> Delete(Guid id)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return false;

            result.Status = 0;
            result.UpdatedDate = DateTime.UtcNow;

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} Delete Function Error", typeof(DriverRepository));
            throw;
        }
    }

    public override async Task<bool> Update(Driver driver)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == driver.Id);
            if (result == null)
                return false;

            result.UpdatedDate = DateTime.UtcNow;
            result.DriverNumber = driver.DriverNumber;
            result.DateOfBirth = driver.DateOfBirth;
            result.FirstName = driver.FirstName;
            result.LastName = driver.LastName;

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} Update Function Error", typeof(DriverRepository));
            throw;
        }
    }
}