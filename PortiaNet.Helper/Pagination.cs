using Microsoft.EntityFrameworkCore;

namespace PortiaNet.Helper
{
    public class PaginationModel
    {
        public int TotalRecords { get; set; }

        public int FilteredRecords { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int CurrentPageRecordsCount { get; protected set; }
    }

    public interface ISelectableModel
    {
        bool IsSelected { get; set; }
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

    public static class PaginationHelper
    {
        public static async Task<PaginationModel<T>> GetPaginationAsync<T, K>(IQueryable<K> allData,
            Func<IQueryable<K>, IQueryable<K>>? filter,
            Func<IQueryable<K>, IQueryable<K>>? sort,
            Func<List<K>, List<T>> mapping, int pageSize, int pageIndex)
        {
            var result = new PaginationModel<T>
            {
                TotalRecords = await allData.CountAsync(),
                PageSize = pageSize
            };

            if (filter != null)
                allData = filter(allData);

            result.FilteredRecords = await allData.CountAsync();
            result.PageIndex = (int)Math.Min(Math.Max(pageIndex, 1), Math.Ceiling(result.FilteredRecords / (double)pageSize));

            if (sort != null)
                allData = sort.Invoke(allData);

            var entities = await allData
                .Skip(Math.Max(0, result.PageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            result.Items = mapping(entities);
            return result;
        }

        public static async Task<PaginationModel<T>> GetPaginationAsync<T, K>(IQueryable<K> allData,
            Func<IQueryable<K>, Task<IQueryable<K>>>? filter,
            Func<IQueryable<K>, Task<IQueryable<K>>>? sort,
            Func<List<K>, Task<List<T>>> mapping, int pageSize, int pageIndex)
        {
            var result = new PaginationModel<T>
            {
                TotalRecords = await allData.CountAsync(),
                PageSize = pageSize
            };

            if (filter != null)
                allData = await filter(allData);

            result.FilteredRecords = await allData.CountAsync();
            result.PageIndex = (int)Math.Min(Math.Max(pageIndex, 1), Math.Ceiling(result.FilteredRecords / (double)pageSize));

            if (sort != null)
                allData = await sort.Invoke(allData);

            var entities = await allData
                .Skip(Math.Max(0, result.PageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            result.Items = await mapping(entities);
            return result;
        }
    }
}
