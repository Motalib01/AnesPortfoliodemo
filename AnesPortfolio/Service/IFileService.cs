namespace AnesPortfolio.Service;

public interface IFileService
{
    Task<string> UploadAsync(IFormFile file, string folder);
    Task DeleteAsync(string filePath);
}

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _env;

    public FileService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> UploadAsync(IFormFile file, string folder)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is empty", nameof(file));

        // Make sure WebRootPath is set
        if (string.IsNullOrEmpty(_env.WebRootPath))
            _env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        // Ensure folder exists under wwwroot
        var uploadPath = Path.Combine(_env.WebRootPath, folder);
        Directory.CreateDirectory(uploadPath); 

        // Generate unique file name
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadPath, fileName);

        // Save file to disk
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Return relative path 
        return $"/{folder}/{fileName}".Replace("\\", "/");
    }

    public Task DeleteAsync(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            return Task.CompletedTask;

        var fullPath = Path.Combine(_env.WebRootPath, filePath.TrimStart('/'));
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        return Task.CompletedTask;
    }
}