using AutoMapper;
using EPMS.Application.DTOs.TemplateDTOs;
using EPMS.Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPMS.Application.Mappings
{
    public class EvaluationTemplateProfile : Profile
    {
        public EvaluationTemplateProfile()
        {
            CreateMap<Criteria, CriteriaResponse>();
            CreateMap<CreateCriteriaRequest, Criteria>()
                .ConstructUsing(req => new Criteria(req.Name, req.Description, req.Weight, req.SectionId));

            CreateMap<Section, SectionResponse>()
                .ForMember(dest => dest.Criteria, opt => opt.MapFrom(src => src.Criteria));
            CreateMap<CreateSectionRequest, Section>()
                .ConstructUsing(req => new Section(req.Name, req.Weight, req.TemplateId));

            CreateMap<EvaluationTemplate, EvaluationTemplateResponse>()
                .ForMember(dest => dest.Sections, opt => opt.MapFrom(src => src.Sections));
            CreateMap<CreateTemplateRequest, EvaluationTemplate>()
                .ConstructUsing(req => new EvaluationTemplate(req.Title, req.Description));
        }
    }
}
