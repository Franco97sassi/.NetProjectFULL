using BibliotecaApp.Etapa8.Contracts.Events;
using Loans.Api.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Loans.Api.Controllers;

[ApiController]
[Route("api/loans")]
public sealed class LoansController(List<LoanItem> loans, HashSet<Guid> suspendedUsers, IPublishEndpoint publish) : ControllerBase
{
    [HttpGet] public IActionResult Get() => Ok(loans);

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LoanItem model)
    {
        if (suspendedUsers.Contains(model.UserId)) return BadRequest("User suspended");
        loans.Add(model);
        await publish.Publish(new LoanCreated(model.Id, model.BookId, model.UserId, model.DueDateUtc));
        return Ok(model);
    }

    [HttpPost("{id:guid}/return")]
    public async Task<IActionResult> Return(Guid id)
    {
        var loan = loans.FirstOrDefault(x => x.Id == id);
        if (loan is null) return NotFound();
        loan.Returned = true;
        await publish.Publish(new LoanReturned(loan.Id, loan.BookId, loan.UserId, DateTime.UtcNow));
        return Ok(loan);
    }
}
