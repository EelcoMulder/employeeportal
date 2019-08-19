using EmployeePortal.Infrastructure.PageModel;
using EmployeePortal.Infrastructure.RequestHandling;
using EmployeePortal.Infrastructure.Services;
using EmployeePortal.TimeRegistration.Model;
using EmployeePortal.TimeRegistration.TimeSheets;
using System.Collections.Generic;
using System.Linq;

namespace EmployeePortal.TimeRegistration.Pages.Timesheet
{
    public class OverviewModel : RequestPageModel
    {
        private readonly CurrentUserService _currentUserService;
        public IList<TimeSheet> TimeSheets { get; set; }
        public CurrentUser CurrentUser { get; set; }
        public void OnGet()
        {
            CurrentUser = _currentUserService.Provide();
            Execute(() =>
            {
                var request = new TimeSheetOverviewRequest(CurrentUser.Id);

                var response = RequestHandlerFactory
                    .Get<TimeSheetOverviewRequest, TimeSheetOverviewReponse>(request)
                    .Handle();

                TimeSheets = response.TimeSheets.ToList();
            });
        }

        public OverviewModel(
            RequestHandlerFactory requestHandlerFactory,
            CurrentUserService currentUserService) : base(requestHandlerFactory)
        {
            _currentUserService = currentUserService;
        }
    }
}