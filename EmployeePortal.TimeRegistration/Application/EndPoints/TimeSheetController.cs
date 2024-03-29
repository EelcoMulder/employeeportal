﻿using EmployeePortal.Infrastructure.RequestHandling;
using EmployeePortal.Infrastructure.RequestHandling.Exceptions;
using EmployeePortal.Infrastructure.Services;
using EmployeePortal.TimeRegistration.Domain.Model;
using EmployeePortal.TimeRegistration.Domain.TimeSheets;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EmployeePortal.TimeRegistration.Application.EndPoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetController : RequestController
    {
        private readonly CurrentUserService _currentUserService;

        public TimeSheetController(IRequestHandlerFactory requestHandlerFactory, CurrentUserService currentUserService)
            : base(requestHandlerFactory)
        {
            _currentUserService = currentUserService;
        }

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

                return response.TimeSheet.ValueOr(() => throw new EntityNotFoundException());
            });
        }

        [HttpGet]
        public ActionResult<TimeSheet[]> Get()
        {
            var currentUser = _currentUserService.Provide();

            return Execute(() =>
            {
                var request = new TimeSheetOverviewRequest(currentUser.Id);

                var response = RequestHandlerFactory
                    .Get<TimeSheetOverviewRequest, TimeSheetOverviewReponse>(request)
                    .Handle();

                return response.TimeSheets.ToArray();
            });
        }

        [HttpPost]
        public ActionResult<TimeSheet> Post([FromBody]TimeSheet timeSheet)
        {
            return Execute(() =>
            {
                var request = new StoreTimeSheetRequest(timeSheet);

                RequestHandlerFactory
                     .Get<StoreTimeSheetRequest, ResponseBase>(request)
                     .Handle();

                return timeSheet;
            });
        }
    }
}