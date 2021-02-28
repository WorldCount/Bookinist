using System.Collections.Generic;
using System.Threading.Tasks;
using Bookinist.DB.Entityes;

namespace Bookinist.Services.Interfaces
{
    internal interface ISalesService
    {
        IEnumerable<Deal> Deals { get; }

        Task<Deal> MakeADealAsync(string bookName, Seller seller, Buyer buyer, decimal price);
    }
}
