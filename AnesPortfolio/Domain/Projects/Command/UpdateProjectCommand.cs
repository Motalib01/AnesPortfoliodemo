using AnesPortfolio.Domain.Projects.Repositories;
using AnesPortfolio.Service;
using MediatR;

namespace AnesPortfolio.Domain.Projects.Command;

public class UpdateProjectCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<IFormFile>? NewImages { get; set; } // optional new images
}

public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, bool>
{
    private readonly IProjectRepository _repo;
    private readonly IFileService _fileService;

    public UpdateProjectHandler(IProjectRepository repo, IFileService fileService)
    {
        _repo = repo;
        _fileService = fileService;
    }

    public async Task<bool> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _repo.GetByIdAsync(request.Id, cancellationToken);
        if (project == null) return false;

        project.Title = request.Title;
        project.Description = request.Description;

        if (request.NewImages is { Count: > 0 })
        {
            var imagePaths = new List<string>();
            foreach (var image in request.NewImages)
            {
                var path = await _fileService.UploadAsync(image, "projects");
                imagePaths.Add(path);
            }

            project.Images.AddRange(imagePaths); // keep old + add new
        }

        _repo.Update(project);
        await _repo.SaveChangesAsync(cancellationToken);

        return true;
    }
}