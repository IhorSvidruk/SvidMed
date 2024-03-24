using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SvidMed.Common;
using SvidMed.Web.Controllers;

namespace SvidMed.Web.Areas.DoctorRole.Controllers
{
    [Authorize(Roles = GlobalConstants.DoctorRoleName)]
    [Area("DoctorRole")]
    public class DoctorRoleController : BaseController
    {
    }
}
