using System.IO;
using System.Linq;

namespace EmployeePortal.Infrastructure.Services
{
    public class RootDirectoryService
    {
        public static string Get()
        {
            bool IsRunningInVisualStudio(string s)
            {
                return (Directory.GetFiles(s, "*.csproj").Any());
            }
            var currentDirectory = Directory.GetCurrentDirectory();
            return IsRunningInVisualStudio(currentDirectory) 
                ? Directory.GetParent(currentDirectory).FullName 
                : currentDirectory;
        }
    }
}