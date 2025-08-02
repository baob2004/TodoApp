using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Entities;
using api.Interfaces;
using api.Mappers;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _context;
        public TodoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Todo?> CreateAsync(Todo todo)
        {
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();

            return todo;
        }

        public async Task<Todo?> DeleteAsync(int id)
        {
            var existingTodo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
            if (existingTodo == null)
            {
                return null;
            }
            _context.Todos.Remove(existingTodo);
            await _context.SaveChangesAsync();
            return existingTodo;
        }

        public async Task<List<Todo>> GetAllAsync()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<Todo?> GetByIdAsync(int id)
        {
            var existingTodo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
            return existingTodo == null ? null : existingTodo;
        }

        public async Task<bool> TodoExists(int id)
        {
            return await _context.Todos.AnyAsync(t => t.Id == id);
        }

        public async Task<Todo?> UpdateAsync(int id, UpdateTodoDto updateTodoDto)
        {
            var existingTodo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
            if (existingTodo == null)
            {
                return null;
            }


            existingTodo.Title = updateTodoDto.Title;
            existingTodo.Content = updateTodoDto.Content;
            existingTodo.isCompleted = updateTodoDto.isCompleted;
            existingTodo.CompletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingTodo;
        }
    }
}