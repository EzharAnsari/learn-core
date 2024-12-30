using Microsoft.EntityFrameworkCore;

namespace Basic.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertParametersPaginationInHeader<T>(this HttpContext httpContext,
            IQueryable<T> queryable)
            {
                if (httpContext == null)
                {
                    throw new ArgumentException(nameof(httpContext));
                }

                double count = await queryable.CountAsync();
                httpContext.Response.Headers.Add("totalAmountOfRecords", count.ToString());
            }
    }
}