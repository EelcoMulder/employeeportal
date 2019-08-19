using EmployeePortal.Infrastructure.PageModel;
using EmployeePortal.Infrastructure.RequestHandling;
using EmployeePortal.TimeRegistration.TimeSheets;
using Optional;

namespace EmployeePortal.TimeRegistration.Pages.Timesheet
{
    public class EditModel : RequestPageModel
    {
        public Option<Model.TimeSheet> TimeSheet { get; set; }

        public EditModel(RequestHandlerFactory requestHandlerFactory) : base(requestHandlerFactory) {}

        public void OnGet(int id)
        {
            PageTitle = "Edit Timesheet";
            Execute(() =>
            {
                var request = new GetTimeSheetRequest(id);
                var response = RequestHandlerFactory
                    .Get<GetTimeSheetRequest, GetTimeSheetReponse>(request)
                    .Handle();

                TimeSheet = response.TimeSheet;
            });
        }
    }
}