using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace humanResourceManager.Ulity
{
	public class PageResult<T>(List<T> items, int totalPages, int totalCount)
	{
		public IEnumerable<T> Items { get; set; } = items;
		public int TotalPages { get; set; } = totalPages;
		public int TotalCount { get; set; } = totalCount;

		public async static Task<PageResult<T>> PageAsync(IQueryable<T> source, int pageNumber, int pageSize)
		{
			var count = await source.CountAsync();
			var items = await source.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
			var totalPages = (int)Math.Ceiling(count / (double)pageSize);
			var totalCount = count;
			return new PageResult<T>(items, totalPages, totalCount);
		}

		public async static Task<PageResult<T>> PageAsync(IQueryable<T> source, int pageNumber, int pageSize, string FieldSort, bool FieldOption)
		{
			string propertyName;
			string methodName;
			ParameterExpression parameter;
			LambdaExpression lambda;
			MemberExpression property;
			var methodCallExpression = source.Expression;

			#region dùng cho việc Order
			if (string.IsNullOrEmpty(FieldSort))
			{
				FieldSort = source.ElementType.GetProperties()[0].Name;
				FieldOption = true;
			}
			propertyName = FieldSort;
			methodName = (FieldOption) ? "OrderByDescending" : "OrderBy";

			parameter = Expression.Parameter(source.ElementType, String.Empty);
			property = Expression.Property(parameter, propertyName);
			lambda = Expression.Lambda(property, parameter);
			methodCallExpression = Expression.Call(typeof(Queryable), methodName,
			[typeof(T), property.Type], methodCallExpression, Expression.Quote(lambda));

			source = source.Provider.CreateQuery<T>(methodCallExpression);
			#endregion

			var count = await source.CountAsync();
			var items = await source.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
			var totalPages = (int)Math.Ceiling(count / (double)pageSize);
			var totalCount = count;
			return new PageResult<T>(items, totalPages, totalCount);
		}
	}
}
