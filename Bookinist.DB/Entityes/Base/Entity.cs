using Bookinist.Interfaces;

namespace Bookinist.DB.Entityes.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
