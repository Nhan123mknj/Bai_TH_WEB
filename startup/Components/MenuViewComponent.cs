using Microsoft.AspNetCore.Mvc;
using startup.Models;
namespace startup.Components

{
    [ViewComponent(Name = "MenuView")]
    public class MenuViewComponent:ViewComponent
    {
        private DataContext _Context;
        public MenuViewComponent(DataContext context)
        {
            _Context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofmenu =(from m in _Context.Menu
                             where(m.IsActive==true)&&(m.Position==1)
                             select m).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default",listofmenu));
        }
    }
}
