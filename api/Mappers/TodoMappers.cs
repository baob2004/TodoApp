using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Entities;

namespace api.Mappers
{
    public static class TodoMappers
    {
        public static TodoDto ToTodoDto(this Todo todo)
        {
            return new TodoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Content = todo.Content,
                isCompleted = todo.isCompleted,
                CompletedAt = todo.CompletedAt,
                CreatedAt = todo.CreatedAt
            };
        }

        public static Todo ToTodoFromCreate(this CreateTodoDto todo)
        {
            return new Todo
            {
                Title = todo.Title,
                Content = todo.Content,
            };
        }
    }
}