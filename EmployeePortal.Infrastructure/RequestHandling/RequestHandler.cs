namespace EmployeePortal.Infrastructure.RequestHandling
{
    public abstract class RequestHandler<T, TY>
        where T : ValidatedRequest
        where TY : IResponse
    {
        protected RequestHandler(T request)
        {
            Request = request;
        }

        public T Request { get; }
        public abstract TY Handle();
    }
}