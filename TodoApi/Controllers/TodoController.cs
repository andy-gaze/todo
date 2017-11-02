using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TodoApi.Interfaces;
using TodoApi.Models;

namespace TodoApi
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return _todoRepository.GetAll();
        }

        // GET: api/values
        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            TodoItem item = _todoRepository.Get(id);
            if (item == null)
                return NotFound();

            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (_todoRepository.Add(item) == null)
                return BadRequest();

            return new ObjectResult(item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TodoItem item)
        {
            if (item == null || item.Id != id)
                return BadRequest();

            if (_todoRepository.Update(item))
                return new NoContentResult();
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (_todoRepository.Delete(id))
                return new NoContentResult();
            else
                return NotFound();

        }
    }
}
