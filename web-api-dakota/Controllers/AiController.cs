using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using web_api_dakota.Models.AI;
using web_api_dakota.Services.Interfaces;

namespace web_api_dakota.Controllers;

public class AiController : ControllerBase
{

    private readonly IMapper _mapper;
    private readonly IService<AiModel, AiRequestDTO, AiResponseDTO, AiUpdateDTO> _service;

    public AiController(IMapper mapper, IService<AiModel, AiRequestDTO, AiResponseDTO, AiUpdateDTO> service)
    {
        _mapper = mapper;
        _service = service;
    }
    

}