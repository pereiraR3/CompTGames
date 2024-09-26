using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using web_api_dakota.Models.Category;
using web_api_dakota.Services.Interfaces;

namespace web_api_dakota.Controllers;

public class CategoryController : ControllerBase
{
    
    private readonly IMapper _mapper;
    private readonly IService<CategoryModel, CategoryRequestDTO, CategoryResponseDTO, CategoryUpdateDTO> _service;

    public CategoryController(IMapper mapper, IService<CategoryModel, CategoryRequestDTO, CategoryResponseDTO, CategoryUpdateDTO> service)
    {
        _mapper = mapper;
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<CategoryResponseDTO>> Create(CategoryRequestDTO request)
    {
        var result = (await _service.GetAllAsync()).Any(x => x.Name == request.Name);

        if (result)
            return Conflict();
        
        var categoryResponseDto = await _service.AddAsync(request);

        if (categoryResponseDto == null)
            return BadRequest("Could not create the organization.");

        return CreatedAtAction(nameof(Create), new { id = categoryResponseDto.Id }, categoryResponseDto);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryResponseDTO>> GetById(int id)
    {
        var categoryResponseDto = await _service.GetByIdAsync(id);

        if (categoryResponseDto == null)
            return NotFound();
        
        return Ok(categoryResponseDto);
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponseDTO>>> GetAll()
    {
        var categoryResponseDtos = await _service.GetAllAsync();

        if (!categoryResponseDtos.Any())
            return NoContent();

        return Ok(categoryResponseDtos);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryResponseDTO>> Update(int id, CategoryUpdateDTO request)
    {
        var result = await _service.UpdateAsync(id, request);
        
        if (!result)
            return NotFound();
        
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<IActionResult>> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);
        
        if (!result)
            return NotFound();
        
        return NoContent();
    }
    

    [HttpDelete]
    public async Task<IActionResult> DeleteAll()
    {
        var result = await _service.DeleteAllAsync();
    
        if (!result)
            return NotFound();

        return NoContent();
    }    

}