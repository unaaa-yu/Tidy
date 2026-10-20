namespace Tidy.Backend.Models.FileItem;

public enum FileCategory
{
    Unknown,
    Documents,
    Images,
    Videos,
    Audio,
    Archives,
    Code,
    Executables,
    Databases,
    Configuration,
    SystemFiles,
    Temporary,
    Shortcuts,
    Others
}

public class FileItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public FileCategory Type { get; set; } = FileCategory.Unknown;

    public string Path { get; set; } = string.Empty;

    public string Hash { get; set; } = string.Empty; // Hash of the file content for duplicate detection

    public long Size { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }
}
