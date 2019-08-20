using System;
using EmployeePortal.Infrastructure.RequestHandling;
using EmployeePortal.Infrastructure.RequestHandling.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.Infrastructure.ApiController
{
    public class RequestController : ControllerBase
    {
        public RequestHandlerFactory RequestHandlerFactory;

        public RequestController(RequestHandlerFactory requestHandlerFactory)
        {
            RequestHandlerFactory = requestHandlerFactory;
        }

        public ActionResult Execute(Action executionCode)
        {
            try
            {
                executionCode.Invoke();
                return Ok();
            }
            catch (RequestException e) when (e is ValidationException exception)
            {
                return BadRequest(e);
            }
            catch (RequestException e) when (e is BusinessLogicException exception)
            {
                return Conflict(e);
            }
            catch (RequestException e) when (e is InfrastructureException exception)
            {
                return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
            }
        }

        public ActionResult<T> Execute<T>(Func<T> executionCode)
        {
            try
            {
                return new ActionResult<T>(executionCode.Invoke());
            }
            catch (RequestException e) when (e is ValidationException exception)
            {
                return BadRequest(e);
            }
            catch (RequestException e) when (e is BusinessLogicException exception)
            {
                return Conflict(e);
            }
            catch (RequestException e) when (e is InfrastructureException exception)
            {
                return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
            }
        }
    }
}