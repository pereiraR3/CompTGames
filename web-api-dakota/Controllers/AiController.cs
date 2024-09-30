using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using web_api_dakota.Models.AI;
using web_api_dakota.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using web_api_dakota.Models.Category;
using web_api_dakota.Models.Organization;
using web_api_dakota.Services;

namespace web_api_dakota.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AiController : ControllerBase
{

    private readonly IMapper _mapper;
    private readonly IService<AiModel, AiRequestDTO, AiResponseDTO, AiUpdateDTO> _service;
    private readonly IAiModelService _aiModelService;
    
    public AiController(
        IMapper mapper,
        IService<AiModel, AiRequestDTO, AiResponseDTO, AiUpdateDTO> service,
        IAiModelService aiModelService
        )
    {
        _mapper = mapper;
        _service = service;
        _aiModelService = aiModelService;
    }

    /// <summary>
    /// Creates a new AI model.
    /// </summary>
    /// <param name="request">Request object containing the data of the AI model to be created.</param>
    /// <returns>Returns the details of the created AI model.</returns>
    /// <response code="201">AI model successfully created.</response>
    /// <response code="400">Invalid request or incomplete data.</response>
    /// <response code="409">Conflict, duplicate AI model name.</response>
    [HttpPost("create")]
    [SwaggerOperation(Summary = "Creates a new AI model", Description = "Creates a new AI model based on the provided data.")]
    [SwaggerResponse(201, "AI model successfully created", typeof(AiResponseDTO))]
    [SwaggerResponse(400, "Invalid or incomplete data")]
    [SwaggerResponse(409, "Conflict, duplicate AI model name")]
    public async Task<ActionResult<AiResponseDTO>> Create(AiRequestDTO request)
    {
        // Verifica se o AI já existe
        var result = (await _service.GetAllAsync()).Any(x => x.Name == request.Name);
        if (result)
            return Conflict();

        // Busca a organização
        var organizationModel = _aiModelService.GetOrganizationModelByIdAsync(request.OrganizationId);
        if (organizationModel == null)
            return NotFound("Organization not found.");

        var aiModelResponse = await _service.AddAsync(request, organizationModel); 
    
        return CreatedAtAction(nameof(Create), new { id = aiModelResponse.Id }, aiModelResponse);
    }
    
    /// <summary>
    /// Gets the details of an AI model by ID.
    /// </summary>
    /// <param name="id">ID of the AI model to retrieve.</param>
    /// <returns>Returns the AI model details.</returns>
    /// <response code="200">AI model found.</response>
    /// <response code="404">AI model not found.</response>
    [HttpGet("getById/{id}")]
    [SwaggerOperation(Summary = "Gets an AI model by ID", Description = "Retrieves an AI model by its ID.")]
    [SwaggerResponse(200, "AI model found", typeof(AiResponseDTO))]
    [SwaggerResponse(404, "AI model not found")]
    public async Task<ActionResult<AiResponseDTO>> GetById(int id)
    {
        var aiResponseDto = await _service.GetByIdAsync(id, e => e.Organization, e => e.CategoryModels);

        if (aiResponseDto == null)
            return NotFound();
        
        return Ok(aiResponseDto);
    }

    /// <summary>
    /// Gets all AI models.
    /// </summary>
    /// <returns>Returns a list of all AI models.</returns>
    /// <response code="200">AI models found.</response>
    /// <response code="204">No AI models found.</response>
    [HttpGet("getAll")]
    [SwaggerOperation(Summary = "Gets all AI models", Description = "Retrieves a list of all AI models.")]
    [SwaggerResponse(200, "AI models found", typeof(IEnumerable<AiResponseDTO>))]
    [SwaggerResponse(204, "No AI models found")]
    public async Task<ActionResult<IEnumerable<AiResponseDTO>>> GetAll()
    {
        var aiResponseDtos = await _service.GetAllAsync();

        if (!aiResponseDtos.Any())
            return NoContent();

        return Ok(aiResponseDtos);
    }

