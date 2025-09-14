using AnesPortfolio.Domain.Resume.Repositories;
using AnesPortfolio.Service;
using MediatR;

namespace AnesPortfolio.Domain.Resume.Command;

public class CreateResumeCommand : IRequest<Guid>
{
    public IFormFile CV { get; set; } = null!;
}

public class CreateResumeHandler : IRequestHandler<CreateResumeCommand, Guid>
{
    private readonly IResumeRepository _repo;
    private readonly IFileService _fileService;

    public CreateResumeHandler(IResumeRepository repo, IFileService fileService)
    {
        _repo = repo;
        _fileService = fileService;
    }

    public async Task<Guid> Handle(CreateResumeCommand request, CancellationToken cancellationToken)
    {
        var cvPath = await _fileService.UploadAsync(request.CV, "resumes");

        var resume = new Resume
        {
            CV = cvPath
        };

        _repo.Add(resume);
        await _repo.SaveChangesAsync(cancellationToken);

        return resume.Id;
    }
}