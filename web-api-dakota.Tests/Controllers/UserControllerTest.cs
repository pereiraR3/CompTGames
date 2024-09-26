using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Moq;
using web_api_dakota.Controllers;
using web_api_dakota.Models;
using web_api_dakota.Models.User;
using web_api_dakota.Services.Interfaces;
using Xunit;

namespace web_api_dakota.Tests.Controllers;

[TestSubject(typeof(UserController))]
public class UserControllerTest
{
    
    private readonly Mock<IService<UserModel, UserRequestDTO, UserResponseDTO, UserUpdateDTO>> _serviceMock;
    private readonly Mock<IMapper> _mapper;
    private readonly UserController _controller;

    public UserControllerTest()
    {
        
        _serviceMock = new Mock<IService<UserModel, UserRequestDTO, UserResponseDTO, UserUpdateDTO>>();
        _mapper = new Mock<IMapper>();
     
        _controller = new UserController(_serviceMock.Object, _mapper.Object);
        
    }

    [Fact]
    public async Task Create_ShoulReturnCreatedAtAction_WhenCalled()
    {
        
        // Arrange 
        var userRequest = new UserRequestDTO("username", "password", "teste@gmail.com", new List<UserRole>());
        var userResponse = new UserResponseDTO(1, "username", "password", "teste@gmail.com", new List<UserRole>());
        
        _serviceMock
            .Setup(s => s.AddAsync(userRequest))
            .ReturnsAsync(userResponse);
        
        // Act 
        var result = await _controller.Create(userRequest);
        
        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal("Create", actionResult.ActionName);
        Assert.Equal(userResponse, actionResult.Value);
        
    }

    [Fact]
    public async Task Create_ShoulReturnBadRequest_WhenUserRequestIsNull()
    {
        // Arrange
        var userRequest = new UserRequestDTO("username", "password", null, null);

        _serviceMock
            .Setup(s => s.AddAsync(userRequest))
            .ReturnsAsync((UserResponseDTO)null);

        // Act
        var result = await _controller.Create(userRequest);

        // Assert
        
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(400, badRequestResult.StatusCode);

    }
    
    
}