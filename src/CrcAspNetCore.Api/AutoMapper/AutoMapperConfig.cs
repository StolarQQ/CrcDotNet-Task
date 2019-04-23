using AutoMapper;
using CrcAspNetCore.Api.Models;

namespace CrcAspNetCore.Api.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CreationBookDto, Book>()
                        .ForMember(x => x.Id, opt => opt.Ignore());
                })
                .CreateMapper();
    }
}
