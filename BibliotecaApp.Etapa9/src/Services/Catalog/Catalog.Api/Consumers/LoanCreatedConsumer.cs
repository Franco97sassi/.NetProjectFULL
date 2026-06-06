using BibliotecaApp.Etapa8.Contracts.Events;
using Catalog.Api.Models;
using MassTransit;

namespace Catalog.Api.Consumers;
public sealed class LoanCreatedConsumer(List<BookItem> books) : IConsumer<LoanCreated>
{
    public Task Consume(ConsumeContext<LoanCreated> context)
    {
        var book = books.FirstOrDefault(b => b.Id == context.Message.BookId);
        if (book is not null && book.Stock > 0) book.Stock--;
        return Task.CompletedTask;
    }
}
