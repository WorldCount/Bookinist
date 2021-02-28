using System.Collections.Generic;
using Bookinist.DB.Entityes.Base;

namespace Bookinist.DB.Entityes
{
    public class Category : NamedEntity
    {
        public virtual ICollection<Book> Books { get; set; }

        public override string ToString() => $"Категория '{Name}'";
    }
}