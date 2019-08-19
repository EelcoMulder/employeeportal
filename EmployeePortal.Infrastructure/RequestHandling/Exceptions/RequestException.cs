using System;

namespace EmployeePortal.Infrastructure.RequestHandling.Exceptions
{
    public abstract class RequestException : Exception
    {
        protected RequestException()
            : base() { }

        protected RequestException(string message)
            : base(message) { }

        protected RequestException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        protected RequestException(string message, Exception innerException)
            : base(message, innerException) { }

        protected RequestException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    }
}
