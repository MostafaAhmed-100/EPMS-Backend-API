using AutoMapper;
using EPMS.Application.DTOs.EvaluationDTOs;
using EPMS.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPMS.Application.Mappings
{
    public class EvaluationProfile : Profile
    {
        public EvaluationProfile()
        {
            CreateMap<EvaluationResponse, EvaluationCriterionResultResponse>()
                .ForMember(dest => dest.CriteriaName,
                    opt => opt.MapFrom(src => src.Criteria != null ? src.Criteria.Name : string.Empty));

            CreateMap<Evaluation, EvaluationSummaryResponse>()
                .ForMember(dest => dest.EmployeeName,
                    opt => opt.MapFrom(src => src.Employee != null ? $"{src.Employee.FirstName} {src.Employee.LastName}" : string.Empty))
                .ForMember(dest => dest.EvaluatorName,
                    opt => opt.MapFrom(src => src.Evaluator != null ? $"{src.Evaluator.FirstName} {src.Evaluator.LastName}" : string.Empty))
                .ForMember(dest => dest.TemplateTitle,
                    opt => opt.MapFrom(src => src.Template != null ? src.Template.Title : string.Empty))
                .ForMember(dest => dest.StartDate,
                    opt => opt.MapFrom(src => src.Period.StartDate))
                .ForMember(dest => dest.EndDate,
                    opt => opt.MapFrom(src => src.Period.EndDate))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<Evaluation, DetailedEvaluationResponse>()
                .ForMember(dest => dest.EmployeeName,
                    opt => opt.MapFrom(src => src.Employee != null ? $"{src.Employee.FirstName} {src.Employee.LastName}" : string.Empty))
                .ForMember(dest => dest.EvaluatorName,
                    opt => opt.MapFrom(src => src.Evaluator != null ? $"{src.Evaluator.FirstName} {src.Evaluator.LastName}" : string.Empty))
                .ForMember(dest => dest.TemplateTitle,
                    opt => opt.MapFrom(src => src.Template != null ? src.Template.Title : string.Empty))
                .ForMember(dest => dest.StartDate,
                    opt => opt.MapFrom(src => src.Period.StartDate))
                .ForMember(dest => dest.EndDate,
                    opt => opt.MapFrom(src => src.Period.EndDate))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.OverallScore,
                    opt => opt.Ignore())
                .ForMember(dest => dest.CriteriaResults,
                    opt => opt.MapFrom(src => src.Responses));
        }
    }
}
