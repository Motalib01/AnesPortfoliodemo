using AnesPortfolio.Domain.Resume.Repositories;
using MediatR;

namespace AnesPortfolio.Domain.Resume.Query;

public class GetResumeByIdQuery : IRequest<Resume?>
{
    public Guid Id { get; set; }
}

public class GetResumeByIdHandler : IRequestHandler<GetResumeByIdQuery, Resume?>
{
    private readonly IResumeRepository _repo;

    public GetResumeByIdHandler(IResumeRepository repo)
    {
        _repo = repo;
    }

    public async Task<Resume?> Handle(GetResumeByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetByIdAsync(request.Id, cancellationToken);
    }
}