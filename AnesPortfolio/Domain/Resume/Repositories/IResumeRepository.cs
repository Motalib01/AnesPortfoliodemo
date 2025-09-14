using AnesPortfolio.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AnesPortfolio.Domain.Resume.Repositories;

public interface IResumeRepository
{
    Task<Resume?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Resume>> GetAllAsync(CancellationToken cancellationToken = default);
    void Add(Resume resume);
    void Update(Resume resume);
    void Delete(Resume resume);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

public class ResumeRepository : IResumeRepository
{
    private readonly PortfolioContext _context;

    public ResumeRepository(PortfolioContext context)
    {
        _context = context;
    }

    public async Task<Resume?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _context.Resumes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<List<Resume>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _context.Resumes.ToListAsync(cancellationToken);

    public void Add(Resume resume) => _context.Resumes.Add(resume);

    public void Update(Resume resume) => _context.Resumes.Update(resume);

    public void Delete(Resume resume) => _context.Resumes.Remove(resume);

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken);
}