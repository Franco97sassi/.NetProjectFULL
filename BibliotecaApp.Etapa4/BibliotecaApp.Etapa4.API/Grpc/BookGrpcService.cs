using BibliotecaApp.Etapa4.Application.Books;
using Grpc.Core;
using MediatR;

namespace BibliotecaApp.Etapa4.API.Grpc;

public class BookGrpcService(IMediator mediator) : BookGrpc.BookGrpcBase
{
    public override async Task<GetBooksResponse> GetBooks(
        GetBooksRequest request,
        ServerCallContext context)
    {
        var books = await mediator.Send(new GetAllBooksQuery(), context.CancellationToken);

        var response = new GetBooksResponse();

        foreach (var book in books)
        {
            response.Books.Add(new BookItem
            {
                Id = book.Id.ToString(),
                Title = book.Titulo,
                Author = book.Autor,
                Stock = book.Stock
            });
        }

        return response;
    }
}