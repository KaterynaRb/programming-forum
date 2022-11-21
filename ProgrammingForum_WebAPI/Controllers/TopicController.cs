using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProgrammingForum_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TopicController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<TopicController>
        [HttpGet]
        public async Task<ActionResult<IList<Topic>>> Get()
        {
            //return _context.Topics.Include(topic => topic.Posts).ThenInclude(post => post.Author);
            var topics = await _context.Topics/*.Include(topic => topic.Posts)*/.ToListAsync();
            if (topics.Count == 0)
            {
                return NotFound();
            }

            return Ok(topics);
        }

        // GET api/<TopicController>/5
        [HttpGet("{id}")]
        public ActionResult<Topic> Get(int id)
        {
            var topic =  _context.Topics.Include(topic => topic.Posts).Where(topic => topic.Id == id).First(); //.FindAsync(id);
            if (topic == null)
            {
                return NotFound();
            }

            return Ok(topic);
        }

        // POST api/<TopicController>
        [HttpPost]
        public async Task<ActionResult<Topic>> Post([FromBody] Topic topic)
        {
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = topic.Id }, topic);
        }

        // PUT api/<TopicController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Topic topic)
        {
            if (id != topic.Id)
            {
                return BadRequest();
            }

            _context.Entry(topic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _context.Topics.FindAsync(id) == null)
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

        // DELETE api/<TopicController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var topic = await _context.Topics.FindAsync(id);
            if (topic == null)
            {
                return NotFound();
            }

            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
