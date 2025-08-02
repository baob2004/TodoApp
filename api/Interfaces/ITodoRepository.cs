using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Entities;

namespace api.Interfaces
{
    public interface ITodoRepository
    {
        public Task<List<Todo>> GetAllAsync();
        public Task<Todo?> GetByIdAsync(int id);
        public Task<Todo?> CreateAsync(Todo todo);
        public Task<Todo?> UpdateAsync(int id, UpdateTodoDto updateTodoDto);
        public Task<Todo?> DeleteAsync(int id);
        public Task<bool> TodoExists(int id);
    }
}