using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        public ClaimRepository()
        {
            using (var context = new AppDataContext())
            {
                var claims = new List<Claim>
                {
                    new Claim
                    {
                        UCR = "C-2021-0001",
                        CompanyId = 1,                       
                        ClaimDate = DateTime.ParseExact("25/01/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        LossDate = DateTime.ParseExact("20/01/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        AssuredName = "Jane Smith",
                        IncurredLoss = 5000,
                        Closed = true
                    },
                    new Claim
                    {
                        UCR = "C-2021-0002",
                        CompanyId = 1,                
                        ClaimDate = DateTime.ParseExact("25/12/2022", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        LossDate = DateTime.ParseExact("20/12/2022", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        AssuredName = "Robert",
                        IncurredLoss = 4000,
                        Closed = false
                    },
                    new Claim
                    {
                        UCR = "C-2021-0003",
                        CompanyId = 2,
                        ClaimDate = DateTime.ParseExact("25/12/2022", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        LossDate = DateTime.ParseExact("20/12/2022", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        AssuredName = "Robert2",
                        IncurredLoss = 4000,
                        Closed = false
                    }
                };

                if (!context.Claims.Any())
                {
                    context.AddRange(claims);
                    context.SaveChanges();
                }
            }
            
        }

        public List<Claim> GetAll()
        {
            using (var context = new AppDataContext())
            {
                var list = context.Claims
                    .ToList();
                return list;
            }
        }

        public Claim GetClaimByRef(string ucf)
        {
            using (var context = new AppDataContext())
            {
                return context.Claims.FirstOrDefault(c => c.UCR.Equals(ucf));
            }
        }        

        public List<Claim> GetCompanyClaims(int CompanyId)
        {
            using (var context = new AppDataContext())
            {
                return context.Claims.Where(c => c.CompanyId == CompanyId).ToList();
            }
        }       

        public bool Update(Claim claim)
        {
            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }
            using (var context = new AppDataContext())
            {
                var index = context.Claims.FirstOrDefault(p => p.UCR == claim.UCR);

                if (index == null)
                {
                    return false;
                }

                context.Claims.Remove(index);
                context.Claims.Add(claim);

                return true;
            }           
        }
    }
}
