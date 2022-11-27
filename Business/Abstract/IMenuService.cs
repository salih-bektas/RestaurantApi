using DataAccess.Domain;

namespace Business.Abstract;

public interface IMenuService
{
    Menu CreateMenu(Menu menu);
    IEnumerable<Menu> GetMenus();
    int DeleteMenu(Menu menu);
    Menu? GetMenuById(Guid id);
    IEnumerable<Menu> GetMenuByDate(DateTime date);
    Menu Update(Menu menu);
}