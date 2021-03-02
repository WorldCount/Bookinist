using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Bookinist.DB.Entityes;
using Bookinist.Infrastructure.DebugServices;
using Bookinist.Interfaces;
using MathCore.WPF.ViewModels;


namespace Bookinist.ViewModels
{
    class BooksViewModel : ViewModel
    {
        private readonly IRepository<Book> _booksRepository;
        private CollectionViewSource _booksViewSource;

        public IEnumerable<Book> Books => _booksRepository.Items;

        public ICollectionView BooksView => _booksViewSource.View;

        #region BooksFilter : string - Искомое слово

        /// <summary>Искомое слово</summary>
        private string _booksFilter;

        /// <summary>Искомое слово</summary>
        public string BooksFilter
        {
            get => _booksFilter;
            set
            {
                if(Set(ref _booksFilter, value))
                    _booksViewSource.View.Refresh();
            }
        }

        #endregion

        public BooksViewModel() : this(new DedugBooksRepository())
        {
            if (!App.IsDisignTime)
                throw new InvalidOperationException("Данный конструктор не предназначен для использования вне дизайнера VisualStudio");
        }

        public BooksViewModel(IRepository<Book> booksRepository)
        {
            _booksRepository = booksRepository;

            _booksViewSource = new CollectionViewSource
            {
                Source = _booksRepository.Items.ToArray(),
                SortDescriptions =
                {
                    new SortDescription(nameof(Book.Name), ListSortDirection.Ascending)
                }
            };

            _booksViewSource.Filter += OnBooksFilter;
        }

        private void OnBooksFilter(object sender, FilterEventArgs e)
        {
            if(!(e.Item is Book book) || string.IsNullOrEmpty(_booksFilter)) return;

            if (!book.Name.Contains(_booksFilter, StringComparison.OrdinalIgnoreCase))
                e.Accepted = false;
        }
    }
}
