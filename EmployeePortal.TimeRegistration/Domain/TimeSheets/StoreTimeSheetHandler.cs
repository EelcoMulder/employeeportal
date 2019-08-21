using EmployeePortal.Infrastructure.Extensions;
using EmployeePortal.Infrastructure.RequestHandling;
using EmployeePortal.TimeRegistration.Domain.Model;
using EmployeePortal.TimeRegistration.Infrastructure;

namespace EmployeePortal.TimeRegistration.Domain.TimeSheets
{
    internal class StoreTimeSheetRequest : ValidatedRequest
    {
        public StoreTimeSheetRequest(TimeSheet timeSheet)
        {
            if (timeSheet == null)
            {
                AddException("Timesheet niet valide");
            }

            TimeSheet = timeSheet;
        }

        public TimeSheet TimeSheet { get; }
    }

    internal class StoreTimeSheetHandler : RequestHandler<StoreTimeSheetRequest, ResponseBase>
    {
        private readonly TimeSheetContext _timeSheetContext;

        public StoreTimeSheetHandler(
            StoreTimeSheetRequest request,
            TimeSheetContext timeSheetContext) : base(request)
        {
            _timeSheetContext = timeSheetContext;
        }

        public override ResponseBase Handle()
        {
            _timeSheetContext.Attach(Request.TimeSheet);
            _timeSheetContext.SetModified(Request.TimeSheet.HourLines);
            _timeSheetContext.SaveChanges();
            return new ResponseBase();
        }
    }
}
