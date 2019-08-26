using EmployeePortal.Infrastructure.Services;
using Microsoft.AspNetCore.Http;

namespace EmployeePortal.Infrastructure.TestHelpers
{
    public class CurrentUserServiceDouble : CurrentUserService
    {
        private readonly string _id;
        private readonly string _userName;

        public CurrentUserServiceDouble(string id, string userName) : base(new HttpContextAccessor())
        {
            _id = id;
            _userName = userName;
        }

        public override CurrentUser? Provide()
        {
            return new CurrentUser
            (
                id: _id,
                userName: _userName
            );
        }
    }
}