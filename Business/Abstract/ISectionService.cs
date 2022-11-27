using DataAccess.Domain;

namespace Business.Abstract;

public interface ISectionService
{
    Section CreateSection(Section section);
    IEnumerable<Section> GetSections();
    int DeleteSection(Section section);
    Section? GetSectionById(Guid id);
    List<Section> GetSectionByMenuId(Guid menuId);
    Section Update(Section section);
}