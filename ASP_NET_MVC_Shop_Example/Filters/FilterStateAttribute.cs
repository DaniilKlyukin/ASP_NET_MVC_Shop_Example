using ASP_NET_MVC_Shop_Example.Data;
using ASP_NET_MVC_Shop_Example.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace ASP_NET_MVC_Shop_Example.Filters
{
    public class FilterStateAttribute : Attribute, IAsyncActionFilter
    {
        private readonly ApplicationDbContext _context;

        public FilterStateAttribute(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.ActionArguments.TryGetValue("filterState", out var filterStateObj);

            if (filterStateObj == null || filterStateObj is not FilterState state)
                throw new ArgumentException("Ну это не дело, фильтр потеряли"); // В принципе, такого случиться не должно

            var minPrice = state.MinPrice ?? await _context.Products.MinAsync(p => p.Price); // Если минимальная или максимальная цена не заданы, то достаём их из БД
            var maxPrice = state.MaxPrice ?? await _context.Products.MaxAsync(p => p.Price);

            state = new FilterState(minPrice, maxPrice);

            context.ActionArguments["filterState"] = state;

            await next();
        }
    }
}
