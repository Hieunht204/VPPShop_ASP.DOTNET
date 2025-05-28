using AutoMapper;
using VPPShop.Models;
using VPPShop.Entities;

namespace VPPShop.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<RegisterViewModel, Customer>();
                //.ForMember(c => c.Fullname, option => option.MapFrom(RegisterViewModel =>
                //    RegisterViewModel.Fullname))
                //.ReverseMap();
        }
    }
}
