using AnesPortfolio.Domain.Projects.Repositories;
using AnesPortfolio.Service;
using MediatR;

namespace AnesPortfolio.Domain.Projects.Command;

public record DeleteProjectCommand(Guid Id) : IRequest<bool>;

public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, bool>
{
    private readonly IProjectRepository _repo;
    private readonly IFileService _fileService;

    public DeleteProjectHandler(IProjectRepository repo, IFileService fileService)
    {
        _repo = repo;
        _fileService = fileService;
    }

    public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repo.GetByIdAsync(request.Id, cancellationToken);
        if (project == null) return false;

        // delete files from disk
        foreach (var imagePath in project.Images)
        {
            await _fileService.DeleteAsync(imagePath);
        }

        _repo.Delete(project);
        await _repo.SaveChangesAsync(cancellationToken);

        return true;
    }
}