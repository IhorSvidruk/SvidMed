using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SvidMed.Common;
using SvidMed.Web.Controllers;

namespace SvidMed.Web.Areas.PatientRole.Controllers
{
    [Authorize(Roles = GlobalConstants.PatientRoleName)]
    [Area("PatientRole")]
    public class PatientRoleController : BaseController
    {
    }
}
