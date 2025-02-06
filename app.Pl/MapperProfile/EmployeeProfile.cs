using app.DAL.model;
using app.Pl.ViewModels;
using AutoMapper;

namespace app.Pl.MapperProfile
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            //if model name is not coorect view model name 
            //.ForMember(d=> d.empName, o=>o.MapFrom(s=>s.Name));
        }
    }
}
