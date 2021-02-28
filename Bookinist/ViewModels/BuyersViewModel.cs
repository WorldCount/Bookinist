using Bookinist.DB.Entityes;
using Bookinist.Interfaces;
using Bookinist.ViewModels.Base;

namespace Bookinist.ViewModels
{
    class BuyersViewModel : ViewModel
    {
        private readonly IRepository<Buyer> _buyersRepository;

        public BuyersViewModel(IRepository<Buyer> buyersRepository)
        {
            _buyersRepository = buyersRepository;
        }
    }
}
