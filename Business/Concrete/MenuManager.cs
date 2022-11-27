using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete;

public class MenuManager : IMenuService
{
    private readonly IMenuDal _menuDal;

    public MenuManager(IMenuDal menuDal)
    {
        _menuDal = menuDal;
    }
    
    public Menu CreateMenu(Menu menu)
    {
        var addedMenu = _menuDal.Add(menu);
        return addedMenu;
    }

    public IEnumerable<Menu> GetMenus()
    {
        var menus = _menuDal.Table.Include(t => t.Sections).ThenInclude(a => a.Products);
        return menus;
    }

    public int DeleteMenu(Menu menu)
    {
        return _menuDal.Delete(menu);
    }

    public Menu? GetMenuById(Guid id)
    {
        var menu = _menuDal.Table.Where(t => t.Id == id)
            .Include(t => t.Sections)
            .ThenInclude(t => t.Products)
            .FirstOrDefault();
        return menu;
    }

    public IEnumerable<Menu> GetMenuByDate(DateTime date)
    {
        var menus = _menuDal.Table.Where(t => t.Date == date.Date)
            .Include(t => t.Sections)
            .ThenInclude(t => t.Products).ToList();
        return menus;
    }

    public Menu Update(Menu menu)
    {
        return _menuDal.Update(menu);
    }
}