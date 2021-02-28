using Bookinist.DB.Entityes.Base;

namespace Bookinist.DB.Entityes
{
    public class Buyer : Person
    {
        public override string ToString() => $"Покупатель '{Surname} {Name} {Patronymic}'";
    }
}