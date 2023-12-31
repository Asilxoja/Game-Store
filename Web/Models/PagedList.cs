﻿namespace Web.Models
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage - 1 < TotalPages;

        public PagedList(List<T> items, int count, int? pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber ?? 1;
            TotalPages = (int)Math.Ceiling((decimal)(count / (double)pageSize));

            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IEnumerable<T> source, int? pageNumber, int pageSize)
        {
            pageNumber--;
            var count = source.Count();
            var items = source.Skip((pageNumber ?? 1 - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
