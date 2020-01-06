using Dapper;
using Orleans;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace OrderGrain
{
    public class ProductState
    {
        public int Stock { get; set; }
    }
    public interface IProductGrain: IGrainWithGuidKey
    {
        Task<bool> DecAsync(int qty);
        Task SetStock(int qty);
    }
    public class ProductGrain: Grain<ProductState>, IProductGrain
    {
      
        public async Task SetStock(int qty)
        {
            State.Stock = qty;
            await WriteStateAsync();
        }
        public async Task<bool> DecAsync(int qty)
        {
            if (qty > State.Stock)
            {
                return await Task.FromResult(false);
            }
            State.Stock -= qty;

           
            await WriteStateAsync();

            return true;
        }
    }
}
