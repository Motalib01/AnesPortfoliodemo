using AnesPortfolio.Domain.Contact.Repositories;
using MediatR;

namespace AnesPortfolio.Domain.Contact.Command;

public class UpsertContactCommand : IRequest
{
    public string Phone { get; set; } = "";
    public string Facebook { get; set; } = "";
    public string Instagram { get; set; } = "";
    public string LinkedIn { get; set; } = "";
    public string Email { get; set; } = "";
}

public class UpsertContactHandler : IRequestHandler<UpsertContactCommand>
{
    private readonly IContactRepository _repo;

    public UpsertContactHandler(IContactRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(UpsertContactCommand request, CancellationToken cancellationToken)
    {
        var contact = new ContactInfo
        {
            Phone = request.Phone,
            Facebook = request.Facebook,
            Instagram = request.Instagram,
            LinkedIn = request.LinkedIn,
            Email = request.Email
        };

        _repo.AddOrUpdate(contact);
        await _repo.SaveChangesAsync(cancellationToken);
    }
}