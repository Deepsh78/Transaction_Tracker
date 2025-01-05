using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionTracker.Model;

namespace TransactionTracker.Services
{
    public interface IDebtService
    {
        Task<List<Debt>> GetPendingDebtsAsync();
        //Task ClearDebtFromInflowAsync(Transaction creditTransaction);
        
    }
}
