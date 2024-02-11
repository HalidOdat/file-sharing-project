namespace Domain
{
    public class FileModel : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
