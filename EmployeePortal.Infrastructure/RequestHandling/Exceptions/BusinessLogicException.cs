using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePortal.Infrastructure.RequestHandling.Exceptions
{
    public class BusinessLogicException : RequestException
    {
        public BusinessLogicException()
            : base() { }

        public BusinessLogicException(string message)
            : base(message) { }

        public BusinessLogicException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public BusinessLogicException(string message, Exception innerException)
            : base(message, innerException) { }

        public BusinessLogicException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    }
}
