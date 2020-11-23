using Library.Core.Models;
using Library.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class InMemoryNewspaperRepository : INewspaperRepository
    {
        private static ISet<Newspaper> _newspapers = new HashSet<Newspaper>();
        public async Task AddAsync(Newspaper newspaper)
        {
            _newspapers.Add(newspaper);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Newspaper>> BrowseAsync(string title = "")
        {
            var newspapers = _newspapers.Where(x => x.Title.ToLowerInvariant().Contains(title.ToLowerInvariant()));
            return await Task.FromResult(newspapers);
        }

        public async Task DeleteAsync(Guid id)
        {
            var newspaper = _newspapers.SingleOrDefault(x => x.Id == id);
            if(newspaper == null)
            {
                return;
            }
            _newspapers.Remove(newspaper);
            await Task.CompletedTask;
        }

        public async Task<Newspaper> GetAsync(Guid id)
            => await Task.FromResult(_newspapers.SingleOrDefault(x => x.Id == id));
        

        public async Task UpdateAsync(Newspaper newspaper)
        {
            await Task.CompletedTask;
        }
    }
}
