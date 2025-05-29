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
            CreateMap<AdminAddProductViewModel, Product>()
                .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SelectedSupplierId))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.SelectedCategoryId));

            CreateMap<Product, AdminAddProductViewModel>()
                .ForMember(dest => dest.SelectedSupplierId, opt => opt.MapFrom(src => src.SupplierId))
                .ForMember(dest => dest.SelectedCategoryId, opt => opt.MapFrom(src => src.CategoryId));

            CreateMap<AdminEditProductViewModel, Product>()
                .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SelectedSupplierId))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.SelectedCategoryId));

            CreateMap<Product, AdminEditProductViewModel>()
                .ForMember(dest => dest.SelectedSupplierId, opt => opt.MapFrom(src => src.SupplierId))
                .ForMember(dest => dest.SelectedCategoryId, opt => opt.MapFrom(src => src.CategoryId));
        }
    }
}
