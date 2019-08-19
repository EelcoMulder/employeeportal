using System.Collections.Generic;
using System.Linq;

namespace EmployeePortal.Infrastructure.RequestHandling
{
    public abstract class ValidatedRequest
    {
        private readonly IList<string> _exceptions = new List<string>();

        protected internal void AddException(string exception)
        {
            _exceptions.Add(exception);
        }

        internal IEnumerable<string> Exceptions => _exceptions;
        public bool IsValid => !Exceptions.Any();
    }
}
