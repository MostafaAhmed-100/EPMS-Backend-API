using AutoMapper;
using EPMS.Application.DTOs.DepartmentDTOs;
using EPMS.Domain.Entitys;
namespace EPMS.Application.Mappings
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentResponse>()
                .ForMember(dest => dest.EmployeesCount,
                    opt => opt.MapFrom(src => src.Employees != null ? src.Employees.Count : 0));

            CreateMap<CreateDepartmentRequest, Department>()
                .ConstructUsing(req => new Department(req.Name, req.Description));
        }
    }
}
