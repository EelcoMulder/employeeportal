using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePortal.Infrastructure.RequestHandling.Exceptions
{
    public class EntityNotFoundException : BusinessLogicException
    {
        public EntityNotFoundException()
            : base() { }

        public EntityNotFoundException(string message)
            : base(message) { }

        public EntityNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }

        public EntityNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    }
}
