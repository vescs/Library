using FluentAssertions;
using Library.Core.Models;
using Library.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Library.Tests.Repositories.InMemory
{
    public class BookRepositoryTests
    {
        protected List<Book> bookList = new List<Book>()
        {
            new Book(Guid.NewGuid(), "TestTitle1", "TestDescription", "TestAuthor", 2000, "TestHouse", 10, DateTime.UtcNow),
            new Book(Guid.NewGuid(), "TestTitle2", "TestDescription", "TestAuthor", 2000, "TestHouse", 10, DateTime.UtcNow),
            new Book(Guid.NewGuid(), "TestTitle3", "TestDescription", "TestAuthor", 2000, "TestHouse", 10, DateTime.UtcNow),
            new Book(Guid.NewGuid(), "TestTitle4", "TestDescription", "TestAuthor", 2000, "TestHouse", 10, DateTime.UtcNow),
            new Book(Guid.NewGuid(), "TestTitle5", "TestDescription", "TestAuthor", 2000, "TestHouse", 10, DateTime.UtcNow),
        };

        [Fact]
        public async Task Add_async_should_properly_add_book_so_it_can_be_fetched()
        {
            var bookRepository = new InMemoryBookRepository();
            var book1 = bookList.FirstOrDefault();

            await bookRepository.AddAsync(book1);
            var fetchedBook = await bookRepository.GetAsync(book1.Id);
            await bookRepository.DeleteAsync(book1.Id);

            fetchedBook.Should().NotBeNull();
            fetchedBook.Should().BeEquivalentTo(book1);
        }

        [Fact]
        public async Task Browse_async_should_return_collection_of_books()
        {
            var bookRepository = new InMemoryBookRepository();

            foreach (var b in bookList)
            {
                await bookRepository.AddAsync(b);
            }
            var fetchedBooks = await bookRepository.BrowseAsync();
            
            fetchedBooks.Should().NotBeEmpty();
            fetchedBooks.Should().HaveCount(bookList.Count);
            fetchedBooks.Should().BeEquivalentTo(bookList);
        }

        [Fact]
        public async Task Delete_async_should_properly_delete_book_so_it_cant_get_fetched()
        {
            var bookRepository = new InMemoryBookRepository();
            var book1 = bookList.FirstOrDefault();

            await bookRepository.DeleteAsync(book1.Id);
            var fetchedBook = await bookRepository.GetAsync(book1.Id);

            fetchedBook.Should().BeNull();
            fetchedBook.Should().NotBeEquivalentTo(book1);
        }
    }
}
