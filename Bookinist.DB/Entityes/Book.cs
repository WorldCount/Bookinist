using Bookinist.DB.Entityes.Base;

namespace Bookinist.DB.Entityes
{
    public class Book : NamedEntity
    {
        public virtual Category Category { get; set; }

        public override string ToString() => $"Книга '{Name}'";
    }
}
