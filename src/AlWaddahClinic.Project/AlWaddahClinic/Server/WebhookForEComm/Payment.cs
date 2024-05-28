namespace AlWaddahClinic.Server.WebhookForEComm
{
    public class Payment
    {
        public string id { get; set; }
        public string status { get; set; }
        public int amount { get; set; }
        public int fee { get; set; }
        public string currency { get; set; }
        public int refunded { get; set; }
        public string? refunded_at { get; set; }
        public int captured { get; set; }
        public string? captured_at { get; set; }
        public string? voided_at { get; set; }
        public string? description { get; set; }
        public string? invoice_id { get; set; }
        public string? ip { get; set; }
        public string? created_at { get; set; }
        public string? updated_at { get; set; }
        public Dictionary<string,string> metadata { get; set; }
        public Source source { get; set; }
    }
}
