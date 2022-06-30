using Application.Models;
using Domain;

namespace Application.Abstractions
{
    public interface ILibraryRepository
    {
        bool SaveChanges();

        Book GetBookById(int Id);

        BookAvailability GetBookAvailability(int Id);

        List<Book> GetMostBorrowedBooks(int topRange);

        void CreateBook(Book book);
    }
}
