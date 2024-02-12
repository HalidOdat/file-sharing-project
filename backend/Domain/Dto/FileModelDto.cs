namespace Domain.Identity;

public class FileModelDto : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ContentType { get; set; }
    public long Size { get; set; } 
    public byte[] Content { get; set; }
}