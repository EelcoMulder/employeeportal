using System.Linq;
using EmployeePortal.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeePortal.Web.Application
{
    public class IndexModel : PageModel
    {
        public string UserName { get; set; } = string.Empty;

        private readonly CurrentUserService _currentUserService;

        public IndexModel(CurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public void OnGet()
        {
            var currentUser = _currentUserService.Provide();

            UserName = 
                currentUser == null 
                    ? "<Unkown user>" 
                    : currentUser.UserName;
        }
    }
}
