using Bookinist.DB.Entityes;
using Bookinist.Interfaces;
using MathCore.WPF.ViewModels;

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
