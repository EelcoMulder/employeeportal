namespace EmployeePortal.Infrastructure.RequestHandling
{
    public interface IRequestHandlerController<TY> where TY : ResponseBase
    {
        TY Handle();
    }
}