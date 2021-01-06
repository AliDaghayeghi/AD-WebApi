using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADWebApi.Models.Contexts;
using ADWebApi.Models.Entities;

namespace ADWebApi.Models.Services
{
    public class TodoRepository
    {
        private readonly DataBaseContext _context;
        public TodoRepository(DataBaseContext context)
        {
            _context = context;
        }

        public List<TodoDto> GetAll()
        {
            return _context.ToDos.Select(p => new TodoDto
            {
                Id = p.Id,
                InsertTime = p.InsertTime,
                IsRemoved = p.IsRemoved,
                Text = p.Text
            }).ToList();
        }
        public TodoDto Get(int Id)
        {
            var todo = _context.ToDos.SingleOrDefault(p => p.Id == Id);
            return new TodoDto()
            {
                Id = todo.Id,
                InsertTime = todo.InsertTime,
                IsRemoved = todo.IsRemoved,
                Text = todo.Text,
            };
        }

        public AddToDoDto Add(AddToDoDto todo)
        {
            ToDo newToDo = new ToDo()
            {
                Id = todo.Todo.Id,
                InsertTime = DateTime.Now,
                IsRemoved = false,
                Text = todo.Todo.Text,

            };
            foreach (var item in todo.Categories)
            {
                var category = _context.Categories.SingleOrDefault(p => p.Id == item);
                newToDo.Categories.Add(category);
            }
            _context.ToDos.Add(newToDo);
            _context.SaveChanges();
            return new AddToDoDto
            {
                Todo = new TodoDto
                {
                    Id = newToDo.Id,
                    InsertTime = newToDo.InsertTime,
                    IsRemoved = newToDo.IsRemoved,
                    Text = newToDo.Text,
                }
                  ,
                Categories = todo.Categories
            };
        }

        public void Delete(int Id)
        {
            //_context.ToDos.Remove(new ToDo { Id = Id });
            var todo = _context.ToDos.Find(Id);
            todo.IsRemoved = true;
            _context.SaveChanges();
        }

        public bool Edit(EditToDoDto edit)
        {
            var todo = _context.ToDos.SingleOrDefault(p => p.Id == edit.Id);
            todo.Text = edit.Text;
            _context.SaveChanges();
            return true;
        }

    }


    public class TodoDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime InsertTime { get; set; }
        public bool IsRemoved { get; set; }
    }

    public class AddToDoDto
    {
        public TodoDto Todo { get; set; }
        public List<int> Categories { get; set; } = new List<int>();
    } 
    
    public class EditToDoDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<int> Categories { get; set; }
    }
}
