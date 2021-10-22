using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopBridge.Models;
using ShopBridge.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : Controller
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ILogger _logger;

        public InventoryController(IActivityRepository activityRepository, ILogger<InventoryController> logger)
        {
            _activityRepository = activityRepository;
            _logger = logger;
        }

        // GET: InventoryController
        [HttpGet("GetAllItems")]
        public async Task<ActionResult> GetAllItems()
        {
            List<Items> items = new List<Items>();
            try
            {
                items = await _activityRepository.GetItems();
                if (items.Count > 0)
                {
                    return Ok(items);
                }
                else
                {
                    return NotFound("Items not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Error!!!");
            }
        }

        // GET: InventoryController/Create
        [HttpPost("AddItem")]
        public async Task<ActionResult> AddItem([FromBody] Items item)
        {
            try
            {
                int IsItemAdded = await _activityRepository.AddItems(item);
                if (IsItemAdded == 1)
                {
                    return Ok("Items Added Successfully");
                }
                return BadRequest("Invalid Entry");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Error!!!");
            }
        }

        [HttpPut("UpdateItem")]
        public async Task<ActionResult> UpdateItem([Required][FromBody] Items item)
        {
            try
            {
                int IsItemUpdated= await _activityRepository.UpdateItem(item);
                if (IsItemUpdated == 1)
                {
                    return Ok(item);
                }
                else
                {
                    return BadRequest("Invalid Entry");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Error!!!");
            }
        }

        // GET: InventoryController/Delete/5
        [HttpDelete("DeleteItem")]
        public async Task<ActionResult> DeleteItem([Required] int id)
        {
            try
            {
                int IsItemDeleted = await _activityRepository.DeleteItem(id);
                if (IsItemDeleted == 1)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest("Invalid Entry");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Error!!!");
            }
        }
    }
}
