namespace EmployeePortal.Infrastructure.RequestHandling
{
    public interface IRequestHandlerFactory
    {
        RequestHandlerController<T, TY> Get<T, TY>(T request)
            where T : ValidatedRequest
            where TY: ResponseBase;
    }
}