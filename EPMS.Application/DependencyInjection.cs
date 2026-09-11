using EPMS.Application.Services.AuthService;
using EPMS.Application.Services.DepartmentQueryService.CommandService;
using EPMS.Application.Services.DepartmentQueryService.QueryService;
using EPMS.Application.Services.EmployeeService.CommandService;
using EPMS.Application.Services.EmployeeService.QueryService;
using EPMS.Application.Services.EvaluationsService.CommandService;
using EPMS.Application.Services.EvaluationsService.QueryService;
using EPMS.Application.Services.EvaluationTemplateService.CommandService;
using EPMS.Application.Services.EvaluationTemplateService.QueryService;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EPMS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(cfg => cfg.AddMaps(assembly));
            services.AddValidatorsFromAssembly(assembly);
            services.AddScoped<IDepartmentQueryService, DepartmentQueryService>();
            services.AddScoped<IDepartmentCommandService, DepartmentCommandService>();
            services.AddScoped<IEmployeeQueryService, EmployeeQueryService>();
            services.AddScoped<IEmployeeCommandService, EmployeeCommandService>();
            services.AddScoped<IEvaluationTemplateQueryService, EvaluationTemplateQueryService>();
            services.AddScoped<IEvaluationTemplateCommandService, EvaluationTemplateCommandService>();
            services.AddScoped<IEvaluationQueryService, EvaluationQueryService>();
            services.AddScoped<IEvaluationCommandService, EvaluationCommandService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
