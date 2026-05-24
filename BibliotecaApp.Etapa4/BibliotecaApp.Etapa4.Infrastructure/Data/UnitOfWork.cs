using BibliotecaApp.Etapa4.Domain.Repositories;

namespace BibliotecaApp.Etapa4.Infrastructure.Data;

public class UnitOfWork(BibliotecaDbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => context.SaveChangesAsync(ct);
}
