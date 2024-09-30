using AutoMapper;
using web_api_dakota.Models.AI;
using web_api_dakota.Models.Category;
using web_api_dakota.Models.Organization;
using web_api_dakota.Models.Plan;
using web_api_dakota.Models.User;

namespace web_api_dakota.Data.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        
        // Mapped UserModel
        
        CreateMap<UserModel, UserResponseDTO>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.RoleModels));
        
        CreateMap<UserUpdateDTO, UserModel>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())  // Ignora o mapeamento do Id, já que ele não é alterado no update.
            .ForMember(dest => dest.Password,
                opt => opt
                    .MapFrom(src => HashPassword(src.Password))); // Hash da senha
        
        // Mapped OrganizationModel
        
        CreateMap<OrganizationResponseDTO, OrganizationModel>();
        
        CreateMap<OrganizationModel, OrganizationResponseDTO>()
            .ForMember(dest => dest.AiModels, opt => opt.MapFrom(src => src.AiModels));
        
        // Mapped AiModel

        CreateMap<AiRequestDTO, AiModel>();
        
        CreateMap<AiModel, AiResponseDTO>()
            .ForMember(dest => dest.Organization, opt => opt.MapFrom(src => src.Organization))
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.CategoryModels));

        // Mapped PlanModel
        
        CreateMap<PlanModel, PlanResponseDTO>();
        
        // Mapped CategoryModel
        
        CreateMap<CategoryModel, CategoryResponseDTO>()
            .ForMember(dest => dest.AiModels, opt => opt.MapFrom(src => src.AiModels));

        CreateMap<CategoryResponseDTO, CategoryModel>()
            .ForMember(dest => dest.AiModels, opt => opt.Ignore()); // Ignorando o mapeamento de AiModels para evitar loops

        CreateMap<CategoryUpdateDTO, CategoryModel>()
            .ForAllMembers(
                opts => opts.Condition(
                    (src, dest, srcMember) => srcMember != null));
    }
    
    private string HashPassword(string password)
    {
        // Substitua isso por um algoritmo real de hash, como BCrypt
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password)); // Simulação de hash
    }
    
}