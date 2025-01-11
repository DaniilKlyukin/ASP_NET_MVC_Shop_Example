namespace ASP_NET_MVC_Shop_Example.Services
{
    public class SortStateService
    {
        public IStateStorage Storage { get; }

        public SortStateService(IStateStorage storage)
        {
            Storage = storage;
        }
    }
}