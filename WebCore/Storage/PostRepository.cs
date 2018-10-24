using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Models;

namespace WebCore.Storage
{
    public class PostRepository : IPostRepository
    {
        private readonly MongoContext _dbContext;

        public PostRepository(IOptions<Settings> options)
        {
            _dbContext = new MongoContext(options);
        }

        public async Task Add(Post post)
        {
            await _dbContext.Posts.InsertOneAsync(post);
        }

        public async Task<bool> Delete(string id)
        {
            var res = await _dbContext.Posts.DeleteOneAsync(Builders<Post>.Filter.Eq("Id", id));

            return res.IsAcknowledged && res.DeletedCount > 0;
        }

        public async Task<IEnumerable<Post>> Search(PostSearchParams postSearchParams)
        {
            //simplified search
            var query = _dbContext.Posts.Find(p => p.Text.Contains(postSearchParams.Content)
            || postSearchParams.Authors.Contains(p.AuthorId) 
            || postSearchParams.Tags.Any(t => p.Tags.Contains(t)));

            var res = _dbContext.Posts.Find(p => postSearchParams.Tags.Intersect(postSearchParams.Tags).Any());

            return await query.ToListAsync();
        }

        public async Task<Post> Get(string id)
        {
            return await _dbContext.Posts.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _dbContext.Posts.Find(_ => true).ToListAsync();
        }

        public async Task<bool> Update(string id, string content)
        {
            var filter = Builders<Post>.Filter.Eq(s => s.Id, id);
            var update = Builders<Post>.Update.Set(s => s.Text, content).CurrentDate(s => s.UpdatedDate);

            var res = await _dbContext.Posts.UpdateOneAsync(filter, update);

            return res.IsAcknowledged && res.ModifiedCount > 0;
        }
    }
}
