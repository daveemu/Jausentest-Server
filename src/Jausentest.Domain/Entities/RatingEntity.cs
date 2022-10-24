namespace Jausentest.Domain.Entities;

public class RatingEntity
{
    public long Id { get; set; }
    public string Comment { get; set; }
    public double Score { get; set; }
    public BeislEntity Beisl { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        return GetType() == obj.GetType() && this.Id == ((RatingEntity)obj).Id;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}