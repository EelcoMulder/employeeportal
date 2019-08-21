using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.Infrastructure.Extensions
{
    public static class DbContextUpdateSetModified
    {
        public static void SetModified<T>(this DbContext context, IEnumerable<T> entities)
        {
            foreach (var e in entities)
            {
                if (!new[] {EntityState.Added, EntityState.Deleted}.Contains(context.Entry(e).State))
                {
                    context.Entry(e).State = EntityState.Modified;
                }
            }
        }
    }
}