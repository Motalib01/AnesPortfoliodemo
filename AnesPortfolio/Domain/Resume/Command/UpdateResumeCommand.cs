using AnesPortfolio.Domain.Resume.Repositories;
using AnesPortfolio.Service;
using MediatR;

namespace AnesPortfolio.Domain.Resume.Command;

public class UpdateResumeCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public IFormFile CV { get; set; }
}

public class UpdateResumeHandler : IRequestHandler<UpdateResumeCommand, Unit>
{
    private readonly IResumeRepository _repo;
    private readonly IFileService _fileService;

    public UpdateResumeHandler(IResumeRepository repo, IFileService fileService)
    {
        _repo = repo;
        _fileService = fileService;
    }

    public async Task<Unit> Handle(UpdateResumeCommand request, CancellationToken cancellationToken)
    {
        var resume = await _repo.GetByIdAsync(request.Id, cancellationToken);
        if (resume == null) throw new KeyNotFoundException("Resume not found");

        // delete old CV
        if (!string.IsNullOrEmpty(resume.CV))
            await _fileService.DeleteAsync(resume.CV);

        // upload new CV
        resume.CV = await _fileService.UploadAsync(request.CV, "resumes");

        _repo.Update(resume);
        await _repo.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}