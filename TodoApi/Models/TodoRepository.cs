using System.Collections.Generic;
using System.Linq;
using TodoApi.Interfaces;

namespace TodoApi.Models
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _dbContext;

        public TodoRepository(TodoContext dbContext)
        {
            _dbContext = dbContext;
            if (_dbContext.TodoItems.Count() == 0)
            {
                _dbContext.TodoItems.Add(new TodoItem { Name = "Items1" });
                _dbContext.SaveChanges();
            }
        }

        public TodoItem Add(TodoItem item)
        {
            if (item == null)
                return null;

            _dbContext.TodoItems.Add(item);
            _dbContext.SaveChanges();

            return item;
        }

        public TodoItem Get(long id)
        {
            return GetById(id);
        }

        public bool Update(TodoItem item)
        {
            if (item == null)
                return false;

            TodoItem todo = GetById(item.Id);
            if (todo == null)
                return false;

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _dbContext.TodoItems.Update(todo);
            _dbContext.SaveChanges();

            return true;
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return _dbContext.TodoItems.ToList();
        }

        public bool Delete(long id)
        {
            TodoItem todo = GetById(id);
            if (todo == null)
                return false;

            _dbContext.TodoItems.Remove(todo);
            _dbContext.SaveChanges();

            return false;
        }

        private TodoItem GetById(long id)
        {
            return _dbContext.TodoItems.FirstOrDefault(t => t.Id == id);
        }
    }
}
