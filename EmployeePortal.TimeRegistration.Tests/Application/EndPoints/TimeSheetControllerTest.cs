using System;
using System.Linq;
using EmployeePortal.Infrastructure.RequestHandling;
using EmployeePortal.Infrastructure.Services;
using EmployeePortal.Infrastructure.TestHelpers;
using EmployeePortal.TimeRegistration.Application.EndPoints;
using EmployeePortal.TimeRegistration.Domain.Model;
using EmployeePortal.TimeRegistration.Domain.TimeSheets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using Optional;
using Xunit;

namespace EmployeePortal.TimeRegistration.Tests.Application.EndPoints
{
    public class TimeSheetControllerTest
    {
        private readonly TimeSheetController _subject;
        private readonly Mock<IRequestHandlerFactory> _requestHandlerFactory;
        private readonly AutoMocker _mocker = new AutoMocker();
        private const int ValidId = 1;
        private const string ValidUserId = "TESTUSERID";

        public TimeSheetControllerTest()
        {
            _requestHandlerFactory = _mocker.GetMock<IRequestHandlerFactory>();
            var currentUserService = new CurrentUserServiceDouble("TESTID", "TESTUSERNAME");
            _mocker.Use<CurrentUserService>(currentUserService);
            _subject = _mocker.CreateInstance<TimeSheetController>();
        }

        [Fact]
        public void TimeSheetController_WhenGetByIdFound_ItShouldReturnOkWithTimeSheet()
        {
            // Arrange
            var request = new GetTimeSheetRequest(ValidId);
            WithHandlerSetup(request, () => new GetTimeSheetReponse(Option.Some(new TimeSheet())));
            // Act
            var result = _subject.Get(ValidId);
            // Assert
            Assert.IsAssignableFrom<ActionResult<TimeSheet>>(result);
            Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            _mocker.VerifyAll();
        }

        [Fact]
        public void TimeSheetController_WhenGetByIdNotFound_ItShouldReturnNotFoundResult()
        {
            // Arrange
            var request = new GetTimeSheetRequest(ValidId);
            WithHandlerSetup(request, () => new GetTimeSheetReponse(Option.None<TimeSheet>()));
            // Act
            var result = _subject.Get(ValidId);
            // Assert
            Assert.IsAssignableFrom<ActionResult<TimeSheet>>(result);
            Assert.IsAssignableFrom<NotFoundObjectResult>(result.Result);
            _mocker.VerifyAll();
        }

        [Fact]
        public void TimeSheetController_WhenGetNoResults_ItShouldReturnOkWithEmptyList()
        {
            // Arrange
            var request = new TimeSheetOverviewRequest(ValidUserId);
            WithHandlerSetup(request, () => new TimeSheetOverviewReponse(Enumerable.Empty<TimeSheet>()));
            // Act
            var result = _subject.Get();
            // Assert
            Assert.IsAssignableFrom<ActionResult<TimeSheet[]>>(result);
            Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            _mocker.VerifyAll();
        }

        private void WithHandlerSetup<T, TY>(T request, Func<TY> handle)
            where T : ValidatedRequest
            where TY : ResponseBase
        {
            var requestHandler =
                new RequestHandlerMock<T, TY>(request, handle);
            var requestHandlerController =
                new RequestHandlerController<T, TY>(requestHandler);
            _requestHandlerFactory
                .Setup(r => r.Get<T, TY>(It.IsAny<T>()))
                .Returns(requestHandlerController);
        }
    }
}
