using System.Reflection;
using Bleeter.BleetsService.Data.Models;
using Bleeter.BleetsService.Dtos;
using Mapster;

namespace Bleeter.BleetsService.Extensions;

public static class MapsterConfigurationExtension
{
    public static IServiceCollection RegisterMapsterConfiguration(this IServiceCollection collection)
    {
        TypeAdapterConfig<BleetModel, GetBleetDto>.NewConfig()
            .Map(d => d.Likes, d => d.Likes.Count)
            .Map(d => d.Comments, d => d.Comments.Count);
        
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        
        return collection;
    }
}