using Microsoft.AspNetCore.Mvc;
using TodoListApp.Data;
using TodoListApp.Models;

namespace TodoListApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var items = _context.TodoItems.ToList();// 從資料庫讀取
            return View(items);
        }

        // 新增任務
        [HttpPost]
        public IActionResult Add(string title)
        {
            if (!string.IsNullOrWhiteSpace(title)){
                var newTodo = new TodoItem
                {
                    Title = title,
                    IsDone = false
                };

                _context.TodoItems.Add(newTodo);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // 刪除任務
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = _context.TodoItems.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // 進入編輯畫面
        public IActionResult Edit(int id)
        {
            var item = _context.TodoItems.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);// 對應到Views內Todo內的Edit.cshtml
        }

        //提交編輯內容
        [HttpPost]
        public IActionResult Edit(TodoItem updatedItem)
        {
            var item = _context.TodoItems.FirstOrDefault(t => t.Id == updatedItem.Id);
            if(item != null)
            {
                item.Title = updatedItem.Title;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
