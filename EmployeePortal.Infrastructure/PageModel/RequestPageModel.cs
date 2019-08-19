using System;
using System.Collections.Generic;
using System.Linq;
using EmployeePortal.Infrastructure.RequestHandling;
using EmployeePortal.Infrastructure.RequestHandling.Exceptions;

namespace EmployeePortal.Infrastructure.PageModel
{
    public class RequestPageModel : Microsoft.AspNetCore.Mvc.RazorPages.PageModel
    {
        public string PageTitle { get; set; } = string.Empty;
        public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
        public RequestHandlerFactory RequestHandlerFactory;

        public RequestPageModel(RequestHandlerFactory requestHandlerFactory)
        {
            RequestHandlerFactory = requestHandlerFactory;
        }

        public void Execute(Action executionCode)
        {
            try
            {
                executionCode.Invoke();
            }
            catch (RequestException e) when (e is ValidationException exception)
            {
                Errors = exception.Exceptions;
            }
            catch (RequestException e)
            {
                Errors = new[] { e.Message };
            }
        }
    }
}
