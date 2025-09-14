using AnesPortfolio.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AnesPortfolio.Domain.Contact.Repositories;

public interface IContactRepository
{
    Task<ContactInfo?> GetAsync(CancellationToken cancellationToken = default);
    void AddOrUpdate(ContactInfo contact);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

public class ContactRepository : IContactRepository
{
    private readonly PortfolioContext _context;

    public ContactRepository(PortfolioContext context)
    {
        _context = context;
    }

    public async Task<ContactInfo?> GetAsync(CancellationToken cancellationToken = default) =>
        await _context.ContactInfos.FirstOrDefaultAsync(cancellationToken);

    public void AddOrUpdate(ContactInfo contact)
    {
        var existing = _context.ContactInfos.FirstOrDefault();
        if (existing == null)
        {
            _context.ContactInfos.Add(contact);
        }
        else
        {
            existing.Phone = contact.Phone;
            existing.Facebook = contact.Facebook;
            existing.Instagram = contact.Instagram;
            existing.LinkedIn = contact.LinkedIn;
            existing.Email = contact.Email;
            _context.ContactInfos.Update(existing);
        }
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await _context.SaveChangesAsync(cancellationToken);
}