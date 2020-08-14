using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace aspnetcore3_demo.Helpers {
    /// <summary>
    /// 分页类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : List<T> where T : class {
        /// <summary>
        /// 当前页
        /// </summary>
        /// <value></value>
        public int CurrentPage { get; private set; }
        /// <summary>
        /// 总页数
        /// </summary>
        /// <value></value>
        public int TotalPages { get; private set; }
        /// <summary>
        /// 每页多少条
        /// </summary>
        /// <value></value>
        public int PageSize { get; private set; }
        /// <summary>
        /// 总条数
        /// </summary>
        /// <value></value>
        public int TotalCount { get; private set; }
        /// <summary>
        /// 是否有上一页
        /// </summary>
        /// <value></value>
        public bool HasPrevious => CurrentPage > 1;
        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList (List<T> items, int count, int pageNumber, int pageSize) {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling (count / (double) pageSize);
            this.AddRange (items);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="pageNumber">当前页</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>分页对象</returns>
        public static async Task<PagedList<T>> Create (IQueryable<T> source, int pageNumber, int pageSize) {
            var count = await source.CountAsync ();
            var items = await source.Skip ((pageNumber - 1) * pageSize).Take (pageSize).ToListAsync ();
            return new PagedList<T> (items, count, pageNumber, pageSize);
        }
    }
}
