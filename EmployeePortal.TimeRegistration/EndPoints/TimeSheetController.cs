﻿using EmployeePortal.Infrastructure.ApiController;
using EmployeePortal.Infrastructure.RequestHandling;
using EmployeePortal.TimeRegistration.Model;
using EmployeePortal.TimeRegistration.TimeSheets;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EmployeePortal.TimeRegistration.EndPoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetController : RequestController
    {
        public TimeSheetController(RequestHandlerFactory requestHandlerFactory) : base(requestHandlerFactory) {}

        [HttpGet("{id}")]
        public ActionResult<TimeSheet> Get(int id)
        {
            // TODO: Dtos something something?
            return Execute(() =>
            {
                var request = new GetTimeSheetRequest(id);

                var response = RequestHandlerFactory
                    .Get<GetTimeSheetRequest, GetTimeSheetReponse>(request)
                    .Handle();

                return response.TimeSheet.ValueOr(() => throw new NotImplementedException());
            });
        }
    }
}
