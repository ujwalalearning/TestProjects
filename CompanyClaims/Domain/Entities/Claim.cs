using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Claim
    {
        [Key]
        public string UCR { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }
        public int CompanyId { get; set; }
        public DateTime ClaimDate { get; set; }
        public DateTime LossDate { get; set; }
        public string AssuredName { get; set; }
        public decimal IncurredLoss { get; set; }
        public bool Closed { get; set; }
    }
}
