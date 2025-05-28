using Microsoft.AspNetCore.Mvc;
using TodoListApp.Models;

namespace TodoListApp.Controllers
{
    public class TodoController : Controller
    {
        private static List<TodoItem> todoList = new List<TodoItem>();
        public IActionResult Index()
        {
            return View(todoList);
        }

        [HttpPost]
        public IActionResult Add(string title)
        {
            if (!string.IsNullOrWhiteSpace(title)){
                var newTodo = new TodoItem
                {
                    Id = todoList.Count + 1,
                    Title = title,
                    IsDone = false
                };
                todoList.Add(newTodo);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = todoList.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                todoList.Remove(item);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var item = todoList.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);// 對應到View內的Edit.cshtml
        }

        [HttpPost]
        public IActionResult Edit(TodoItem updatedItem)
        {
            var item = todoList.FirstOrDefault(t => t.Id == updatedItem.Id);
            if(item != null)
            {
                item.Title = updatedItem.Title;
            }
            return RedirectToAction("Index");
        }
    }
}
