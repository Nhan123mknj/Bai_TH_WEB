using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using startup.Models;
using startup.Utilities;

namespace startup.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly DataContext _context;

        public PostController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            // Tạo danh sách menu cho dropdown
            var mnList = _context.Menus
                .Select(m => new SelectListItem
                {
                    Text = m.MenuName,
                    Value = m.MenuID.ToString()
                })
                .ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.mnList = mnList;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Add(post);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        public IActionResult Details(long id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        public IActionResult Edit(long id)
        {
            var post = _context.Posts.Find(id);

            if (post == null)
            {
                return NotFound();
            }

            // Tương tự như trong Create, bạn cần lấy danh sách menu và truyền vào ViewBag để hiển thị dropdown cho MenuID
            var mnList = _context.Menus
                .Select(m => new SelectListItem
                {
                    Text = m.MenuName,
                    Value = m.MenuID.ToString()
                })
                .ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "----Select----",
                Value = string.Empty
            });
            ViewBag.mnList = mnList;

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                post.CreatedDate = DateTime.Now; // Gán giá trị CreatedDate

                _context.Update(post);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        public IActionResult Delete(long id)
        {
            var post = _context.Posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var post = _context.Posts.Find(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var posts = _context.Posts.ToList();

            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");

            return View(posts);
        }
    }
}