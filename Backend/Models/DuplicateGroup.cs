namespace Tidy.Backend.Models;

public class DuplicateGroup
{
    public string Hash { get; set; } = string.Empty;
    public List<FileItem>  Files { get; set; } = new List<FileItem>();
}