    /// <summary>
    /// Adds a category to a specific AI model.
    /// </summary>
    /// <param name="idAi">The ID of the AI model.</param>
    /// <param name="idCategory">The ID of the category to be added.</param>
    /// <returns>No content if successful, otherwise not found.</returns>
    /// <response code="204">Category successfully added to the AI model.</response>
    /// <response code="404">AI model or category not found.</response>
    [HttpPost("{idAi}/category/{idCategory}")]
    [SwaggerOperation(Summary = "Adds a category to an AI model", Description = "Adds a specific category to the given AI model.")]
    [SwaggerResponse(204, "Category successfully added to the AI model.")]
    [SwaggerResponse(404, "AI model or category not found.")]
    public async Task<ActionResult<AiResponseDTO>> AddCategory(int idAi, int idCategory)
    {
        var result = await _aiModelService.AddCategoryToAiModelAsync(idAi, idCategory);
    
        if(!result)
            return NotFound();
    
        return NoContent();
    }

    /// <summary>
    /// Removes a category from a specific AI model.
    /// </summary>
    /// <param name="idAi">The ID of the AI model.</param>
    /// <param name="idCategory">The ID of the category to be removed.</param>
    /// <returns>No content if successful, otherwise not found.</returns>
    /// <response code="204">Category successfully removed from the AI model.</response>
    /// <response code="404">AI model or category not found.</response>
    [HttpDelete("{idAi}/category/{idCategory}")]
    [SwaggerOperation(Summary = "Removes a category from an AI model", Description = "Removes a specific category from the given AI model.")]
    [SwaggerResponse(204, "Category successfully removed from the AI model.")]
    [SwaggerResponse(404, "AI model or category not found.")]
    public async Task<IActionResult> DeleteCategory(int idAi, int idCategory)
    {
        var result = await _aiModelService.RemoveCategoryFromAiModelAsync(idAi, idCategory);
    
        if(!result)
            return NotFound();
    
        return NoContent();
    }

    /// <summary>
    /// Updates an AI model.
    /// </summary>
    /// <param name="id">ID of the AI model to update.</param>
    /// <param name="request">Updated data for the AI model.</param>
    /// <returns>No content if the update was successful.</returns>
    /// <response code="204">AI model successfully updated.</response>
    /// <response code="404">AI model not found.</response>
    [HttpPut("updateById/{id}")]
    [SwaggerOperation(Summary = "Updates an AI model", Description = "Updates an AI model by its ID.")]
    [SwaggerResponse(204, "AI model successfully updated")]
    [SwaggerResponse(404, "AI model not found")]
    public async Task<ActionResult<AiResponseDTO>> Update(int id, AiUpdateDTO request)
    {
        var result = await _service.UpdateAsync(id, request);
        
        if (!result)
            return NotFound();
        
        return NoContent();
    }

    /// <summary>
    /// Deletes an AI model by ID.
    /// </summary>
    /// <param name="id">ID of the AI model to delete.</param>
    /// <returns>No content if the deletion was successful.</returns>
    /// <response code="204">AI model successfully deleted.</response>
    /// <response code="404">AI model not found.</response>
    [HttpDelete("deleteById/{id}")]
    [SwaggerOperation(Summary = "Deletes an AI model", Description = "Deletes an AI model by its ID.")]
    [SwaggerResponse(204, "AI model successfully deleted")]
    [SwaggerResponse(404, "AI model not found")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        
        if (!result)
            return NotFound();
        
        return NoContent();
    }

    /// <summary>
    /// Deletes all AI models.
    /// </summary>
    /// <returns>No content if the deletion was successful.</returns>
    /// <response code="204">All AI models successfully deleted.</response>
    /// <response code="404">No AI models found.</response>
    [HttpDelete("deleteAll")]
    [SwaggerOperation(Summary = "Deletes all AI models", Description = "Deletes all AI models.")]
    [SwaggerResponse(204, "All AI models successfully deleted")]
    [SwaggerResponse(404, "No AI models found")]
    public async Task<IActionResult> DeleteAll()
    {
        var result = await _service.DeleteAllAsync();
    
        if (!result)
            return NotFound();

        return NoContent();
    }
    
}
