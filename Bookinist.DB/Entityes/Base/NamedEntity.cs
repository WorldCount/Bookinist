using System.ComponentModel.DataAnnotations;

namespace Bookinist.DB.Entityes.Base
{
    public abstract class NamedEntity : Entity
    {
        [Required]
        public string Name { get; set; }
    }
}