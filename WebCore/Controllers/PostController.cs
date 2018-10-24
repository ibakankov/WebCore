using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCore.Models;
using WebCore.Storage;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebCore.Controllers
{
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // GET: api/<controller>
        /// <summary>
        /// Get all posts
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<Post>> Get()
        {
            return await _postRepository.GetAll();
        }

        // GET api/<controller>/5
        /// <summary>
        /// Get post by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<Post> Get(string id)
        {
            return await _postRepository.Get(id);
        }

        // POST api/<controller>
        /// <summary>
        /// Add new post
        /// </summary>
        [HttpPost]
        public async Task<string> Post([FromBody]PostEditModel post)
        {
            var p = new Post()
            {
                Id = Guid.NewGuid().ToString(),
                Text = post.Text,
                AuthorId = post.AuthorId,
                Tags = post.Tags ?? Array.Empty<string>()
            };
            await _postRepository.Add(p);

            return p.Id;
        }

        // PUT api/<controller>/5
        /// <summary>
        /// Update post
        /// </summary>
        [HttpPut("{id}")]
        public async Task Put(string id, [FromBody]string value)
        {
            await _postRepository.Update(id, value);
        }

        // DELETE api/<controller>/5
        /// <summary>
        /// Delete post
        /// </summary>
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _postRepository.Delete(id);
        }

        /// <summary>
        /// Search post on any match
        /// </summary>
        [HttpPost("search")]
        public async Task<IEnumerable<Post>> Search([FromBody]PostSearchParams postSearchParams)
        {
            return await _postRepository.Search(postSearchParams);
        }
    }
}
