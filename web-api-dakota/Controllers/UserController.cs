using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using web_api_dakota.Models.User;
using web_api_dakota.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace web_api_dakota.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IService<UserModel, UserRequestDTO, UserResponseDTO, UserUpdateDTO> _service;
    private readonly IUserModelService _userService;
    private readonly IMapper _mapper;

    public UserController(
        IService<UserModel, UserRequestDTO, UserResponseDTO, UserUpdateDTO> service,
        IUserModelService userService,
        IMapper mapper
    )
    {
        _userService = userService;
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="request">Request object containing the data of the user to be created.</param>
    /// <returns>Returns the details of the created user.</returns>
    /// <response code="201">User successfully created.</response>
    /// <response code="400">Invalid request or incomplete data.</response>
    /// <response code="409">Invalid request duplicate value.</response>
    [HttpPost("create")]
    [SwaggerOperation(Summary = "Creates a new user", Description = "Creates a new user based on the data provided in the request body.")]
    [SwaggerResponse(201, "User successfully created", typeof(UserResponseDTO))]
    [SwaggerResponse(400, "Invalid or incomplete data")]
    [SwaggerResponse(409, "Conflict")]
    public async Task<ActionResult<UserResponseDTO>> Create(UserRequestDTO request)
    {
        var result = (await _service.GetAllAsync()).Any(i => i.Email == request.Email);

        if (result)
            return Conflict();
        
        var userResponseDto = await _service.AddAsync(request);

        if (userResponseDto == null)
            return BadRequest("Could not create the user.");

        return CreatedAtAction(nameof(Create), new { id = userResponseDto.Id }, userResponseDto);
    }

    /// <summary>
    /// Retrieves a user by ID.
    /// </summary>
    /// <param name="id">User ID.</param>
    /// <returns>Returns the details of the requested user.</returns>
    /// <response code="200">User successfully found.</response>
    /// <response code="404">User not found.</response>
    [HttpGet("getById/{id}")]
    [SwaggerOperation(Summary = "Retrieves a user by ID", Description = "Retrieves the details of a specific user based on the provided ID.")]
    [SwaggerResponse(200, "User successfully found", typeof(UserResponseDTO))]
    [SwaggerResponse(404, "User not found")]
    public async Task<ActionResult<UserResponseDTO>> GetById(int id)
    {
        var userResponseDto = await _service.GetByIdAsync(id, e => e.RoleModels);

        if (userResponseDto == null)
            return NotFound();

        return Ok(userResponseDto);
    }

    /// <summary>
    /// Lists all users.
    /// </summary>
    /// <returns>Returns the list of all users.</returns>
    /// <response code="200">List of users successfully retrieved.</response>
    /// <response code="204">No users found.</response>
    [HttpGet("getAll")]
    [SwaggerOperation(Summary = "Lists all users", Description = "Retrieves the list of all registered users.")]
    [SwaggerResponse(200, "List of users successfully retrieved", typeof(IEnumerable<UserResponseDTO>))]
    [SwaggerResponse(204, "No users found")]
    public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetAll()
    {
        var userResponseDtos = await _service.GetAllAsync();

        if (userResponseDtos == null || userResponseDtos.Count() == 0)
            return NoContent();

        return Ok(userResponseDtos);
    }

    /// <summary>
    /// Adds a role to a specific user.
    /// </summary>
    /// <param name="idUser">The ID of the user to whom the role will be added.</param>
    /// <param name="idRole">The ID of the role to be added.</param>
    /// <returns>No content if the role is successfully added to the user, otherwise returns not found.</returns>
    /// <response code="204">The role was successfully added to the user.</response>
    /// <response code="404">The user or role was not found.</response>
    [HttpPost("{idUser}/role/{idRole}")]
    [SwaggerOperation(Summary = "Add a role to a user", Description = "Adds a specific role to a user based on the user ID and role ID.")]
    [SwaggerResponse(204, "The role was successfully added to the user.")]
    [SwaggerResponse(404, "User or role not found.")]
    public async Task<IActionResult> AddRoles(int idUser, int idRole)
    {
        var result = await _userService.AddRoleToUserModelAsync(idUser, idRole);
    
        if (!result)
            return NotFound();

        return NoContent();
    }
    
    /// <summary>
    /// Removes a role from a specific user.
    /// </summary>
    /// <param name="idUser">The ID of the user from whom the role will be removed.</param>
    /// <param name="idRole">The ID of the role to be removed.</param>
    /// <returns>No content if successful, otherwise not found.</returns>
    /// <response code="204">Role successfully removed from the user.</response>
    /// <response code="404">User or role not found.</response>
    [HttpDelete("{idUser}/role/{idRole}")]
    [SwaggerOperation(Summary = "Removes a role from a user", Description = "Removes a specific role from the given user.")]
    [SwaggerResponse(204, "Role successfully removed from the user.")]
    [SwaggerResponse(404, "User or role not found.")]
    public async Task<IActionResult> RemoveRoles(int idUser, int idRole)
    {
        var result = await _userService.RemoveRoleFromUserModelAsync(idUser, idRole);
    
        if (!result)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Updates the data of an existing user.
    /// </summary>
    /// <param name="id">User ID.</param>
    /// <param name="request">Request object containing the updated user data.</param>
    /// <returns>Returns the status of the operation.</returns>
    /// <response code="204">User successfully updated.</response>
    /// <response code="404">User not found.</response>
    [HttpPut("updateById/{id}")]
    [SwaggerOperation(Summary = "Updates a user", Description = "Updates the data of an existing user based on the provided information.")]
    [SwaggerResponse(204, "User successfully updated")]
    [SwaggerResponse(404, "User not found")]
    public async Task<ActionResult<UserResponseDTO>> Update(int id, [FromBody] UserUpdateDTO request)
    {
        var userResponseDto = await _service.UpdateAsync(id, request);

        if (!userResponseDto)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Removes a user by ID.
    /// </summary>
    /// <param name="id">User ID.</param>
    /// <returns>Returns the status of the operation.</returns>
    /// <response code="200">User successfully removed.</response>
    /// <response code="404">User not found.</response>
    [HttpDelete("deleteById/{id}")]
    [SwaggerOperation(Summary = "Removes a user", Description = "Removes a specific user based on the provided ID.")]
    [SwaggerResponse(200, "User successfully removed")]
    [SwaggerResponse(404, "User not found")]
    public async Task<ActionResult> DeleteById(int id)
    {
        var result = await _service.DeleteAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Removes all users from the system.
    /// </summary>
    /// <returns>Returns the status of the operation.</returns>
    /// <response code="204">All users successfully removed.</response>
    /// <response code="404">No users found for removal.</response>
    [HttpDelete("deleteAll")]
    [SwaggerOperation(Summary = "Removes all users", Description = "Removes all existing users from the system.")]
    [SwaggerResponse(204, "All users successfully removed")]
    [SwaggerResponse(404, "No users found for removal")]
    public async Task<IActionResult> DeleteAll()
    {
        var result = await _service.DeleteAllAsync();
    
        if (!result)
            return NotFound();

        return NoContent();
    }
    
}
