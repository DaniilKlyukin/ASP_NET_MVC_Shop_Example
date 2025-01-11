using ASP_NET_MVC_Shop_Example.Models;

namespace ASP_NET_MVC_Shop_Example.Services
{
    public class PreviousStateStorage : IStateStorage
    {
        private SortState? _stored;

        public void Store(SortState state)
        {
            _stored = new SortState(state.SortField, state.SortDirection);
        }

        public SortState? Stored()
        {
            return _stored;
        }
    }
}