using Autofac;

namespace EmployeePortal.Infrastructure.RequestHandling
{
    public class RequestHandlerFactory
    {
        private readonly IComponentContext _container;

        public RequestHandlerFactory(IComponentContext container)
        {
            _container = container;
        }

        public RequestHandlerController<T, TY> Get<T, TY>(T request)
            where T : ValidatedRequest
            where TY: ResponseBase
        {
            var requestHandler = _container.Resolve<RequestHandler<T, TY>>(new TypedParameter(typeof(T), request));
            return new RequestHandlerController<T, TY>(requestHandler);
        }
    }
}
