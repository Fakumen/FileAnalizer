using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemAnalizer.Infrastructure
{
    public static class UserAccess
    {
        public static bool IsCurrentUserAdmin
        {
            get
            {
                var principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
    }
}
