using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeePortal.Infrastructure.RequestHandling.Exceptions
{
    public class ValidationException : RequestException
    {
        public IEnumerable<string> Exceptions { get; private set; } = Enumerable.Empty<string>();

        public ValidationException(IEnumerable<string> exceptions)
        {
            Exceptions = exceptions;
        }

        public ValidationException()
            : base() { }

        public ValidationException(string message)
            : base(message) { }

        public ValidationException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public ValidationException(string message, Exception innerException)
            : base(message, innerException) { }

        public ValidationException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    }
}
