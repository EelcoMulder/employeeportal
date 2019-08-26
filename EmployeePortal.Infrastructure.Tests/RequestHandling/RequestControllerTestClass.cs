using EmployeePortal.Infrastructure.RequestHandling;

namespace EmployeePortal.Infrastructure.Tests.RequestHandling
{
    internal class RequestControllerTestRequest : ValidatedRequest {}

    internal class RequestControllerTestClass : RequestController
    {
        public RequestControllerTestClass(IRequestHandlerFactory requestHandlerFactory) : base(requestHandlerFactory)
        {
        }
    }
}
