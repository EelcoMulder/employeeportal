using System.Linq;
using EmployeePortal.Infrastructure.RequestHandling;
using EmployeePortal.TimeRegistration.Domain.Model;
using EmployeePortal.TimeRegistration.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Optional;

namespace EmployeePortal.TimeRegistration.Domain.TimeSheets
{
    internal class GetTimeSheetRequest : ValidatedRequest
    {
        public GetTimeSheetRequest(int id)
        {
            TimeSheetId = id;
            if (TimeSheetId < 1)
            {
                AddException("Timesheet id not valid");
            }
        }

        public int TimeSheetId { get; }
    }

    internal class GetTimeSheetReponse : ResponseBase
    {
        public GetTimeSheetReponse(Option<TimeSheet> timeSheet)
        {
            TimeSheet = timeSheet;
        }

        public Option<TimeSheet> TimeSheet { get; }
    }

    internal class GetTimeSheetHandler : RequestHandler<GetTimeSheetRequest, GetTimeSheetReponse>
    {
        private readonly TimeSheetContext _timeSheetContext;

        public GetTimeSheetHandler(GetTimeSheetRequest getTimeSheetRequest, TimeSheetContext timeSheetContext) 
            : base(getTimeSheetRequest)
        {
            _timeSheetContext = timeSheetContext;
        }

        public override GetTimeSheetReponse Handle()
        {
            var timesheet = 
                _timeSheetContext
                    .TimeSheets
                    .Include(t => t.HourLines)
                    .FirstOrDefault(t => t.Id.Equals(Request.TimeSheetId));
            return timesheet == null
                ? new GetTimeSheetReponse(Option.None<TimeSheet>())
                : new GetTimeSheetReponse(Option.Some(timesheet));
        }
    }
}