using System;
namespace AlWaddahClinic.Server.Models
{
	public class Payment : Base
	{
        public Guid Id { get; set; }
        public Guid ClinicId { get; set; }
        public Guid HealthRecordId { get; set; }
        public virtual HealthRecord HealthRecord { get; set; }
        public string? Description { get; set; }
        public decimal? Amount { get; set; }
        public string? Currency { get; set; }
        public DateTime? PaidOn { get; set; }
    }
}

