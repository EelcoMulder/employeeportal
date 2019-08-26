using EmployeePortal.Infrastructure.RequestHandling.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq.AutoMock;
using Xunit;

namespace EmployeePortal.Infrastructure.Tests.RequestHandling
{
    public class RequestControllerTest
    {
        private RequestControllerTestClass _subject;

        public RequestControllerTest()
        {
            var mocker = new AutoMocker();
            _subject = mocker.CreateInstance<RequestControllerTestClass>();
        }

        [Fact]
        public void RequestController_WhenExecutedSuccessFully_ItShouldReturnOk()
        {
            // Arrange
            // Act
            var result =  _subject.Execute(() => { });

            // Assert
            Assert.IsAssignableFrom<OkResult>(result);
        }

        [Fact]
        public void RequestController_WhenExecuteThrowsValidationException_ItShouldReturnBadRequest()
        {
            // Arrange
            // Act
            var result =  _subject.Execute(() => throw new ValidationException());

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Fact]
        public void RequestController_WhenExecuteThrowsBusinessLogicException_ItShouldReturnConflict()
        {
            // Arrange
            // Act
            var result =  _subject.Execute(() => throw new BusinessLogicException());

            // Assert
            Assert.IsAssignableFrom<ConflictObjectResult>(result);
        }

        [Fact]
        public void RequestController_WhenExecuteThrowsInfrastructureException_ItShouldReturnServiceUnavailable()
        {
            // Arrange
            // Act
            var result =  _subject.Execute(() => throw new InfrastructureException());

            // Assert
            Assert.IsAssignableFrom<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status503ServiceUnavailable,  ((StatusCodeResult)result).StatusCode);
        }

        // <T>
        [Fact]
        public void RequestController_WhenGenericExecutedSuccessFully_ItShouldReturnOk()
        {
            // Arrange
            var thevalue = true;
            // Act
            var result =  _subject.Execute<bool>(() => thevalue);

            // Assert
            Assert.IsAssignableFrom<OkObjectResult>(result.Result);
            Assert.Equal(thevalue, ((OkObjectResult)result.Result).Value);
        }

        [Fact]
        public void RequestController_WhenGenericExecuteThrowsValidationException_ItShouldReturnBadRequest()
        {
            // Arrange
            // Act
            var result =  _subject.Execute<bool>(() => throw new ValidationException());

            // Assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }
        [Fact]
        public void RequestController_WhenGenericExecuteThrowsEntityNotFoundException_ItShouldReturnNotFound()
        {
            // Arrange
            // Act
            var result = _subject.Execute(() => throw new EntityNotFoundException());

            // Assert
            Assert.IsAssignableFrom<NotFoundObjectResult>(result);
        }

        [Fact]
        public void RequestController_WhenGenericExecuteThrowsBusinessLogicException_ItShouldReturnConflict()
        {
            // Arrange
            // Act
            var result =  _subject.Execute(() => throw new BusinessLogicException());

            // Assert
            Assert.IsAssignableFrom<ConflictObjectResult>(result);
        }

        [Fact]
        public void RequestController_WhenGenericExecuteThrowsInfrastructureException_ItShouldReturnServiceUnavailable()
        {
            // Arrange
            // Act
            var result =  _subject.Execute(() => throw new InfrastructureException());

            // Assert
            Assert.IsAssignableFrom<StatusCodeResult>(result);
            Assert.Equal(StatusCodes.Status503ServiceUnavailable, ((StatusCodeResult)result).StatusCode);
        }
    }
}
