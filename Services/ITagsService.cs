using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionTracker.Model;

namespace TransactionTracker.Services
{
    public interface ITagsService
    {
        Task<List<Tags>> GetTagsAsync();
        Task AddTagAsync(Tags tag);
    }
}
