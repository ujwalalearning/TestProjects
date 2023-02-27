using AutoMapper;
using CompanyClaims.Mappings;
using DataLayer.Repositories;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Globalization;
using WebAPI.Controllers;
using Xunit;

namespace UnitTestProject
{
    
    public class MyApiControllerTests
    {
        private Mock<ICompanyRepository> _mockCompanyRepo;
        private Mock<IClaimRepository> _mockClaimRepo;
        private Mock<IMapper> _mockMapperRepo;
        private CompanyController _companyController;
        private ClaimsController _claimController;
        List<Company> _companyList;
        List<Claim> _claimList;

        public MyApiControllerTests()
        {
            _companyList = new List<Company>
                {
                     new Company
                    {
                        Id= 1,
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


            _claimList = new List<Claim>
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
                    }
                };

            Setup();
        }

        public IMapper GetMapper()
        {
            var profile = new MapEntities();
            var config = new MapperConfiguration(x => x.AddProfile(profile));
            return new Mapper(config);
        }

        public void Setup()
        {
            var mapper = GetMapper();
            _mockCompanyRepo = new Mock<ICompanyRepository>();
            _mockClaimRepo= new Mock<IClaimRepository>();
            _companyController = new CompanyController(_mockCompanyRepo.Object, mapper);
            _claimController = new ClaimsController(_mockClaimRepo.Object, mapper);
        }
        

        [Fact]
        public void Get_ReturnsAllCompanies()
        {
           
            // Arrange            
            _mockCompanyRepo.Setup(repo => repo.GetAll()).Returns(_companyList);            

            // Act
            var result = _companyController.GetCompanies();
            var okResult = result as OkObjectResult;

            // Assert
            var companies = Assert.IsType<List<CompanyDTO>>(okResult.Value);
            Assert.Equal(2, companies.Count);
        }

       

        [Fact]
        public void Get_ReturnsCompanyById()
        {

            // Arrange           
            var mapper = GetMapper();            
            
            _mockCompanyRepo.Setup(repo => repo.GetCompany(It.IsAny<int>())).Returns((int i) => _companyList.FirstOrDefault(x => x.Id == i));         

            
            // Act
            var result = _companyController.GetCompany(1);
            var okResult = result as OkObjectResult;

            // Assert
            var company = Assert.IsType<CompanyDTO>(okResult.Value);
            Assert.Equal(company.IsInsuranceActive, true);
        }

        [Fact]
        public void Get_ReturnsClaimByRef()
        {

            // Arrange           
            var mapper = GetMapper();

            _mockClaimRepo.Setup(repo => repo.GetClaimByRef(It.IsAny<string>())).Returns((string s) => _claimList.FirstOrDefault(x => x.UCR.Equals(s)));

            // Act
            var result = _claimController.GetClaimbyUCR("C-2021-0001");
            var okResult = result as OkObjectResult;

            // Assert
            var claim = Assert.IsType<ClaimDTO>(okResult.Value);
            Assert.Equal(claim.AssuredName, "Jane Smith");
        }

    }

}