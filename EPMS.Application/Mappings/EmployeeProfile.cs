using AutoMapper;
using EPMS.Application.DTOs.EmployeeDTOs;
using EPMS.Domain.Entitys;
namespace EPMS.Application.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeResponse>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.DepartmentName,
                    opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty));

            CreateMap<CreateEmployeeRequest, Employee>()
                .ConstructUsing(req => new Employee(
                    req.FirstName,
                    req.LastName,
                    req.JobTitle,
                    req.HireDate,
                    req.DepartmentId));
        }
    }
}
