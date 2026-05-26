using BibliotecaApp.Etapa8.Contracts.Events;
using Catalog.Api.Models;
using MassTransit;

namespace Catalog.Api.Consumers;
public sealed class LoanReturnedConsumer(List<BookItem> books) : IConsumer<LoanReturned>
{
    public Task Consume(ConsumeContext<LoanReturned> context)
    {
        var book = books.FirstOrDefault(b => b.Id == context.Message.BookId);
        if (book is not null) book.Stock++;
        return Task.CompletedTask;
    }
}
