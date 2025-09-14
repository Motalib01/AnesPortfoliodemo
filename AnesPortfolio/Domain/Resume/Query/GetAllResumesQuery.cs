using AnesPortfolio.Domain.Resume.Repositories;
using MediatR;

namespace AnesPortfolio.Domain.Resume.Query;

public class GetAllResumesQuery : IRequest<List<Resume>> { }

public class GetAllResumesHandler : IRequestHandler<GetAllResumesQuery, List<Resume>>
{
    private readonly IResumeRepository _repo;

    public GetAllResumesHandler(IResumeRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Resume>> Handle(GetAllResumesQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetAllAsync(cancellationToken);
    }
}