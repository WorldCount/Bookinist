using Bookinist.DB.Entityes;
using Bookinist.Interfaces;
using MathCore.WPF.ViewModels;

namespace Bookinist.ViewModels
{
    class BooksViewModel : ViewModel
    {
        private readonly IRepository<Book> _booksRepository;

        public BooksViewModel(IRepository<Book> booksRepository)
        {
            _booksRepository = booksRepository;
        }
    }
}
