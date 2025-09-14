using AnesPortfolio.Domain.Contact.Repositories;
using MediatR;

namespace AnesPortfolio.Domain.Contact.Query;

public class GetContactQuery : IRequest<ContactInfo?> { }

public class GetContactHandler : IRequestHandler<GetContactQuery, ContactInfo?>
{
    private readonly IContactRepository _repo;

    public GetContactHandler(IContactRepository repo)
    {
        _repo = repo;
    }

    public async Task<ContactInfo?> Handle(GetContactQuery request, CancellationToken cancellationToken)
    {
        return await _repo.GetAsync(cancellationToken);
    }
}