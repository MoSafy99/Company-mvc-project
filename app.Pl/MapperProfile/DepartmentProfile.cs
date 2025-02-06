using app.DAL.model;
using app.Pl.ViewModels;
using AutoMapper;

namespace app.Pl.MapperProfile
{
    public class DepartmentProfile : Profile 
    {
        public DepartmentProfile() 
        {
            CreateMap<Department,DepartmentViewModel>().ReverseMap();
        }
    }
}
