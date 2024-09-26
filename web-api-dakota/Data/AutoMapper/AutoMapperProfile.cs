using AutoMapper;
using web_api_dakota.Models.Category;
using web_api_dakota.Models.Organization;
using web_api_dakota.Models.Plan;
using web_api_dakota.Models.User;

namespace web_api_dakota.Data.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        
        // Mapeamento de UserModel para UserResponseDTO (do domínio para o DTO de resposta)
        CreateMap<UserModel, UserResponseDTO>();
        CreateMap<UserUpdateDTO, UserModel>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())  // Ignora o mapeamento do Id, já que ele não é alterado no update.
            .ForMember(dest => dest.Password,
                opt => opt
                    .MapFrom(src => HashPassword(src.Password))); // Hash da senha

        // Mapeamento de CategoryModel para CategoryResponseDTO
        CreateMap<CategoryModel, CategoryResponseDTO>();

        // Mapeamento de OrganizationModel para OrganizationResponseDTO
        CreateMap<OrganizationModel, OrganizationResponseDTO>();

        // Mapeamento de PlanModel para PlanResponseDTO
        CreateMap<PlanModel, PlanResponseDTO>();
        
        // Mapeamento de CategoryRequestDTO para CategoryModel (para criação e atualização)
        CreateMap<CategoryRequestDTO, CategoryModel>();

        // O mapeamento de atualização deve ignorar campos nulos (Partial Update)
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