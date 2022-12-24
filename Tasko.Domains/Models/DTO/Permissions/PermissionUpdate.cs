using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasko.Domains.Models.DTO.Permissions
{
    public interface IPermissionUpdate
    {
        string Name { get; set; }
        string DisplayName { get; set; }
        string Description { get; set; }
    }
    public class PermissionUpdate : IPermissionUpdate
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}
