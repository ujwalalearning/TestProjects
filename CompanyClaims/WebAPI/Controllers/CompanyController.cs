using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Domain.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>List of companies</returns>
        [HttpGet]
        public IActionResult GetCompanies()
        {
            return Ok(_mapper.Map <List<CompanyDTO>>( _companyRepository.GetAll()));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Company details for the specified id</returns>
        [HttpGet("{id}")]
        public IActionResult GetCompany(int Id)
        {
            var company = _companyRepository.GetCompany(Id);
            return Ok(_mapper.Map<CompanyDTO>(company));
        }
    }
}
