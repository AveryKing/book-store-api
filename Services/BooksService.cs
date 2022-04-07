using BookStoreApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookStoreApi.Services;

public class BooksService
{
    private readonly IMongoCollection<Book> _booksCollection;

    public BooksService(IOptions<BookStoreDatabaseSettings> dbSettings)
    {
        var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
        var database = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
        _booksCollection = database.GetCollection<Book>(dbSettings.Value.BooksCollectionName);
    }

    public async Task<List<Book>> GetBooksAsync() => await _booksCollection.Find(_ => true).ToListAsync();
    
   public async Task<Book> GetBookAsync(string id) => await _booksCollection.Find(book => book.Id == id).FirstOrDefaultAsync();

    public async Task<Book> CreateBookAsync(Book book)
    {
        await _booksCollection.InsertOneAsync(book);
        return book;
    }

    public async Task<Book> UpdateBookAsync(Book book)
    {
        await _booksCollection.ReplaceOneAsync(b => b.Id == book.Id, book);
        return book;
    }

    public async Task DeleteBookAsync(Book book)
    {
        await _booksCollection.DeleteOneAsync(b => b.Id == book.Id);
    }

}