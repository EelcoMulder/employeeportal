using System;
using Xunit;

namespace EmployeePortal.TimeRegistration.Tests
{
    public class Concrete
    {
        public string Hello()
        {
            return "Hello";
        }
    }

    public class TestConcrete : Concrete
    {
        public new string Hello()
        {
            return String.Empty;
        }
    }

    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var concrete = new TestConcrete();
            Assert.Equal(String.Empty, concrete.Hello());
        }
    }
}
