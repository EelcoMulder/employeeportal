using System.Linq;
using EmployeePortal.Infrastructure.RequestHandling;
using EmployeePortal.TimeRegistration.Infrastructure;

namespace EmployeePortal.TimeRegistration.Domain.TimeSheets
{
    internal class DeleteHourLineRequest : ValidatedRequest
    {
        public DeleteHourLineRequest(int id)
        {
            HourLineId = id;
            if (HourLineId < 1)
            {
                AddException("Hourline id not valid");
            }
        }

        public int HourLineId { get; }
    }

    internal class DeleteHourLineHandler : RequestHandler<DeleteHourLineRequest, ResponseBase>
    {
        private readonly TimeSheetContext _timeSheetContext;

        public DeleteHourLineHandler(
            DeleteHourLineRequest request, 
            TimeSheetContext timeSheetContext) : base(request)
        {
            _timeSheetContext = timeSheetContext;
        }

        public override ResponseBase Handle()
        {
            var hourLine = _timeSheetContext
                .HourLines
                .FirstOrDefault(h => h.Id.Equals(Request.HourLineId));
            if (hourLine != null)
            {
                _timeSheetContext.HourLines.Remove(hourLine);
                _timeSheetContext.SaveChanges();
            }
            return new ResponseBase();
        }
    }
}