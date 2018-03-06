using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotNetCore2WebApi.Data;
using dotNetCore2WebApi.Entities;
using Microsoft.Extensions.Logging;
using dotNetCore2WebApi.Models.Posts;
using Microsoft.AspNetCore.Cors;

namespace dotNetCore2WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Posts")]
    [EnableCors("allowLocalClientServer")]
    public class PostsController : Controller
    {
        private readonly BlogDbContext _context;
        private readonly ILogger _logger;

        public PostsController(BlogDbContext context,
                ILogger<PostsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Posts
        [HttpGet]
        public IEnumerable<Post> GetPosts()
        {
            
            _logger.LogInformation("Ran GET: api/Posts");
            return _context.Posts;
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost([FromRoute] int id)
        {
            _logger.LogInformation($"Ran GET: api/Posts/{id}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);

            if (post == null)
            {
                return NotFound();
            }
            
            return Ok(post);
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost([FromRoute] int id, [FromBody] PostRequestModel updatePost)
        {
            _logger.LogInformation($"Ran PUT: api/Posts/{id}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            post.Title = updatePost.Title;
            post.Summary = updatePost.Summary;
            post.Content = updatePost.Content;
            post.IsPublished = updatePost.IsPublished;
            post.UpdateDate = DateTime.Now;

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Posts
        [HttpPost]
        public async Task<IActionResult> PostPost([FromBody] PostRequestModel createPost)
        {
            _logger.LogInformation($"Ran POST: api/Posts");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var post = new Post{
                Title = createPost.Title,
                Summary = createPost.Summary,
                Content = createPost.Content,
                IsPublished = createPost.IsPublished,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                UserId = 1 // TODO: Change when User functionality is added \
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {
            _logger.LogInformation($"Ran DELETE: api/Posts/{id}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok(post);
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}