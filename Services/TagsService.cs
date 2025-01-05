using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionTracker.JsonHandler;
using TransactionTracker.Model;

namespace TransactionTracker.Services
{
    public class TagsService : TagsBase, ITagsService
    {
        public async Task<List<Tags>> GetTagsAsync()
        {
            return await Task.Run(() => new List<Tags>
            {
                new Tags { TagId = Guid.NewGuid(), TagName = "Yearly" },
                new Tags { TagId = Guid.NewGuid(), TagName = "Monthly" },
                new Tags { TagId = Guid.NewGuid(), TagName = "Food" },
                new Tags { TagId = Guid.NewGuid(), TagName = "Drinks" },
                new Tags { TagId = Guid.NewGuid(), TagName = "Clothes" },
                new Tags { TagId = Guid.NewGuid(), TagName = "Gadgets" },
                new Tags { TagId = Guid.NewGuid(), TagName = "Miscellaneous" },
                new Tags { TagId = Guid.NewGuid(), TagName = "Fuel" },
                new Tags { TagId = Guid.NewGuid(), TagName = "Rent" },
                new Tags { TagId = Guid.NewGuid(), TagName = "EMI" },
                new Tags { TagId = Guid.NewGuid(), TagName = "Party" },
            });
        }
    

        public async Task AddTagAsync(Tags tag)
        {
            var tags = LoadTags();  // Load tags synchronously
            tags.Add(tag);  // Add the new tag

            // Save the updated tags list to the file
            await Task.Run(() => SaveTags(tags));
        }
    }

}
