using Castle.DynamicProxy;
using System.Diagnostics;
using System.Linq;

namespace EmployeePortal.Infrastructure.Logging
{
    public class LoggingInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Debug.WriteLine("Calling method {0} with parameters {1}... ",
                invocation.Method.Name,
                string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray()));

            invocation.Proceed();

            Debug.WriteLine("Done: result was {0}.", invocation.ReturnValue);
        }
    }
}
