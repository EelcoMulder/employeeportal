using EmployeePortal.Infrastructure.PageModel;
using EmployeePortal.Infrastructure.RequestHandling;
using EmployeePortal.Infrastructure.RequestHandling.Exceptions;
using EmployeePortal.TimeRegistration.Model;
using EmployeePortal.TimeRegistration.TimeSheets;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.TimeRegistration.Pages.Timesheet
{
    public class Edit_Model : RequestPageModel
    {
        public TimeSheet TimeSheet { get; set; }
        public void OnGet(int id)
        {
            Execute(() =>
            {
                var request = new GetTimeSheetRequest(id);

                var response = RequestHandlerFactory
                    .Get<GetTimeSheetRequest, GetTimeSheetReponse>(request)
                    .Handle();

                TimeSheet = response
                    .TimeSheet
                    .ValueOr(() => throw new BusinessLogicException("Timesheet not found"));
            });
        }

        public IActionResult OnPostDelete(int id, int hourLineId)
        {
            var request = new DeleteHourLineRequest(hourLineId);

            RequestHandlerFactory
                .Get<DeleteHourLineRequest, ResponseBase>(request)
                .Handle();

            return RedirectToPage("/Timesheet/Edit_", new {id = id});
        }

        public Edit_Model(RequestHandlerFactory requestHandlerFactory) : base(requestHandlerFactory)
        {
        }
    }
}