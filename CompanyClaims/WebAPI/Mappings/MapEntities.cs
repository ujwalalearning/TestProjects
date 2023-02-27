using Domain.DTOs;
using Domain.Entities;
using AutoMapper;

namespace CompanyClaims.Mappings
{
    public class MapEntities : Profile
    {
        public MapEntities()
        {
            CreateMap<Claim, ClaimDTO>();
            CreateMap<Company, CompanyDTO>();
            CreateMap<ClaimType, ClaimTypeDTO>();

            CreateMap<ClaimDTO, Claim>();
        }
    }
}
