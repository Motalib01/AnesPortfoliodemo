using AnesPortfolio.Domain.Projects.Repositories;
using MediatR;

namespace AnesPortfolio.Domain.Projects.Query;

public record GetProjectByIdQuery(Guid Id) : IRequest<Project?>;

public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, Project?>
{
    private readonly IProjectRepository _repo;

    public GetProjectByIdHandler(IProjectRepository repo)
    {
        _repo = repo;
    }

    public async Task<Project?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetByIdAsync(request.Id, cancellationToken);
    }
}