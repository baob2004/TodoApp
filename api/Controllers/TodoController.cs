using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/todo")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _todoRepo;
        public TodoController(ITodoRepository todoRepo)
        {
            _todoRepo = todoRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var todos = await _todoRepo.GetAllAsync();

            var todoDto = todos.Select(t => t.ToTodoDto());
            return Ok(todoDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var existingTodo = await _todoRepo.GetByIdAsync(id);
            return existingTodo == null ? NotFound() : Ok(existingTodo.ToTodoDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTodoDto createTodoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var todo = createTodoDto.ToTodoFromCreate();

            await _todoRepo.CreateAsync(todo);

            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo.ToTodoDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTodoDto updateTodoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingTodo = await _todoRepo.UpdateAsync(id, updateTodoDto);
            if (existingTodo == null)
            {
                return NotFound();
            }

            return Ok(existingTodo.ToTodoDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var existingTodo = await _todoRepo.DeleteAsync(id);
            if (existingTodo == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}