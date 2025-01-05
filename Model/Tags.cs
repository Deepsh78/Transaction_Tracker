using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionTracker.Model
{
    public class Tags
    {
        public Guid TagId { get; set; }
        public string TagName { get; set; }
        public string CustomTags { get; set; }
    }
}
