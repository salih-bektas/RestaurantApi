using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Domain;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete;

public class SectionManager : ISectionService
{
    private readonly ISectionDal _sectionDal;

    public SectionManager(ISectionDal sectionDal)
    {
        _sectionDal = sectionDal;
    }
    
    public Section CreateSection(Section section)
    {
        var addedSection = _sectionDal.Add(section);
        return addedSection;
    }

    public IEnumerable<Section> GetSections()
    {
        var sections = _sectionDal.Table.Include(t => t.Products);
        return sections;
    }

    public int DeleteSection(Section section)
    {
        return _sectionDal.Delete(section);
    }

    public Section? GetSectionById(Guid id)
    {
        var section = _sectionDal.Table.Where(t => t.Id == id)
            .Include(t => t.Products).FirstOrDefault();
        return section;
    }

    public List<Section> GetSectionByMenuId(Guid menuId)
    {
        var sections = _sectionDal.Table.Where(t => t.MenuId == menuId).Include(a => a.Products).ToList();
        return sections;
    }

    public Section Update(Section section)
    {
        return _sectionDal.Update(section);
    }
}