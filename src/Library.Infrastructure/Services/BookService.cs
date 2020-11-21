using AutoMapper;
using Library.Core.Models;
using Library.Core.Repositories;
using Library.Infrastructure.DTO;
using Library.Infrastructure.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BookDTO>> BrowseAsync(string title = "")
        {
            var books = await _bookRepository.BrowseAsync(title);
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task<IEnumerable<BookDTO>> BrowseAuthorsAsync(string author = "")
        {
            var books = await _bookRepository.BrowseAuthorsAsync(author);
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task<IEnumerable<BookDTO>> BrowseHousesAsync(string house = "")
        {
            var books = await _bookRepository.BrowseHousesAsync(house);
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task CreateAsync(Guid id, string title, string description, string author, int pages, string publishingHouse, DateTime premiereDate)
        {
            var book = await _bookRepository.GetAsync(id);
            if (book != null)
            {
                throw new Exception($"Book with id {id} already exists.");
            }
            book = new Book(id, title, description, author, pages, publishingHouse, premiereDate);
            await _bookRepository.AddAsync(book);
        }

        public async Task DeleteAsync(Guid id)
        {
            var book = await _bookRepository.GetAsync(id);
            if(book == null)
            {
                throw new Exception($"Book with id {id} does not exist.");
            }
            await _bookRepository.DeleteAsync(id);
        }

        public async Task<BookDTO> GetAsync(Guid id)
        {
            var book = await _bookRepository.GetAsync(id);
            return _mapper.Map<BookDTO>(book);
        }

        public async Task<BookDTO> GetAsync(string title)
        {
            var book = await _bookRepository.GetAsync(title);
            return _mapper.Map<BookDTO>(book);
        }

        public async Task UpdateAsync(Guid id, string title, string description)
        {
            var book = await _bookRepository.GetAsync(id);
            if (!string.IsNullOrWhiteSpace(title))
            {
                book.SetTitle(title);
            }
            if(!string.IsNullOrWhiteSpace(description))
            {
                book.SetDescription(description);
            }
            await _bookRepository.UpdateAsync(book);
        }
    }
}
