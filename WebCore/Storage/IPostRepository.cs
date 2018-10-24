using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Models;

namespace WebCore.Storage
{
    public interface IPostRepository
    {
        Task Add(Post post);
        Task<Post> Get(string id);
        Task<IEnumerable<Post>> GetAll();
        Task<bool> Update(string id, string content);
        Task<bool> Delete(string id);
        Task<IEnumerable<Post>> Search(PostSearchParams postSearchParams);
    }
}
