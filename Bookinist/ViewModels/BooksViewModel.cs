﻿using Bookinist.DB.Entityes;
using Bookinist.Interfaces;
using Bookinist.ViewModels.Base;

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