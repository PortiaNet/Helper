using Microsoft.EntityFrameworkCore;

namespace PortiaNet.Helper.Pagination
{
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

        public static async Task<SelectablePaginationModel<T>> GetSelectablePaginationAsync<T, K>(IQueryable<K> allData,
            Func<IQueryable<K>, IQueryable<K>>? filter,
            Func<IQueryable<K>, IQueryable<K>>? sort,
            Func<List<K>, List<T>> mapping, int pageSize, int pageIndex)
            where T : ISelectableModel, new()
        {
            var result = new SelectablePaginationModel<T>
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

        public static async Task<SelectablePaginationModel<T>> GetSelectablePaginationAsync<T, K>(IQueryable<K> allData,
            Func<IQueryable<K>, Task<IQueryable<K>>>? filter,
            Func<IQueryable<K>, Task<IQueryable<K>>>? sort,
            Func<List<K>, Task<List<T>>> mapping, int pageSize, int pageIndex)
            where T : ISelectableModel, new()
        {
            var result = new SelectablePaginationModel<T>
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
