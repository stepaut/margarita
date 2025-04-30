using System.Threading.Tasks;

namespace margarita.Data.Repositories.RecipeBook;

public interface IRepository
{
    Task Save();
}

internal abstract class RepositoryBase
{
    protected readonly BarDbContext _context;

    public RepositoryBase(BarDbContext context)
    {
        _context = context;
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
