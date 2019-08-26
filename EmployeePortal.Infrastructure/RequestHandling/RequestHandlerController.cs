using EmployeePortal.Infrastructure.RequestHandling.Exceptions;

namespace EmployeePortal.Infrastructure.RequestHandling
{
    //Orchestrator? Dispatcher?
    public class RequestHandlerController<T, TY> : IRequestHandlerController<TY> 
        where T : ValidatedRequest
        where TY: ResponseBase
    {
        private readonly RequestHandler<T, TY> _requestHandler;

        public RequestHandlerController(RequestHandler<T, TY> requestHandler)
        {
            _requestHandler = requestHandler;
        }

        public TY Handle()
        {
            if (!_requestHandler.Request.IsValid)
            {
                throw new ValidationException(_requestHandler.Request.Exceptions);
            }
            return _requestHandler.Handle();
        }
    }
}
