using AutoMapper;
using ElasticSAearch.Entities.Entities;
using ElasticSAearch.Models.RequestModels;

namespace ElasticSAearch.Manager.DTOMapperProfile;

/// <summary>
/// AutoMapper profile for mapping between task models and entities.
/// </summary>
public class AutoMapperProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AutoMapperProfile"/> class.
    /// </summary>
    public AutoMapperProfile()
    {
        CreateMap<UserModel, UserEntity>().ReverseMap();
    }
}
