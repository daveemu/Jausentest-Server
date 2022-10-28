namespace Jausentest.Domain.Entities;

public class ImageEntity
{
    public long Id { get; set; }
    public string FileName { get; set; }
    public BeislEntity Beisl { get; set; }
}