﻿using EmployeePortal.Infrastructure.ApiController;
using EmployeePortal.Infrastructure.RequestHandling;
using EmployeePortal.TimeRegistration.TimeSheets;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.TimeRegistration.EndPoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class HourLineController : RequestController
    {
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Execute(() =>
            {
                var request = new DeleteHourLineRequest(id);

                RequestHandlerFactory
                    .Get<DeleteHourLineRequest, ResponseBase>(request)
                    .Handle();
            });
        }

        public HourLineController(RequestHandlerFactory requestHandlerFactory) : base(requestHandlerFactory)
        {
        }
    }
}
