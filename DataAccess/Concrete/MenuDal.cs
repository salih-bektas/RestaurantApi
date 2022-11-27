using DataAccess.Abstract;
using DataAccess.Domain;
using DataAccess.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete;

public class MenuDal : Repository<Menu>, IMenuDal
{
    public MenuDal(DbContext context) : base(context) {}
}