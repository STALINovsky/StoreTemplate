using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace StoreTemplate.Constants
{
    public static class IdentityRolesConstants
    {
        public const string AdminRoleName = "Admin";
        public const string ManagerRoleName = "Manager";
        public const string VisitorRoleName = "Visitor";
    }
}
