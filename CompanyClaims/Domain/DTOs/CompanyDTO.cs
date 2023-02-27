using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public bool Active { get; set; }
        public DateTime InsuranceEndDate { get; set; }
        public bool IsInsuranceActive
        {
            get
            {
                return InsuranceEndDate > DateTime.Now ? true : false;

            }

        }
    }
}
