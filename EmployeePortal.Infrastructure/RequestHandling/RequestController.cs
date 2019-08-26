using System;
using EmployeePortal.Infrastructure.RequestHandling.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.Infrastructure.RequestHandling
{
    public abstract class RequestController : ControllerBase
    {
        public IRequestHandlerFactory RequestHandlerFactory;

        protected RequestController(IRequestHandlerFactory requestHandlerFactory)
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
            catch (ValidationException exception)
            {
                return BadRequest(exception);
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(exception);
            }
            catch (BusinessLogicException exception)
            {
                return Conflict(exception);
            }
            catch (InfrastructureException exception)
            {
                return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
            }// TODO: Unhandled
        }

        public ActionResult<T> Execute<T>(Func<T> executionCode)
        {
            try
            {
                return Ok(executionCode.Invoke());
            }
            catch (ValidationException exception)
            {
                return BadRequest(exception);
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(exception);
            }
            catch (BusinessLogicException exception)
            {
                return Conflict(exception);
            }
            catch (InfrastructureException)
            {
                return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
            }// TODO: Unhandled
        }
    }
}