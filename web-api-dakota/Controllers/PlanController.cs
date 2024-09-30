using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using web_api_dakota.Models.Plan;
using web_api_dakota.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace web_api_dakota.Controllers;

public class PlanController : ControllerBase
{
    
    private readonly IMapper _mapper;
    private readonly IService<PlanModel, PlanRequestDTO, PlanResponseDTO, PlanUpdateDTO> _service;

    public PlanController(IMapper mapper, IService<PlanModel, PlanRequestDTO, PlanResponseDTO, PlanUpdateDTO> service)
    {
        _mapper = mapper;
        _service = service;
    }

    /// <summary>
    /// Creates a new plan.
    /// </summary>
    /// <param name="request">Request object containing the data of the plan to be created.</param>
    /// <returns>Returns the details of the created plan.</returns>
    /// <response code="201">Plan successfully created.</response>
    /// <response code="400">Invalid request or incomplete data.</response>
    [HttpPost("create")]
    [SwaggerOperation(Summary = "Creates a new plan", Description = "Creates a new plan based on the provided data.")]
    [SwaggerResponse(201, "Plan successfully created", typeof(PlanResponseDTO))]
    [SwaggerResponse(400, "Invalid or incomplete data")]
    public async Task<ActionResult<PlanResponseDTO>> Create(PlanRequestDTO request)
    {
        var planResponseDto = await _service.AddAsync(request);

        if (planResponseDto == null)
            return BadRequest("Could not create the plan.");

        return CreatedAtAction(nameof(Create), new { id = planResponseDto.Id }, planResponseDto);
    }

    /// <summary>
    /// Gets the details of a plan by ID.
    /// </summary>
    /// <param name="id">ID of the plan to retrieve.</param>
    /// <returns>Returns the plan details.</returns>
    /// <response code="200">Plan found.</response>
    /// <response code="404">Plan not found.</response>
    [HttpGet("getById/{id}")]
    [SwaggerOperation(Summary = "Gets a plan by ID", Description = "Retrieves a plan by its ID.")]
    [SwaggerResponse(200, "Plan found", typeof(PlanResponseDTO))]
    [SwaggerResponse(404, "Plan not found")]
    public async Task<ActionResult<PlanResponseDTO>> GetById(int id)
    {
        var planResponseDto = await _service.GetByIdAsync(id, e => e.Ai);

        if (planResponseDto == null)
            return NotFound();
        
        return Ok(planResponseDto);
    }

    /// <summary>
    /// Gets all plans.
    /// </summary>
    /// <returns>Returns a list of all plans.</returns>
    /// <response code="200">Plans found.</response>
    /// <response code="204">No plans found.</response>
    [HttpGet("getAll")]
    [SwaggerOperation(Summary = "Gets all plans", Description = "Retrieves a list of all plans.")]
    [SwaggerResponse(200, "Plans found", typeof(IEnumerable<PlanResponseDTO>))]
    [SwaggerResponse(204, "No plans found")]
    public async Task<ActionResult<IEnumerable<PlanResponseDTO>>> GetAll()
    {
        var planResponseDtos = await _service.GetAllAsync();

        if (!planResponseDtos.Any())
            return NoContent();

        return Ok(planResponseDtos);
    }

    /// <summary>
    /// Updates a plan.
    /// </summary>
    /// <param name="id">ID of the plan to update.</param>
    /// <param name="request">Updated data for the plan.</param>
    /// <returns>No content if the update was successful.</returns>
    /// <response code="204">Plan successfully updated.</response>
    /// <response code="404">Plan not found.</response>
    [HttpPut("updateById/{id}")]
    [SwaggerOperation(Summary = "Updates a plan", Description = "Updates a plan by its ID.")]
    [SwaggerResponse(204, "Plan successfully updated")]
    [SwaggerResponse(404, "Plan not found")]
    public async Task<ActionResult<PlanResponseDTO>> Update(int id, PlanUpdateDTO request)
    {
        var result = await _service.UpdateAsync(id, request);
        
        if (!result)
            return NotFound();
        
        return NoContent();
    }

    /// <summary>
    /// Deletes a plan by ID.
    /// </summary>
    /// <param name="id">ID of the plan to delete.</param>
    /// <returns>No content if the deletion was successful.</returns>
    /// <response code="204">Plan successfully deleted.</response>
    /// <response code="404">Plan not found.</response>
    [HttpDelete("deleteById/{id}")]
    [SwaggerOperation(Summary = "Deletes a plan", Description = "Deletes a plan by its ID.")]
    [SwaggerResponse(204, "Plan successfully deleted")]
    [SwaggerResponse(404, "Plan not found")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        
        if (!result)
            return NotFound();
        
        return NoContent();
    }

    /// <summary>
    /// Deletes all plans.
    /// </summary>
    /// <returns>No content if the deletion was successful.</returns>
    /// <response code="204">All plans successfully deleted.</response>
    /// <response code="404">No plans found.</response>
    [HttpDelete("deleteAll")]
    [SwaggerOperation(Summary = "Deletes all plans", Description = "Deletes all plans.")]
    [SwaggerResponse(204, "All plans successfully deleted")]
    [SwaggerResponse(404, "No plans found")]
    public async Task<IActionResult> DeleteAll()
    {
        var result = await _service.DeleteAllAsync();
    
        if (!result)
            return NotFound();

        return NoContent();
    }   
}
