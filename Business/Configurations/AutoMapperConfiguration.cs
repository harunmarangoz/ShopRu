using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Models.Dtos;
using Models.Entities;

namespace Business.Configurations;

public static class AutoMapperConfiguration
{
    public static void ConfigureService(IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new DefaultMapProfile());
        });
        var mapper = mappingConfig.CreateMapper();
        services.AddSingleton<IMapper>(mapper);
    }
}

public class DefaultMapProfile : Profile
{
    public DefaultMapProfile()
    {
        CreateMap<UserDto, User>();
        
        CreateMap<CreateOrderDto, Order>();
        CreateMap<GetOrderDto, Order>();
        CreateMap<OrderItemDto, OrderItem>();
    }
}