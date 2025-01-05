using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionTracker.Model;

namespace TransactionTracker.Services
{
    public interface IInflowService
    {
        Task<List<Inflow>> GetAllInflowsAsync();
        Task AddInflowAsync(Inflow inflow);
    }
}
