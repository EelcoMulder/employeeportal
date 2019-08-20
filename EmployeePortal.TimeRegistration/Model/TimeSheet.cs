using System.Collections;
using System.Collections.Generic;

namespace EmployeePortal.TimeRegistration.Model
{
    public class TimeSheet
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public IList<HourLine> HourLines { get; set; }
    }
}
