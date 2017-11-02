using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Interfaces
{
    public interface ITodoRepository
    {
        TodoItem Add(TodoItem item);
        TodoItem Get(long id);
        bool Update(TodoItem item);
        IEnumerable<TodoItem> GetAll();
        bool Delete(long id);
    }
}
