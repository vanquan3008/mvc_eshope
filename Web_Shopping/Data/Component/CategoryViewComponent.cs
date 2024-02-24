using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Shopping.Data;

namespace Web_Shopping.Data.Component
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly DataContext _dataContext;
        public CategoryViewComponent(DataContext Data)
        {
            _dataContext = Data;
        }
        public async Task<IViewComponentResult> InvokeAsync() => View(await _dataContext.Categories.ToListAsync());
    }
}
