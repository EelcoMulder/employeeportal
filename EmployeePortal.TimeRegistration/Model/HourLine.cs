using System;
using System.Dynamic;

namespace EmployeePortal.TimeRegistration.Model
{
    public class HourLine
    {
        public int Id { get; set; }
        public int TimeSheetId { get; set; }
        public byte DayOfMonth { get; set; }
        public byte Hours { get; set; }
        public string Description { get; set; }
        public TimeSheet TimeSheet { get; set; }
    }
}