using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderGrain
{
    public class OrderState
    {
        public string ProductName { get; set; }
        public decimal Qty { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public interface IOrderGrain:IGrainWithGuidKey
    {
        Task Create(string name, int qty);
    }
    public class OrderGrain:Grain<OrderState>,IOrderGrain
    {
        public async Task Create(string name,int qty)
        {
            State.ProductName = name;
            State.Qty = qty;
            State.CreateTime = DateTime.Now;
            await WriteStateAsync();
        }
    }
}
