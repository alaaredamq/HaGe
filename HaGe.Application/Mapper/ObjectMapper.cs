using AutoMapper;
using HaGe.Application.Models;
using HaGe.Core.Entities;
using Profile = AutoMapper.Profile;

namespace HaGe.Application.Mapper; 

public static class ObjectMapper
{
    private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
    {
        var config = new MapperConfiguration(cfg =>
        {
            // This line ensures that internal properties are also mapped over.
            cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
            cfg.AddProfile<HaGeRunDtoMapper>();
        });
        var mapper = config.CreateMapper();
        return mapper;
    });
    public static IMapper Mapper => Lazy.Value;
}

public class HaGeRunDtoMapper : Profile
{
    public HaGeRunDtoMapper()
    {
        CreateMap<User, RegisterViewModel>();
        CreateMap<RegisterViewModel, User>()
            .ConstructUsing(x => new User(x.FirstName!, x.LastName!, x.Email!, x.Password!))
            .ForMember(user => user.Name, opt => opt.MapFrom(x => x.FirstName + " " + x.LastName));
        
        CreateMap<User, LoginViewModel>();
        CreateMap<LoginViewModel, User>();
        
        CreateMap<CodeList, CodeListModel>();
        CreateMap<CodeListModel, CodeList>();
        
        CreateMap<CodeListValue, CodeListValueModel>();
        CreateMap<CodeListValueModel, CodeListValue>();
    }
}