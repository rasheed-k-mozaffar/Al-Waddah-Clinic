using System.Text.Json;
using AlWaddahClinic.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace AlWaddahClinic.Server.WebhookForEComm;

[ApiController]
[Route("/api/[controller]")]
public class ElController : ControllerBase
{
    private readonly ClinicDbContext _db;

    public ElController(ClinicDbContext db)
    {
        _db = db;
    }

    [HttpPost("payment")]
    public async Task PaymentWebhook([FromBody]PaymentWebhookResponse response)
    {
        var jsonString = JsonSerializer.Serialize(response, new JsonSerializerOptions()
        {
            WriteIndented = true
        });

        await _db.Dummies.AddAsync
        (
            new Dummy()
            {
                Id = Guid.NewGuid(),
                Message = jsonString,
                EndpointHitOn = DateTime.Now
            }
        );

        await _db.SaveChangesAsync();
    }

    [HttpGet("get-payments")]
    public async Task<IActionResult> GetPayments()
    {
        var payments = await _db.Dummies.ToListAsync();
        return Ok(payments);
    }
}