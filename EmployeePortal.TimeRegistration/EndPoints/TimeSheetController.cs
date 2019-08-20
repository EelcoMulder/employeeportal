using EmployeePortal.Infrastructure.ApiController;
using EmployeePortal.Infrastructure.RequestHandling;
using EmployeePortal.TimeRegistration.Model;
using EmployeePortal.TimeRegistration.TimeSheets;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using EmployeePortal.Infrastructure.Services;

namespace EmployeePortal.TimeRegistration.EndPoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetController : RequestController
    {
        private readonly CurrentUserService _currentUserService;

        public TimeSheetController(RequestHandlerFactory requestHandlerFactory, CurrentUserService currentUserService)
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

                return response.TimeSheet.ValueOr(() => throw new NotImplementedException());
            });
        }

        [HttpGet()]
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
