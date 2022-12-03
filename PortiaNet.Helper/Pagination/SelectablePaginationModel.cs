namespace PortiaNet.Helper.Pagination
{
    public interface ISelectableModel
    {
        bool IsSelected { get; set; }
    }

    public class SelectablePaginationModel<T> : PaginationModel
        where T : ISelectableModel
    {
        private List<T> _items = new();
        public List<T> Items
        {
            get => _items;
            set
            {
                _items = value;
                if (value == null)
                    CurrentPageRecordsCount = 0;
                else
                    CurrentPageRecordsCount = value.Count;
            }
        }
    }
}
