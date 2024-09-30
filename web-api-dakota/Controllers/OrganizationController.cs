using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using web_api_dakota.Models.Organization;
using web_api_dakota.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace web_api_dakota.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationController : ControllerBase
{
    private readonly IService<OrganizationModel, OrganizationRequestDTO, OrganizationResponseDTO, OrganizationUpdateDTO> _service;
    private readonly IMapper _mapper;

    public OrganizationController(IService<OrganizationModel, OrganizationRequestDTO, OrganizationResponseDTO, OrganizationUpdateDTO> service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new organization.
    /// </summary>
    /// <param name="request">Request object containing the data of the organization to be created.</param>
    /// <returns>Returns the details of the created organization.</returns>
    /// <response code="201">Organization successfully created.</response>
    /// <response code="400">Invalid request or incomplete data.</response>
    /// <response code="409">Invalid request duplicate value.</response>
    [HttpPost("create")]
    [SwaggerOperation(Summary = "Creates a new organization", Description = "Creates a new organization based on the provided data.")]
    [SwaggerResponse(201, "Organization successfully created", typeof(OrganizationResponseDTO))]
    [SwaggerResponse(400, "Invalid or incomplete data")]
    [SwaggerResponse(409, "Conflict")]
    public async Task<ActionResult<OrganizationResponseDTO>> Create(OrganizationRequestDTO request)
    {
        var result = (await _service.GetAllAsync()).Any(x => x.Name == request.Name);
        
        if(result)
            return Conflict();
        
        var organizationResponseDto = await _service.AddAsync(request);

        if (organizationResponseDto == null)
            return BadRequest("Could not create the organization.");

        return CreatedAtAction(nameof(Create), new { id = organizationResponseDto.Id }, organizationResponseDto);
    }

    /// <summary>
    /// Gets an organization by ID.
    /// </summary>
    /// <param name="id">Organization ID.</param>
    /// <returns>Returns the details of the requested organization.</returns>
    /// <response code="200">Organization successfully found.</response>
    /// <response code="404">Organization not found.</response>
    [HttpGet("getById/{id}")]
    [SwaggerOperation(Summary = "Gets an organization by ID", Description = "Retrieves the details of a specific organization based on the provided ID.")]
    [SwaggerResponse(200, "Organization successfully found", typeof(OrganizationResponseDTO))]
    [SwaggerResponse(404, "Organization not found")]
    public async Task<ActionResult<OrganizationResponseDTO>> GetById(int id)
    {
        var organizationResponseDto = await _service.GetByIdAsync(id, e => e.AiModels);

        if (organizationResponseDto == null)
            return NotFound();
        
        return Ok(organizationResponseDto);
    }

    /// <summary>
    /// Lists all organizations.
    /// </summary>
    /// <returns>Returns the list of all organizations.</returns>
    /// <response code="200">List of organizations successfully retrieved.</response>
    /// <response code="204">No organizations found.</response>
    [HttpGet("getAll")]
    [SwaggerOperation(Summary = "Lists all organizations", Description = "Retrieves the list of all registered organizations.")]
    [SwaggerResponse(200, "List of organizations successfully retrieved", typeof(IEnumerable<OrganizationResponseDTO>))]
    [SwaggerResponse(204, "No organizations found")]
    public async Task<ActionResult<IEnumerable<OrganizationResponseDTO>>> GetAll()
    {
        var organizationResponseDtos = await _service.GetAllAsync();

        if (organizationResponseDtos == null)
            return NoContent();

        return Ok(organizationResponseDtos);
    }

    /// <summary>
    /// Updates the data of an existing organization.
    /// </summary>
    /// <param name="id">Organization ID.</param>
    /// <param name="request">Request object with the updated data of the organization.</param>
    /// <returns>Returns the status of the operation.</returns>
    /// <response code="204">Organization successfully updated.</response>
    /// <response code="404">Organization not found.</response>
    [HttpPut("updateById/{id}")]
    [SwaggerOperation(Summary = "Updates an organization", Description = "Updates the data of an existing organization based on the provided data.")]
    [SwaggerResponse(204, "Organization successfully updated")]
    [SwaggerResponse(404, "Organization not found")]
    public async Task<ActionResult<OrganizationResponseDTO>> Update(int id, OrganizationUpdateDTO request)
    {
        var result = await _service.UpdateAsync(id, request);
        
        if (!result)
            return NotFound();
        
        return NoContent();
    }

    /// <summary>
    /// Removes an organization by ID.
    /// </summary>
    /// <param name="id">Organization ID.</param>
    /// <returns>Returns the status of the operation.</returns>
    /// <response code="204">Organization successfully removed.</response>
    /// <response code="404">Organization not found.</response>
    [HttpDelete("deleteById/{id}")]
    [SwaggerOperation(Summary = "Removes an organization", Description = "Removes a specific organization based on the provided ID.")]
    [SwaggerResponse(204, "Organization successfully removed")]
    [SwaggerResponse(404, "Organization not found")]
    public async Task<IActionResult> DeleteById(int id)
    {
        var result = await _service.DeleteAsync(id);
        
        if (!result)
            return NotFound();
        
        return NoContent();
    }
    
    /// <summary>
    /// Removes all organizations from the system.
    /// </summary>
    /// <returns>Returns the status of the operation.</returns>
    /// <response code="204">All organizations successfully removed.</response>
    /// <response code="404">No organizations found for removal.</response>
    [HttpDelete("deleteAll")]
    [SwaggerOperation(Summary = "Removes all organizations", Description = "Removes all existing organizations from the system.")]
    [SwaggerResponse(204, "All organizations successfully removed")]
    [SwaggerResponse(404, "No organizations found for removal")]
    public async Task<IActionResult> DeleteAll()
    {
        var result = await _service.DeleteAllAsync();
    
        if (!result)
            return NotFound();

        return NoContent();
    }
    
}
