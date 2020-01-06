using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderGrain;
using Orleans;

namespace OrderDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IGrainFactory factory;
        public OrderController(IGrainFactory grainFactory)
        {
            factory = grainFactory;
        }

        [HttpGet]
        [Route("SetStock")]
        public async Task<int> SetStock(int stock)
        {
            await factory.GetGrain<IProductGrain>(Guid.Empty).SetStock(stock);
            return stock;
        }


        [HttpGet]
        [Route("Create")]
        public async Task<bool> Create(int qty)
        {
            var rst= await factory.GetGrain<IProductGrain>(Guid.Empty).DecAsync(qty);
            if (rst)
            {
                await factory.GetGrain<IOrderGrain>(Guid.NewGuid()).Create("测试商品", qty);
            }
            return rst;
        }
    }
}
