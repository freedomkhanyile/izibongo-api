using System;
using System.Collections.Generic;
using System.Linq;

namespace izibongo.api.API.Helpers.HATEOAS
{
    public class PageList<T> : List<T>
    {
        public uint CurrentPage { get; set; }
        public uint TotalPages { get; set; }
        public uint PageSize { get; set; }
        public uint TotalCount { get; set; }
        public bool HasPrevious => this.CurrentPage > 1;
        public bool HasNext => this.CurrentPage < this.TotalPages;

        public PageList(
            IEnumerable<T> items,
            uint count,
            uint pageNumber,
            uint pageSize)
        {
            this.TotalCount = count;
            this.PageSize = pageSize;
            this.CurrentPage = pageNumber;
            this.TotalPages = (uint)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public static PageList<T> Create(
            IEnumerable<T> source,
            uint pageNumber,
            uint pageSize)
        {
            var count = (uint)source.Count();
            var items = source
                .Skip((int)((pageNumber - 1 ) * pageSize))
                .Take((int)pageSize)
                .ToList();
            return new PageList<T>(items, count, pageNumber, pageSize);
        }
    }
}