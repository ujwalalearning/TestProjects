using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IClaimRepository 
    {
        List<Claim> GetAll();
        Claim GetClaimByRef(string ucf);
        List<Claim> GetCompanyClaims(int CompanyId);        

        bool Update(Claim claim);

        
    }
}
