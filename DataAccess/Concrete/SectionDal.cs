using DataAccess.Abstract;
using DataAccess.Domain;
using DataAccess.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete;

public class SectionDal : Repository<Section>, ISectionDal
{
    public SectionDal(DbContext context) : base(context) {}
}