using System.Collections.Generic;

namespace Jausentest.Domain.Entities
{
    public class TagEntity
    {
        public string Name { get; set; }
        public List<BeislEntity> Beisl { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return GetType() == obj.GetType() && this.Name == ((TagEntity)obj).Name;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}