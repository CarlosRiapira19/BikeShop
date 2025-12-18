using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using BikeShopAPI.Controllers;
using BikeShopAPI.Entities;
using Xunit;

namespace BikeShopAPI.Test.Controllers
{
    public class BikeShopControllerTest
    {
        private readonly BikeShopController _controller;
        private readonly Mock<ILogger<BikeShopController>> _loggerMock;

        public BikeShopControllerTest()
        {
            _loggerMock = new Mock<ILogger<BikeShopController>>();
            _controller = new BikeShopController(_loggerMock.Object);
        }

        [Fact]
        public void GetAll_ReturnsAllItems()
        {
            var result = _controller.GetAll().Result as OkObjectResult;

            Assert.NotNull(result);
            var items = Assert.IsType<List<BikeShop>>(result.Value);
            Assert.NotNull(items);
        }

        [Fact]
        public void GetById_ReturnsBikeShop()
        {
            // Primero crear un elemento
            var newBikeShop = new BikeShop 
            { 
                Name = "Test Shop",
                Address = "Test Address",
                City = "Test City"
            };
            var createResult = _controller.Create(newBikeShop).Result as CreatedAtActionResult;
            Assert.NotNull(createResult);
            var createdShop = createResult.Value as BikeShop;
            Assert.NotNull(createdShop);

            // Ahora buscar el elemento creado
            var result = _controller.GetById(createdShop.Id).Result as OkObjectResult;
            
            Assert.NotNull(result);
            Assert.IsType<BikeShop>(result.Value);
        }
        [Fact]
        public void GetById_ReturnsNotFound_WhenIdDoesNotExist()
        {
            var result = _controller.GetById(999).Result;
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_ReturnsCreatedBikeShop()
        {
            var newBikeShop = new BikeShop 
            { 
                Name = "Test Bike Shop",
                Address = "Test Address",
                City = "Test City"
            };

            var result = _controller.Create(newBikeShop).Result as CreatedAtActionResult;
            
            Assert.NotNull(result);
            Assert.IsType<BikeShop>(result.Value);
            Assert.Equal(nameof(_controller.GetById), result.ActionName);
        }

        [Fact]
        public void Create_ReturnsBadRequest_WhenModelIsInvalid()
        {
            _controller.ModelState.AddModelError("Name", "Required");
            var newBikeShop = new BikeShop();

            var result = _controller.Create(newBikeShop).Result;
            
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Update_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Primero crear un elemento
            var newBikeShop = new BikeShop 
            { 
                Name = "Original Shop",
                Address = "Original Address",
                City = "Original City"
            };
            var createResult = _controller.Create(newBikeShop).Result as CreatedAtActionResult;
            Assert.NotNull(createResult);
            var createdShop = createResult.Value as BikeShop;
            Assert.NotNull(createdShop);

            // Ahora actualizar el elemento creado
            var updatedBikeShop = new BikeShop 
            { 
                Id = createdShop.Id,
                Name = "Updated Shop",
                Address = "Updated Address",
                City = "Updated City"
            };

            var result = _controller.Update(createdShop.Id, updatedBikeShop);
            
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Update_ReturnsNotFound_WhenIdDoesNotExist()
        {
            var updatedBikeShop = new BikeShop 
            { 
                Id = 999,
                Name = "Updated Shop"
            };

            var result = _controller.Update(999, updatedBikeShop);
            
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Update_ReturnsBadRequest_WhenIdMismatch()
        {
            var updatedBikeShop = new BikeShop { Id = 2 };

            var result = _controller.Update(1, updatedBikeShop);
            
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Delete_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Primero crear un elemento
            var newBikeShop = new BikeShop 
            { 
                Name = "Shop to Delete"
            };
            var createResult = _controller.Create(newBikeShop).Result as CreatedAtActionResult;
            Assert.NotNull(createResult);
            var createdShop = createResult.Value as BikeShop;
            Assert.NotNull(createdShop);

            // Ahora eliminar el elemento creado
            var result = _controller.Delete(createdShop.Id);
            
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void Delete_ReturnsNotFound_WhenIdDoesNotExist()
        {
            var result = _controller.Delete(999);
            
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
