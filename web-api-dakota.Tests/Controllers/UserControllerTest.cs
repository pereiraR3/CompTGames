using System.Collections.Generic;
using System.Linq;
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

namespace web_api_dakota.Tests.Controllers
{
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
        public async Task Create_ShouldReturnCreatedAtAction_WhenCalled()
        {
            // Arrange 
            var userRequest = new UserRequestDTO("username", "password", "teste@gmail.com", new List<UserRole>());
            var userResponse = new UserResponseDTO(1, "username", "password", "teste@gmail.com", new List<UserRole>());

            _serviceMock
                .Setup(s => s.AddAsync(userRequest, null))
                .ReturnsAsync(userResponse);

            // Act 
            var result = await _controller.Create(userRequest);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("Create", actionResult.ActionName);
            Assert.Equal(userResponse, actionResult.Value);
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenUserRequestIsNull()
        {
            // Arrange
            var userRequest = new UserRequestDTO("username", "password", null, null);

            _serviceMock
                .Setup(s => s.AddAsync(userRequest, null))
                .ReturnsAsync((UserResponseDTO)null);

            // Act
            var result = await _controller.Create(userRequest);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenUserExists()
        {
            // Arrange
            var userResponse = new UserResponseDTO(1, "username", "password", "teste@gmail.com", new List<UserRole>());

            _serviceMock
                .Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync(userResponse);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(userResponse, okResult.Value);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            _serviceMock
                .Setup(s => s.GetByIdAsync(1))
                .ReturnsAsync((UserResponseDTO)null);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WhenUsersExist()
        {
            // Arrange
            var userResponseList = new List<UserResponseDTO>
            {
                new UserResponseDTO(1, "username1", "password1", "teste1@gmail.com", new List<UserRole>()),
                new UserResponseDTO(2, "username2", "password2", "teste2@gmail.com", new List<UserRole>())
            };

            _serviceMock
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(userResponseList);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(userResponseList, okResult.Value);
        }

        [Fact]
        public async Task GetAll_ShouldReturnNoContent_WhenNoUsersExist()
        {
            // Arrange
            _serviceMock
                .Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<UserResponseDTO>());

            // Act
            var result = await _controller.GetAll();

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task Update_ShouldReturnNoContent_WhenUserIsUpdated()
        {
            // Arrange
            var userUpdate = new UserUpdateDTO("newUsername", "newPassword", "newEmail@gmail.com", new List<UserRole>());

            _serviceMock
                .Setup(s => s.UpdateAsync(1, userUpdate))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.Update(1, userUpdate);

            // Assert
            var actualResult = Assert.IsType<ActionResult<UserResponseDTO>>(result);
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenUserIsNotFound()
        {
            // Arrange
            var userUpdate = new UserUpdateDTO("newUsername", "newPassword", "newEmail@gmail.com", new List<UserRole>());

            _serviceMock
                .Setup(s => s.UpdateAsync(1, userUpdate))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.Update(1, userUpdate);

            // Assert
            var actualResult = Assert.IsType<ActionResult<UserResponseDTO>>(result);
            Assert.IsType<NotFoundResult>(actualResult.Result);
        }

        [Fact]
        public async Task DeleteById_ShouldReturnNoContent_WhenUserIsDeleted()
        {
            // Arrange
            _serviceMock
                .Setup(s => s.DeleteAsync(1))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteById(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteById_ShouldReturnNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            _serviceMock
                .Setup(s => s.DeleteAsync(1))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteAll_ShouldReturnNoContent_WhenAllUsersAreDeleted()
        {
            // Arrange
            _serviceMock
                .Setup(s => s.DeleteAllAsync())
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteAll();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteAll_ShouldReturnNotFound_WhenNoUsersExistToDelete()
        {
            // Arrange
            _serviceMock
                .Setup(s => s.DeleteAllAsync())
                .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteAll();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
