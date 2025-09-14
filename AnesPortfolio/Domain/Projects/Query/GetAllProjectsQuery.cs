using AnesPortfolio.Domain.Projects.Repositories;
using MediatR;

namespace AnesPortfolio.Domain.Projects.Query;

public class GetAllProjectsQuery : IRequest<List<Project>> { }

public class GetAllProjectsHandler : IRequestHandler<GetAllProjectsQuery, List<Project>>
{
    private readonly IProjectRepository _repo;

    public GetAllProjectsHandler(IProjectRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Project>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetAllAsync(cancellationToken);
    }
}