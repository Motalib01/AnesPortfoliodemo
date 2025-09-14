namespace AnesPortfolio.Domain.Resume;

public class Resume
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string CV { get; set; }
}