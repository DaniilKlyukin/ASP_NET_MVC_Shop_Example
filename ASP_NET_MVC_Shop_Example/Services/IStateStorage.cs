using ASP_NET_MVC_Shop_Example.Models;

namespace ASP_NET_MVC_Shop_Example.Services
{
    public interface IStateStorage
    {
        void Store(SortState state);

        SortState? Stored();
    }
}