using System.Linq.Expressions;
using AutoRiaClone.Database;
using Microsoft.EntityFrameworkCore;

namespace AutoRiaClone.Services;

public class DatabaseUtils(ApplicationDbContext context)
{
    public async Task<bool> IsRecordExists<T>(Expression<Func<T, bool>> action) where T: class, IEntity
    {
        var set = context.Set<T>();
        var result = await set.FirstOrDefaultAsync(action);
        return result is not null;
    }
}