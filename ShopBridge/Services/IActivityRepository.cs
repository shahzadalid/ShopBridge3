using ShopBridge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Services
{
    public interface IActivityRepository
    {
        public Task<int> AddItems(Items item);
        public Task<int> UpdateItem(Items item);

        public Task<int> DeleteItem(int id);

        public Task<List<Items>> GetItems();
    }
}
