namespace AnesPortfolio.Domain.Projects;

public class Project
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<string> Images { get; set; } = new();
    
    //
}