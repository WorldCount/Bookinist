using Bookinist.DB.Entityes.Base;

namespace Bookinist.DB.Entityes
{
    public class Seller : Person
    {
        public override string ToString() => $"Продавец '{Surname} {Name} {Patronymic}'";
    }
}