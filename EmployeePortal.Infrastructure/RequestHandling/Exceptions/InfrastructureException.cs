using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePortal.Infrastructure.RequestHandling.Exceptions
{
    public class InfrastructureException : RequestException
    {
        public InfrastructureException()
            : base() { }

        public InfrastructureException(string message)
            : base(message) { }

        public InfrastructureException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public InfrastructureException(string message, Exception innerException)
            : base(message, innerException) { }

        public InfrastructureException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    }
}
