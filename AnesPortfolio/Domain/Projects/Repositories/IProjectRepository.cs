using AnesPortfolio.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AnesPortfolio.Domain.Projects.Repositories;

public interface IProjectRepository
{
    Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Project>> GetAllAsync(CancellationToken cancellationToken = default);
    void Add(Project project);
    void Update(Project project);
    void Delete(Project project);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

public class ProjectRepository : IProjectRepository
{
    private readonly PortfolioContext _context;

    public ProjectRepository(PortfolioContext context)
    {
        _context = context;
    }

    public async Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _context.Projects.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<List<Project>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _context.Projects.ToListAsync(cancellationToken);

    public void Add(Project project) => _context.Projects.Add(project);

    public void Update(Project project) => _context.Projects.Update(project);

    public void Delete(Project project) => _context.Projects.Remove(project);

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken);
}