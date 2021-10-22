using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ShopBridge.Controllers;
using ShopBridge.Models;
using ShopBridge.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ControllerTestCases
{
    public class InventoryControllerTest
    {
        [Fact]
        public async Task Calling_GetAllItems_Should_Return_ItemsList()
        {
            var logger = new Mock<ILogger<InventoryController>>();
            var Iactivityrepo = new Mock<IActivityRepository>();

            List<Items> ListItems = new List<Items>()
            {
                new Items{id=1,name="mouse",description="nothing",price=555.5},
                new Items{id=2,name="keyboard",description="nothing",price=555.5},
                new Items{id=3,name="cpu fan",description="nothing",price=555.5},
            };

            Iactivityrepo.Setup(x => x.GetItems()).Returns(Task.FromResult(ListItems));

            InventoryController controller = new InventoryController(Iactivityrepo.Object, logger.Object);


            var result = await controller.GetAllItems();

            var okResult = result as OkObjectResult;

            // assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Calling_GetAllItems_Should_Return_ItemsNullList()
        {
            var logger = new Mock<ILogger<InventoryController>>();
            var Iactivityrepo = new Mock<IActivityRepository>();

            List<Items> ListItems = new List<Items>();

            Iactivityrepo.Setup(x => x.GetItems()).Returns(Task.FromResult(ListItems));

            InventoryController controller = new InventoryController(Iactivityrepo.Object, logger.Object);


            var result = await controller.GetAllItems();

            var notFoundObjectResult = result as NotFoundObjectResult;

            // assert
            Assert.Equal(404, notFoundObjectResult.StatusCode);
        }

        [Fact]
        public async Task Calling_AddItems_Should_Return_OkObject()
        {
            var logger = new Mock<ILogger<InventoryController>>();
            var Iactivityrepo = new Mock<IActivityRepository>();

            Items item = new Items { id = 1, name = "mouse", description = "nothing", price = 555.5 };

            Iactivityrepo.Setup(x => x.AddItems(item)).Returns(Task.FromResult(1));

            InventoryController controller = new InventoryController(Iactivityrepo.Object, logger.Object);


            var result = await controller.AddItem(item);

            var okResult = result as OkObjectResult;

            // assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Calling_AddItems_Should_Return_BadObject()
        {
            var logger = new Mock<ILogger<InventoryController>>();
            var Iactivityrepo = new Mock<IActivityRepository>();

            Items item = new Items { id = 1, name = "mouse", description = "nothing", price = 555.5 };

            Iactivityrepo.Setup(x => x.AddItems(item)).Returns(Task.FromResult(0));

            InventoryController controller = new InventoryController(Iactivityrepo.Object, logger.Object);


            var result = await controller.AddItem(item);

            var badResult = result as BadRequestObjectResult;

            // assert
            Assert.NotNull(badResult);
            Assert.Equal(400, badResult.StatusCode);
        }


        [Fact]
        public async Task Calling_UpdateItems_Should_Return_OkResult()
        {
            var logger = new Mock<ILogger<InventoryController>>();
            var Iactivityrepo = new Mock<IActivityRepository>();

            Items item = new Items { id = 1, name = "mouse", description = "nothing", price = 555.5 };

            Iactivityrepo.Setup(x => x.UpdateItem(item)).Returns(Task.FromResult(1));

            InventoryController controller = new InventoryController(Iactivityrepo.Object, logger.Object);


            var result = await controller.UpdateItem(item);

            var okResult = result as OkObjectResult;

            // assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }


        [Fact]
        public async Task Calling_UpdateItems_Should_Return_BadRequest()
        {
            var logger = new Mock<ILogger<InventoryController>>();
            var Iactivityrepo = new Mock<IActivityRepository>();

            Items item = new Items { id = 1, name = "mouse", description = "nothing", price = 555.5 };

            Iactivityrepo.Setup(x => x.UpdateItem(item)).Returns(Task.FromResult(0));

            InventoryController controller = new InventoryController(Iactivityrepo.Object, logger.Object);


            var result = await controller.UpdateItem(item);

            var badResult = result as BadRequestObjectResult;

            // assert
            Assert.NotNull(badResult);
            Assert.Equal(400, badResult.StatusCode);
        }

        [Fact]
        public async Task Calling_DeleteItems_Should_Return_NoContent()
        {
            var logger = new Mock<ILogger<InventoryController>>();
            var Iactivityrepo = new Mock<IActivityRepository>();


            Iactivityrepo.Setup(x => x.DeleteItem(1)).Returns(Task.FromResult(1));

            InventoryController controller = new InventoryController(Iactivityrepo.Object, logger.Object);


            var result = await controller.DeleteItem(1);

            var noContent = result as NoContentResult;

            // assert

            Assert.Equal(204, noContent.StatusCode);
        }

        [Fact]
        public async Task Calling_DeleteItems_Should_Return_BadResult()
        {
            var logger = new Mock<ILogger<InventoryController>>();
            var Iactivityrepo = new Mock<IActivityRepository>();

            Iactivityrepo.Setup(x => x.DeleteItem(1)).Returns(Task.FromResult(0));

            InventoryController controller = new InventoryController(Iactivityrepo.Object, logger.Object);


            var result = await controller.DeleteItem(1);

            var badResult = result as BadRequestObjectResult;

            // assert
            Assert.NotNull(badResult);
            Assert.Equal(400, badResult.StatusCode);
        }
    }
}
