using AnesPortfolio.Domain.Resume.Repositories;
using AnesPortfolio.Service;
using MediatR;

namespace AnesPortfolio.Domain.Resume.Command;

public class DeleteResumeCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}

public class DeleteResumeHandler : IRequestHandler<DeleteResumeCommand, Unit>
{
    private readonly IResumeRepository _repo;
    private readonly IFileService _fileService;

    public DeleteResumeHandler(IResumeRepository repo, IFileService fileService)
    {
        _repo = repo;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(DeleteResumeCommand request, CancellationToken cancellationToken)
    {
        var resume = await _repo.GetByIdAsync(request.Id, cancellationToken);
        if (resume == null) throw new KeyNotFoundException("Resume not found");

        if (!string.IsNullOrEmpty(resume.CV))
            await _fileService.DeleteAsync(resume.CV);

        _repo.Delete(resume);
        await _repo.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}