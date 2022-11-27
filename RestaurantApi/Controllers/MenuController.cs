using AutoMapper;
using Business.Abstract;
using DataAccess.Domain;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Models;

namespace RestaurantApi.Controllers;

[ApiController]
[Route("api/menus")]
public class MenuController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMenuService _menuService;
    private readonly IProductService _productService;
    private readonly ISectionService _sectionService;

    public MenuController(IMapper mapper,
        IMenuService menuService,
        IProductService productService,
        ISectionService sectionManager)
    {
        _mapper = mapper;
        _menuService = menuService;
        _productService = productService;
        _sectionService = sectionManager;
    }
    
    //Menu operations
    
    /// <summary>
    /// Get Menu with Sections and Products 
    /// </summary>
    /// <returns>Menu</returns>
    [HttpGet]
    public IEnumerable<MenuViewDto> Get()
    {
        return _menuService.GetMenus().Select(c => _mapper.Map<MenuViewDto>(c));
    }
    
    /// <summary>
    /// Get list of Menus for a date
    /// </summary>
    /// <param name="date"></param>
    /// <returns>List of Menu</returns>
    [HttpGet("{date:DateTime}")]
    public IEnumerable<MenuViewDto> Get(DateTime date)
    {
        return _menuService.GetMenus().Select(c => _mapper.Map<MenuViewDto>(c));
    }

    /// <summary>
    /// Get Menu by an Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>List of Menu</returns>
    [HttpGet("{id:Guid}")]
    public ActionResult Get(Guid id)
    {
        var menu = _menuService.GetMenuById(id);
        if (menu == null)
        {
            return NotFound($"Menu with Id {id} not found");
        }
        return Ok(_mapper.Map<MenuViewDto>(menu));
    }
    
    /// <summary>
    /// Create a Menu with selected Sections
    /// </summary>
    /// <param name="menuDto"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult CreateMenu(MenuDto menuDto)
    {
        var menu = new Menu
        {
            Date = menuDto.Date
        };
        foreach (var sectionId in menuDto.Sections)
        {
            var section = _sectionService.GetSectionById(sectionId);
            if (section == null)
            {
                return NotFound($"Section with Id {sectionId} not found");
            }
            menu.Sections.Add(section);
        }

        var createdMenu = _menuService.CreateMenu(menu);
        return Ok(_mapper.Map<MenuViewDto>(createdMenu));
    }
    
    /// <summary>
    /// Deletes menu with given Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public ActionResult Delete(Guid id)
    {
        var menu = _menuService.GetMenuById(id);
        if (menu == null)
        {
            return NotFound($"Product with Id = {id} not found");
        }

        _menuService.DeleteMenu(menu);
        return Ok();
    }
    
    /// <summary>
    /// Update Menu
    /// </summary>
    /// <param name="id"></param>
    /// <param name="menuDto"></param>
    /// <returns></returns>
    [HttpPut("{id:Guid}")]
    public ActionResult UpdateMenu(Guid id, MenuDto menuDto)
    {
        try
        {
            var menuToUpdate = _menuService.GetMenuById(id);

            if (menuToUpdate == null)
                return NotFound($"Menu with Id = {id} not found");

            var sectionList = new List<Section>();
            foreach (var sectionId in menuDto.Sections)
            {
                var section = _sectionService.GetSectionById(sectionId);
                if (section == null)
                {
                    return NotFound($"Product with Id = {id} not found");
                }
                sectionList.Add(section);
            }

            menuToUpdate.Date = menuDto.Date;
            menuToUpdate.Sections.Clear();
            menuToUpdate.Sections = sectionList;

            _menuService.Update(menuToUpdate);
            
            return Ok(_mapper.Map<MenuViewDto>(menuToUpdate));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }

    
    //section operations
    
    /// <summary>
    /// Creates a Section. Section has to be related to a Menu 
    /// </summary>
    /// <param name="menuId">Related Menu Id</param>
    /// <param name="sectionDto">Section parameters</param>
    /// <returns></returns>
    [HttpPost("{menuId:guid}/sections")]
    public ActionResult CreateSection(Guid menuId, SectionCreateDto sectionDto)
    {
        var section = new Section
        {
            Title = sectionDto.Title,
            MenuId = menuId
        };
        foreach (var productId in sectionDto.Products)
        {
            var product = _productService.GetProductById(productId);
            if (product == null)
            {
                return NotFound($"Product with Id {productId} not found");
            }
            section.Products.Add(product);
        }
        var result = _sectionService.CreateSection(section);
        return Ok();
    }
    
    /// <summary>
    /// Returns list of Sections for a Menu
    /// </summary>
    /// <param name="menuId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{menuId:guid}/sections")]
    public IEnumerable<SectionViewDto> Sections(Guid menuId)
    {
        var result = _sectionService.GetSectionByMenuId(menuId).Select(c => _mapper.Map<SectionViewDto>(c));
        return result;
    }
    
    /// <summary>
    /// Returns list of all Sections
    /// </summary>
    /// <returns>List of Sections</returns>
    [HttpGet]
    [Route("/sections")]
    public IEnumerable<SectionViewDto> Sections()
    {
        var result = _sectionService.GetSections().Select(c => _mapper.Map<SectionViewDto>(c));
        return result;
    }
    
    /// <summary>
    /// Updated Section - Products should be valid for a successful update
    /// </summary>
    /// <param name="id"></param>
    /// <param name="sectionUpdateDto"></param>
    /// <returns></returns>
    [HttpPut("sections/{id:Guid}")]
    public ActionResult UpdateSection(Guid id, SectionUpdateDto sectionUpdateDto)
    {
        try
        {
            var sectionToUpdate = _sectionService.GetSectionById(id);

            if (sectionToUpdate == null)
                return NotFound($"Section with Id = {id} not found");

            var productList = new List<Product>();
            foreach (var productId in sectionUpdateDto.Products)
            {
                var product = _productService.GetProductById(productId);
                if (product == null)
                {
                    return NotFound($"Product with Id = {id} not found");
                }
                productList.Add(product);
            }

            sectionToUpdate.Title = sectionUpdateDto.Title;
            sectionToUpdate.Products.Clear();
            sectionToUpdate.Products = productList;

            _sectionService.Update(sectionToUpdate);
            
            return Ok(_mapper.Map<SectionViewDto>(sectionToUpdate));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }
    
    
    //product operations
    
    /// <summary>
    /// Returns list of Products.
    /// </summary>
    [HttpGet]
    [Route("products")]
    public IEnumerable<ProductViewDto> Products()
    {
        return _productService.GetProducts().Select(c => _mapper.Map<ProductViewDto>(c));
    }
    
    /// <summary>
    /// Return Product with given id.
    /// </summary>
    [HttpGet]
    [Route("products/{id:Guid}")]
    public ProductViewDto Product(Guid id)
    {
        return _mapper.Map<ProductViewDto>(_productService.GetProductById(id));
    }
    
    /// <summary>
    /// Creates a Product.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST api/menu/product
    ///     {        
    ///       "price": 64.0,
    ///       "name": "Tartine Lunch Box with Mozzarella",
    ///       "description": "Tartine description"        
    ///     }
    /// </remarks>
    [HttpPost("products")]
    [Produces("application/json")]
    public ProductViewDto CreateProduct(ProductCreateDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        
        var result = _mapper.Map<ProductViewDto>(_productService.CreateProduct(product));
        return result;
    }
    
    /// <summary>
    /// Deletes Product with id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("products")]
    public ActionResult DeleteProduct(Guid id)
    {
        var product = _productService.GetProductById(id);
        if (product == null)
        {
            return NotFound($"Product with Id = {id} not found");
        }
        
        _productService.DeleteProduct(product);
        return Ok();
    }
    
    /// <summary>
    /// Updates the Product
    /// </summary>
    /// <param name="id"></param>
    /// <param name="productUpdateDto"></param>
    /// <returns></returns>
    [HttpPut("products/{id:Guid}")]
    public ActionResult<ProductViewDto> UpdateProduct(Guid id, ProductUpdateDto productUpdateDto)
    {
        try
        {
            var productToUpdate = _productService.GetProductById(id);

            if (productToUpdate == null)
                return NotFound($"Product with Id = {id} not found");

            productToUpdate.Amount = productUpdateDto.Amount;
            productToUpdate.Description = productUpdateDto.Description;
            productToUpdate.Name = productUpdateDto.Name;
            productToUpdate.Price = productUpdateDto.Price;
            productToUpdate.SubTitle = productUpdateDto.SubTitle;
            
            return _mapper.Map<ProductViewDto>(_productService.Update(productToUpdate));
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error updating data");
        }
    }
}