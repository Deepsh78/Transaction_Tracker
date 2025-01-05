using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionTracker.JsonHandler;
using TransactionTracker.Model;

namespace TransactionTracker.Services
{
    public class InflowService: InflowBase, IInflowService
    {
        public async Task<List<Inflow>> GetAllInflowsAsync()
        {
            return await LoadInflowsAsync();
        }

        public async Task AddInflowAsync(Inflow inflow)
        {
            var inflows = await LoadInflowsAsync();
            inflows.Add(inflow);
            await SaveInflowsAsync(inflows);
        }
    }
}
