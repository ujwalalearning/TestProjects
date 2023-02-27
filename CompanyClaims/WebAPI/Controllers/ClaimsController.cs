using AutoMapper;
using DataLayer.Repositories;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimsController : ControllerBase
    {
        private readonly IClaimRepository _claimRepository;
        private readonly IMapper _mapper;
        public ClaimsController(IClaimRepository claimRepository, IMapper mapper)
        {
            _claimRepository = claimRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetClaims()
        {
            return Ok(_mapper.Map<List<ClaimDTO>>(_claimRepository.GetAll()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ucr"></param>
        /// <returns></returns>
        [HttpGet("{ucr}")]
        public IActionResult GetClaimbyUCR(string ucr)
        {
            var claim = _claimRepository.GetClaimByRef(ucr);
            return Ok(_mapper.Map<ClaimDTO>(claim));
        }        


        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns>Claimsofacompany</returns>
        [HttpGet("{companyId:int}")]
        public IActionResult GetCompanyClaims(int companyId)
        {
            var claim = _claimRepository.GetCompanyClaims(companyId);
            return Ok(_mapper.Map<List<ClaimDTO>>(claim));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="claimDTO"></param>
        /// <returns>Updates the claim</returns>
        [HttpPut()]
        public IActionResult UpdateClaim([FromBody] ClaimDTO claimDTO)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var claim = _claimRepository.Update(_mapper.Map<Claim>(claimDTO));

            if (claim) { return Ok(claimDTO); }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
