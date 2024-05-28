using Microsoft.AspNetCore.Mvc;

namespace AlWaddahClinic.Server.WebhookForEComm;

[ApiController]
[Route("/api/[controller]")]
public class ElController : ControllerBase
{
    [HttpPost("payment")]
    public async Task PaymentWebhook([FromBody]PaymentWebhookResponse response)
    {
        Console.WriteLine("Yooooo");
    }
}