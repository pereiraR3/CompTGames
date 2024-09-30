using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using web_api_dakota.Models.Category;
using web_api_dakota.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace web_api_dakota.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    
    private readonly IMapper _mapper;
    private readonly IService<CategoryModel, CategoryRequestDTO, CategoryResponseDTO, CategoryUpdateDTO> _service;

    public CategoryController(IMapper mapper, IService<CategoryModel, CategoryRequestDTO, CategoryResponseDTO, CategoryUpdateDTO> service)
    {
        _mapper = mapper;
        _service = service;
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="request">Request object containing the data of the category to be created.</param>
    /// <returns>Returns the details of the created category.</returns>
    /// <response code="201">Category successfully created.</response>
    /// <response code="400">Invalid request or incomplete data.</response>
    /// <response code="409">Conflict, duplicate category name.</response>
    [HttpPost("create")]
    [SwaggerOperation(Summary = "Creates a new category", Description = "Creates a new category based on the provided data.")]
    [SwaggerResponse(201, "Category successfully created", typeof(CategoryResponseDTO))]
    [SwaggerResponse(400, "Invalid or incomplete data")]
    [SwaggerResponse(409, "Conflict, duplicate category name")]
    public async Task<ActionResult<CategoryResponseDTO>> Create(CategoryRequestDTO request)
    {
        var result = (await _service.GetAllAsync()).Any(x => x.Name == request.Name);

        if (result)
            return Conflict();
        
        var categoryResponseDto = await _service.AddAsync(request);

        if (categoryResponseDto == null)
            return BadRequest("Could not create the category.");

        return CreatedAtAction(nameof(Create), new { id = categoryResponseDto.Id }, categoryResponseDto);
    }

    /// <summary>
    /// Gets the details of a category by ID.
    /// </summary>
    /// <param name="id">ID of the category to retrieve.</param>
    /// <returns>Returns the category details.</returns>
    /// <response code="200">Category found.</response>
    /// <response code="404">Category not found.</response>
    [HttpGet("getById/{id}")]
    [SwaggerOperation(Summary = "Gets a category by ID", Description = "Retrieves a category by its ID.")]
    [SwaggerResponse(200, "Category found", typeof(CategoryResponseDTO))]
    [SwaggerResponse(404, "Category not found")]
    public async Task<ActionResult<CategoryResponseDTO>> GetById(int id)
    {
        var categoryResponseDto = await _service.GetByIdAsync(id, e => e.AiModels);

        if (categoryResponseDto == null)
            return NotFound();
        
        return Ok(categoryResponseDto);
    }

    /// <summary>
    /// Gets all categories.
    /// </summary>
    /// <returns>Returns a list of all categories.</returns>
    /// <response code="200">Categories found.</response>
    /// <response code="204">No categories found.</response>
    [HttpGet("getAll")]
    [SwaggerOperation(Summary = "Gets all categories", Description = "Retrieves a list of all categories.")]
    [SwaggerResponse(200, "Categories found", typeof(IEnumerable<CategoryResponseDTO>))]
    [SwaggerResponse(204, "No categories found")]
    public async Task<ActionResult<IEnumerable<CategoryResponseDTO>>> GetAll()
    {
        var categoryResponseDtos = await _service.GetAllAsync();

        if (!categoryResponseDtos.Any())
            return NoContent();

        return Ok(categoryResponseDtos);
    }

    /// <summary>
    /// Updates a category.
    /// </summary>
    /// <param name="id">ID of the category to update.</param>
    /// <param name="request">Updated data for the category.</param>
    /// <returns>No content if the update was successful.</returns>
    /// <response code="204">Category successfully updated.</response>
    /// <response code="404">Category not found.</response>
    [HttpPut("updateById/{id}")]
    [SwaggerOperation(Summary = "Updates a category", Description = "Updates a category by its ID.")]
    [SwaggerResponse(204, "Category successfully updated")]
    [SwaggerResponse(404, "Category not found")]
    public async Task<ActionResult<CategoryResponseDTO>> Update(int id, CategoryUpdateDTO request)
    {
        var result = await _service.UpdateAsync(id, request);
        
        if (!result)
            return NotFound();
        
        return NoContent();
    }
    
    /// <summary>
    /// Deletes a category by ID.
    /// </summary>
    /// <param name="id">ID of the category to delete.</param>
    /// <returns>No content if the deletion was successful.</returns>
    /// <response code="204">Category successfully deleted.</response>
    /// <response code="404">Category not found.</response>
    [HttpDelete("deleteById/{id}")]
    [SwaggerOperation(Summary = "Deletes a category", Description = "Deletes a category by its ID.")]
    [SwaggerResponse(204, "Category successfully deleted")]
    [SwaggerResponse(404, "Category not found")]
    public async Task<ActionResult<IActionResult>> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        
        if (!result)
            return NotFound();
        
        return NoContent();
    }

    /// <summary>
    /// Deletes all categories.
    /// </summary>
    /// <returns>No content if the deletion was successful.</returns>
    /// <response code="204">All categories successfully deleted.</response>
    /// <response code="404">No categories found.</response>
    [HttpDelete("deleteAll")]
    [SwaggerOperation(Summary = "Deletes all categories", Description = "Deletes all categories.")]
    [SwaggerResponse(204, "All categories successfully deleted")]
    [SwaggerResponse(404, "No categories found")]
    public async Task<IActionResult> DeleteAll()
    {
        var result = await _service.DeleteAllAsync();
    
        if (!result)
            return NotFound();

        return NoContent();
    }    
}
