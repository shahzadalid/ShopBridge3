using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Services
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly ILogger _logger;
        private readonly AppDbContext _context;

        public ActivityRepository(ILogger<ActivityRepository> logger,AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<int> AddItems(Items item)
        {
            int IsItemAdded = 0;
            try
            {
                _context.Item.Add(item);
               IsItemAdded= await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured ",ex);
            }
            return IsItemAdded;

        }

        public async Task<int> DeleteItem(int id)
        {
            int IsItemDeleted = 0;
            try
            {
                var item = _context.Item.FirstOrDefault(x => x.id == id);
                _context.Item.Remove(item);
               IsItemDeleted= await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError("Error Occured ", ex);
            }
            return IsItemDeleted;
        }

        public async Task<List<Items>> GetItems()
        {
            List<Items> items = new List<Items>();
            try
            {
                items =await _context.Item.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured ", ex);
            }
            return items;
        }

        public async Task<int> UpdateItem(Items item)
        {
            int IsItemUpdated = 0;
            try
            {
                _context.Item.Update(item);
                IsItemUpdated = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured ", ex);
            }
            return IsItemUpdated;
        }
    }
}
