using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using startup.Models;


namespace startup.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        private readonly DataContext _context;
        public MenuController(DataContext context)
        {
            _context = context;
        }
        //delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Menu.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deleMenu = _context.Menu.Find(id);
            if (deleMenu == null)
            {
                return NotFound();
            }
            _context.Menu.Remove(deleMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        //end delete

        //Start Create
        // GET: Hiển thị form để tạo menu mới
        public IActionResult Create()
        {
            var mnList = (from m in _context.Menu
                          select new SelectListItem()
                          {
                              Text = m.MenuName,
                              Value = m.MenuID.ToString(),
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = "0"
            });
            ViewBag.mnList = mnList;
            return View();

        }

        // POST: Xử lý việc tạo menu mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Menu mn)
        {
            if (ModelState.IsValid)
            {
                // Lưu menu vào cơ sở dữ liệu (ví dụ: sử dụng Entity Framework)
                _context.Menu.Add(mn);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Chuyển hướng sau khi thêm thành công
            }
            return View(mn);
        }
        //end create

        //Start Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.Menu.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            var mnList = (from m in _context.Menu
                          select new SelectListItem()
                          {
                              Text = m.MenuName,
                              Value = m.MenuID.ToString(),
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.mnList = mnList;
            return View(mn);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Menu mn)
        {
            if (ModelState.IsValid)
            {
                // Cập nhật dữ liệu menu trong cơ sở dữ liệu (sử dụng Entity Framework hoặc phương thức truy cập dữ liệu của bạn)
                _context.Menu.Update(mn);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Chuyển hướng đến trang danh sách menu
            }
            return View(mn); // Hiển thị biểu mẫu chỉnh sửa với lỗi kiểm tra
        }

        //End Edit
        public IActionResult Index()
        {
            var mnList = _context.Menu.OrderBy(m => m.MenuID).ToList();

            
            return View(mnList);
        }
    }
}