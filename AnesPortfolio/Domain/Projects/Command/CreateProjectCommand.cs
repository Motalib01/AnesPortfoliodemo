using AnesPortfolio.Domain.Projects.Repositories;
using AnesPortfolio.Service;
using MediatR;

namespace AnesPortfolio.Domain.Projects.Command;

public class CreateProjectCommand : IRequest<Guid>
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<IFormFile> Images { get; set; } = new();
}

public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, Guid>
{
    private readonly IProjectRepository _repo;
    private readonly IFileService _fileService;

    public CreateProjectHandler(IProjectRepository repo, IFileService fileService)
    {
        _repo = repo;
        _fileService = fileService;
    }

    public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var imagePaths = new List<string>();
        foreach (var image in request.Images)
        {
            var path = await _fileService.UploadAsync(image, "projects");
            imagePaths.Add(path);
        }

        var project = new Project
        {
            Title = request.Title,
            Description = request.Description,
            Images = imagePaths
        };

        _repo.Add(project);
        await _repo.SaveChangesAsync(cancellationToken);

        return project.Id;
    }
}