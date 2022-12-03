namespace PortiaNet.Helper.Pagination
{
    public class PaginationModel
    {
        public int TotalRecords { get; set; }

        public int FilteredRecords { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int CurrentPageRecordsCount { get; protected set; }
    }

    public class PaginationModel<T> : PaginationModel
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
