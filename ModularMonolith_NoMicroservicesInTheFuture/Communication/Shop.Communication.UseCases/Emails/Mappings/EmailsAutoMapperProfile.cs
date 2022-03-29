using AutoMapper;
using Shop.Communication.Entities;
using Shop.Communication.UseCases.Emails.Dto;

namespace Shop.Communication.UseCases.Emails.Mappings
{
    public class EmailsAutoMapperProfile : Profile
    {
        public EmailsAutoMapperProfile()
        {
            CreateMap<Email, EmailDto>();
        }
    }
}
