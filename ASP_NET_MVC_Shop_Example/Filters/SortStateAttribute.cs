using ASP_NET_MVC_Shop_Example.Models;
using ASP_NET_MVC_Shop_Example.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP_NET_MVC_Shop_Example.Filters
{
    public class SortStateAttribute : Attribute, IAsyncActionFilter
    {
        private readonly SortStateService _sortStateService;

        public SortStateAttribute(SortStateService sortStateService)
        {
            _sortStateService = sortStateService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.ActionArguments.TryGetValue("sortState", out var sortStateObj);

            SortState state;
            if (sortStateObj == null || sortStateObj is not SortState parsedState)
                state = new SortState("Name", SortDirection.Ascending);
            else
            {
                var field = parsedState.SortField ?? "Name";

                state = new SortState(field, parsedState.SortDirection);
            }

            if (state != null)
            {
                var stored = _sortStateService.Storage.Stored();

                if (stored != null && state.SortField == stored.SortField)
                {
                    var newDirection = state.SortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;
                    state = new SortState(state.SortField, newDirection);
                }

                _sortStateService.Storage.Store(state);

                context.ActionArguments["sortState"] = state;
            }
            await next();
        }
    }
}
