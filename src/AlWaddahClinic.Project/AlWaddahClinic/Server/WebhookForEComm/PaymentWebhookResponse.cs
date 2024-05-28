namespace AlWaddahClinic.Server.WebhookForEComm
{
    public class PaymentWebhookResponse
    {
        public string? id { get; set; }
        public string? type { get; set; }
        public string? created_at { get; set; }
        public string? secret_token { get; set; }
        public string? account_name { get; set; }
        public bool? live { get; set; }
        public virtual Payment? data { get; set; }
        public string? details_of_response { get; set; }
    }
}
