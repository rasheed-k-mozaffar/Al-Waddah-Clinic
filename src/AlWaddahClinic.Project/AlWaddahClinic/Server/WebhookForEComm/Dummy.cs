using System.ComponentModel.DataAnnotations;

namespace AlWaddahClinic.Server.WebhookForEComm;

public class Dummy
{
    [Key]
    public Guid Id { get; set; }

    public string Message { get; set; }

    public DateTime EndpointHitOn { get; set; }
}