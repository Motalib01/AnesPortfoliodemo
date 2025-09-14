namespace AnesPortfolio.Domain.Contact;

public class ContactInfo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Phone { get; set; } = "";
    public string Facebook { get; set; } = "";
    public string Instagram { get; set; } = "";
    public string LinkedIn { get; set; } = "";
    public string Email { get; set; } = "";
}