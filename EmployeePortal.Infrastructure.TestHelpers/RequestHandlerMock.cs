using System;
using EmployeePortal.Infrastructure.RequestHandling;

namespace EmployeePortal.Infrastructure.TestHelpers
{
    public class RequestHandlerMock<T, TY> : RequestHandler<T, TY>
        where T : ValidatedRequest
        where TY : ResponseBase
    {
        private readonly Func<TY> _handle;

        public RequestHandlerMock(T request, Func<TY> handle) : base(request)
        {
            _handle = handle;
        }

        public override TY Handle()
        {
            return _handle.Invoke();
        }
    }
}