using EmployeePortal.Infrastructure.RequestHandling.Exceptions;

namespace EmployeePortal.Infrastructure.RequestHandling
{
    public class RequestHandlerController<T, TY>
        where T : ValidatedRequest
        where TY: IResponse
    {
        private readonly RequestHandler<T, TY> _requestHandler;

        public RequestHandlerController(RequestHandler<T, TY> requestHandler)
        {
            _requestHandler = requestHandler;
        }

        public TY Handle()
        {
            // TODO: Logging here?
            if (!_requestHandler.Request.IsValid)
            {
                throw new ValidationException(_requestHandler.Request.Exceptions);
            }
            return _requestHandler.Handle();
        }
    }
}
