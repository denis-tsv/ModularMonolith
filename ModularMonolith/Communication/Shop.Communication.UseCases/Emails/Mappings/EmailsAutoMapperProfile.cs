using AutoMapper;
using Shop.Communication.Entities;
using Shop.Communication.UseCases.Emails.Commands.ScheduleEmail;
using Shop.Communication.UseCases.Emails.Dto;

namespace Shop.Communication.UseCases.Emails.Mappings
{
    internal class EmailsAutoMapperProfile : Profile
    {
        public EmailsAutoMapperProfile()
        {
            CreateMap<Email, EmailDto>();
            CreateMap<ScheduleEmailCommand, Email>();
        }
    }
}
