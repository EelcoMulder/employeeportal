using EmployeePortal.TimeRegistration.Infrastructure;
using EmployeePortal.TimeRegistration.Model;
using EmployeePortal.TimeRegistration.TimeSheets;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace EmployeePortal.TimeRegistration.Tests.TimeSheets
{
    public class GetTimeSheetHandlerTest
    {
        private GetTimeSheetHandler _subject;
        private readonly TimeSheetContext _timeSheetContext;
        private const int ValidTimeSheetId = 1;
        private const int InValidTimeSheetId = 123;

        public GetTimeSheetHandlerTest()
        {
            _timeSheetContext = CreateTimeSheetContext();
        }

        [Fact]
        public void GetTimeSheetHandler_WhenCalled_ItShouldReturnGetTimeSheetReponse()
        {
            // Arrange
            var request = new GetTimeSheetRequest(1);
            _subject = new GetTimeSheetHandler(request, _timeSheetContext);
            // Act
            var result = _subject.Handle();
            // Assert
            Assert.IsAssignableFrom<GetTimeSheetReponse>(result);
        }

        [Fact]
        public void GetTimeSheetHandler_WhenCalledWithValidId_ItShouldReturnGetTimeSheetReponseWithTimeSheet()
        {
            // Arrange
            var request = new GetTimeSheetRequest(ValidTimeSheetId);
            _subject = new GetTimeSheetHandler(request, _timeSheetContext);
            WithTimeSheets();
            // Act
            var result = _subject.Handle();
            // Assert
            Assert.True(result.TimeSheet.HasValue);
        }

        [Fact]
        public void GetTimeSheetHandler_WhenCalledWithValidId_ItShouldReturnGetTimeSheetReponseWithNoTimeSheet()
        {
            // Arrange
            var request = new GetTimeSheetRequest(InValidTimeSheetId);
            _subject = new GetTimeSheetHandler(request, _timeSheetContext);
            WithTimeSheets();
            // Act
            var result = _subject.Handle();
            // Assert
            Assert.False(result.TimeSheet.HasValue);
        }

        private void WithTimeSheets()
        {
            var timeSheet = new TimeSheet
            {
                Id = ValidTimeSheetId
            };
            _timeSheetContext.TimeSheets.Add(timeSheet);
            _timeSheetContext.SaveChanges();
        }

        private static TimeSheetContext CreateTimeSheetContext()
        {
            var options = new DbContextOptionsBuilder<TimeSheetContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new TimeSheetContext(options);
            return context;
        }
    }
}
