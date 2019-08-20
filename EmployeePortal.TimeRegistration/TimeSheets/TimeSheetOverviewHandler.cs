using EmployeePortal.Infrastructure.RequestHandling;
using EmployeePortal.TimeRegistration.Infrastructure;
using EmployeePortal.TimeRegistration.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeePortal.TimeRegistration.TimeSheets
{
    internal class TimeSheetOverviewRequest : ValidatedRequest
    {
        public TimeSheetOverviewRequest(string userId)
        {
            UserId = userId;
            if (String.IsNullOrEmpty(UserId))
            {
                AddException("User id cannot be empty");
            }
        }

        public string UserId { get; }
    }

    internal class TimeSheetOverviewReponse : ResponseBase
    {
        public TimeSheetOverviewReponse(IEnumerable<TimeSheet> timeSheets)
        {
            TimeSheets = timeSheets;
        }

        public IEnumerable<TimeSheet> TimeSheets { get; }
    }

    internal class TimeSheetOverviewHandler : RequestHandler<TimeSheetOverviewRequest, TimeSheetOverviewReponse>
    {
        private readonly TimeSheetContext _timeSheetContext;

        public TimeSheetOverviewHandler(
            TimeSheetOverviewRequest request, 
            TimeSheetContext timeSheetContext) : base(request)
        {
            _timeSheetContext = timeSheetContext;
        }

        public override TimeSheetOverviewReponse Handle()
        {
            var timeSheets = 
                _timeSheetContext
                    .TimeSheets
                    .Where(t => t.UserId.Equals(Request.UserId));
            return new TimeSheetOverviewReponse(timeSheets);
        }
    }
}