using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace DataLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {        
        public CompanyRepository() 
        {
            using (var context = new AppDataContext())
            {
                var companies = new List<Company>
                {
                     new Company
                    {   
                        Id = 1, 
                        Name = "ABC Insurance Company",
                        Address1 = "123 Main Street",
                        Postcode = "ABC",
                        Country = "USA",
                        InsuranceEndDate = DateTime.ParseExact("25/12/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    },
                      new Company
                    {  
                        Id = 2,
                        Name = "XYZ Insurance Company",
                        Address1 = "XYZ Main Street",
                        Postcode = "XYZ",
                        Country = "Canada",
                        InsuranceEndDate = DateTime.ParseExact("25/01/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    }
                };

                //this is just for in memory data handling to avoid duplicate data. 
                
                if (!context.Companies.Any())
                {
                    context.AddRange(companies);
                    context.SaveChanges();
                }

            }
        }

        public List<Company> GetAll()
        {
            using (var context = new AppDataContext())
            {
                var list = context.Companies                    
                    .ToList();
                return list;
            }
        }

        public Company GetCompany(int Id)
        {
            using (var context = new AppDataContext())
            {
                return context.Companies.FirstOrDefault(c => c.Id == Id);
            }
            
        }
    }
}
